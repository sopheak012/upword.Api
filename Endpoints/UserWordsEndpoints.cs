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
        const string DeleteUserWordEndpointName = "DeleteUserWord";

        public static RouteGroupBuilder MapUserWordsEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("userwords").WithParameterValidation();

            // Endpoint to retrieve all words for a specific user
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

                        // Retrieve the list of word values associated with the user
                        var userWordValues = await dbContext
                            .UserWords.Where(uw => uw.UserId == userId)
                            .Join(
                                dbContext.Words,
                                uw => uw.WordId,
                                w => w.Id,
                                (uw, w) => w.Value // Select the word value from the Word entity
                            )
                            .ToListAsync();

                        return Results.Ok(userWordValues);
                    }
                )
                .WithName(GetUserWordsEndpointName);

            // Endpoint to create a new UserWord
            group
                .MapPost(
                    "/",
                    async (CreateUserWordDto newUserWord, upwordContext dbContext) =>
                    {
                        // Validate userId and wordValue
                        var userExists = await dbContext.Users.AnyAsync(u =>
                            u.Id == newUserWord.UserId
                        );
                        var wordExists = await dbContext.Words.AnyAsync(w =>
                            w.Value == newUserWord.WordValue
                        );

                        if (!userExists)
                        {
                            return Results.NotFound("User not found.");
                        }

                        if (!wordExists)
                        {
                            return Results.NotFound("Word not found.");
                        }

                        var wordId = await dbContext
                            .Words.Where(w => w.Value == newUserWord.WordValue)
                            .Select(w => w.Id)
                            .FirstOrDefaultAsync();

                        // Check if the UserWord already exists for the user and word
                        bool exists = await dbContext.UserWords.AnyAsync(uw =>
                            uw.UserId == newUserWord.UserId && uw.WordId == wordId
                        );

                        if (exists)
                        {
                            return Results.Conflict("UserWord already exists.");
                        }

                        UserWord userWord = newUserWord.ToEntity(Guid.NewGuid().ToString(), wordId);
                        dbContext.UserWords.Add(userWord);
                        await dbContext.SaveChangesAsync();

                        return Results.CreatedAtRoute(
                            CreateUserWordEndpointName,
                            new { id = userWord.Id },
                            new
                            {
                                userWord.Id,
                                userWord.UserId,
                                userWord.WordId,
                                Word = (await dbContext.Words.FindAsync(wordId)).ToDto() // Ensure you have a ToDto() method in your Word class
                            }
                        );
                    }
                )
                .WithName(CreateUserWordEndpointName);

            // Endpoint to delete an existing UserWord
            group
                .MapDelete(
                    "/{userId}/{wordValue}",
                    async (string userId, string wordValue, upwordContext dbContext) =>
                    {
                        // Validate userId and wordValue
                        var userExists = await dbContext.Users.AnyAsync(u => u.Id == userId);
                        var wordExists = await dbContext.Words.AnyAsync(w => w.Value == wordValue);

                        if (!userExists)
                        {
                            return Results.NotFound("User not found.");
                        }

                        if (!wordExists)
                        {
                            return Results.NotFound("Word not found.");
                        }

                        var wordId = await dbContext
                            .Words.Where(w => w.Value == wordValue)
                            .Select(w => w.Id)
                            .FirstOrDefaultAsync();

                        var userWord = await dbContext.UserWords.FirstOrDefaultAsync(uw =>
                            uw.UserId == userId && uw.WordId == wordId
                        );

                        if (userWord == null)
                        {
                            return Results.NotFound("UserWord not found.");
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
