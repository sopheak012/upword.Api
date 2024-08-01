using Microsoft.EntityFrameworkCore;
using upword.Api.Data;
using upword.Api.Dtos;
using upword.Api.Entities;
using upword.Api.Mapping;

namespace upword.Api.Endpoints
{
    public static class UserWordsEndpoints
    {
        const string GetUserWordsEndpointName = "GetUserWords";
        const string CreateUserWordEndpointName = "CreateUserWord";
        const string UpdateUserWordEndpointName = "UpdateUserWord";
        const string DeleteUserWordEndpointName = "DeleteUserWord";

        public static RouteGroupBuilder MapUserWordsEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("userwords").WithParameterValidation();

            // Endpoint to retrieve all UserWords for a specific user
            group
                .MapGet(
                    "/{userId}",
                    async (string userId, upwordContext dbContext) =>
                    {
                        // Validate userId
                        var userExists = await dbContext.Users.AnyAsync(u => u.Id == userId);
                        if (!userExists)
                        {
                            return Results.NotFound("User not found.");
                        }

                        var userWords = await dbContext
                            .UserWords.Where(uw => uw.UserId == userId)
                            .Select(uw => new
                            {
                                uw.Id,
                                uw.UserId,
                                uw.WordId
                            })
                            .ToListAsync();

                        if (userWords.Count == 0)
                        {
                            return Results.Ok(new { Message = "No words found for this user." });
                        }

                        return Results.Ok(userWords);
                    }
                )
                .WithName(GetUserWordsEndpointName);

            // Endpoint to create a new UserWord
            group
                .MapPost(
                    "/",
                    async (CreateUserWordDto newUserWord, upwordContext dbContext) =>
                    {
                        // Validate userId and wordId
                        var userExists = await dbContext.Users.AnyAsync(u =>
                            u.Id == newUserWord.UserId
                        );
                        var wordExists = await dbContext.Words.AnyAsync(w =>
                            w.Id == newUserWord.WordId
                        );

                        if (!userExists)
                        {
                            return Results.NotFound("User not found.");
                        }

                        if (!wordExists)
                        {
                            return Results.NotFound("Word not found.");
                        }

                        // Check if the UserWord already exists for the user and word
                        bool exists = await dbContext.UserWords.AnyAsync(uw =>
                            uw.UserId == newUserWord.UserId && uw.WordId == newUserWord.WordId
                        );

                        if (exists)
                        {
                            return Results.Conflict("UserWord already exists.");
                        }

                        UserWord userWord = newUserWord.ToEntity(Guid.NewGuid().ToString());
                        dbContext.UserWords.Add(userWord);
                        await dbContext.SaveChangesAsync();

                        // Include the Word details in the response
                        var createdUserWord = await dbContext
                            .UserWords.Include(uw => uw.Word) // Include the related Word entity
                            .FirstOrDefaultAsync(uw => uw.Id == userWord.Id);

                        if (createdUserWord == null)
                        {
                            return Results.NotFound("UserWord not found.");
                        }

                        return Results.CreatedAtRoute(
                            CreateUserWordEndpointName,
                            new { id = createdUserWord.Id },
                            new
                            {
                                createdUserWord.Id,
                                createdUserWord.UserId,
                                createdUserWord.WordId,
                                Word = createdUserWord.Word.ToDto() // Ensure you have a ToDto() method in your Word class
                            }
                        );
                    }
                )
                .WithName(CreateUserWordEndpointName);

            // Endpoint to update an existing UserWord
            group
                .MapPut(
                    "/{userId}/{wordId}",
                    async (
                        string userId,
                        string wordId,
                        UpdateUserWordDto updatedUserWord,
                        upwordContext dbContext
                    ) =>
                    {
                        // Validate userId and wordId
                        var userExists = await dbContext.Users.AnyAsync(u => u.Id == userId);
                        var wordExists = await dbContext.Words.AnyAsync(w => w.Id == wordId);

                        if (!userExists)
                        {
                            return Results.NotFound("User not found.");
                        }

                        if (!wordExists)
                        {
                            return Results.NotFound("Word not found.");
                        }

                        var existingUserWord = await dbContext
                            .UserWords.Include(uw => uw.Word) // Include related Word entity if needed
                            .FirstOrDefaultAsync(uw => uw.UserId == userId && uw.WordId == wordId);

                        if (existingUserWord == null)
                        {
                            return Results.NotFound("UserWord not found.");
                        }

                        // Update properties if necessary
                        existingUserWord.UserId = updatedUserWord.UserId;
                        existingUserWord.WordId = updatedUserWord.WordId;

                        await dbContext.SaveChangesAsync();

                        return Results.Ok(existingUserWord.ToDto());
                    }
                )
                .WithName(UpdateUserWordEndpointName);

            // Endpoint to delete an existing UserWord
            group
                .MapDelete(
                    "/{userId}/{wordId}",
                    async (string userId, string wordId, upwordContext dbContext) =>
                    {
                        // Validate userId and wordId
                        var userExists = await dbContext.Users.AnyAsync(u => u.Id == userId);
                        var wordExists = await dbContext.Words.AnyAsync(w => w.Id == wordId);

                        if (!userExists)
                        {
                            return Results.NotFound("User not found.");
                        }

                        if (!wordExists)
                        {
                            return Results.NotFound("Word not found.");
                        }

                        // Log or output the parameters for debugging
                        Console.WriteLine(
                            $"Attempting to delete UserWord with Word ID: {wordId} for User ID: {userId}"
                        );

                        // Retrieve the UserWord to be deleted
                        var userWord = await dbContext
                            .UserWords.Include(uw => uw.Word) // Include related Word entity if needed
                            .FirstOrDefaultAsync(uw => uw.UserId == userId && uw.WordId == wordId);

                        if (userWord == null)
                        {
                            return Results.NotFound(
                                "UserWord not found or does not belong to this user."
                            );
                        }

                        dbContext.UserWords.Remove(userWord);
                        await dbContext.SaveChangesAsync();

                        return Results.Ok(userWord.ToDto());
                    }
                )
                .WithName(DeleteUserWordEndpointName);

            return group;
        }
    }
}
