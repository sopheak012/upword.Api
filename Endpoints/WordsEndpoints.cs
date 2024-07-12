using Microsoft.EntityFrameworkCore;
using upword.Api.Data;
using upword.Api.Dtos;
using upword.Api.Entities;
using upword.Api.Mapping;

namespace upword.Api.Endpoints;

public static class WordsEndpoints
{
    const string GetWordEndpointName = "GetWord";

    public static RouteGroupBuilder MapWordsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("words").WithParameterValidation();

        // Endpoint to get all words
        group.MapGet(
            "/",
            async (upwordContext dbContext) =>
            {
                var words = await dbContext.Words.ToListAsync();
                return words.Select(w => w.toDto());
            }
        );

        // Endpoint to get a word by id
        group
            .MapGet(
                "/{id}",
                async (string id, upwordContext dbContext) =>
                {
                    var word = await dbContext.Words.FindAsync(id);
                    if (word == null)
                        return Results.NotFound();

                    return Results.Ok(word.toDto());
                }
            )
            .WithName(GetWordEndpointName);

        // Endpoint to create a new word
        group.MapPost(
            "/",
            async (CreateWordDto newWord, upwordContext dbContext) =>
            {
                // Fetch all IDs
                var ids = await dbContext.Words.Select(w => w.Id).ToListAsync();

                // Parse and find max in memory
                var maxId = ids.Count > 0 ? ids.Max(id => int.Parse(id)) : 0;
                var newId = (maxId + 1).ToString();

                Word word = newWord.ToEntity();
                word.Id = newId;

                dbContext.Words.Add(word);
                await dbContext.SaveChangesAsync();

                return Results.CreatedAtRoute(
                    GetWordEndpointName,
                    new { id = word.Id },
                    word.toDto()
                );
            }
        );

        // Endpoint to update an existing word
        group.MapPut(
            "/{id}",
            async (string id, UpdateWordDto updatedWord, upwordContext dbContext) =>
            {
                // Find the word by id
                var existingWord = await dbContext.Words.FindAsync(id);
                if (existingWord == null)
                {
                    return Results.NotFound();
                }

                dbContext.Entry(existingWord).CurrentValues.SetValues(updatedWord.ToEntity(id));
                await dbContext.SaveChangesAsync();

                return Results.Ok(existingWord.toDto());
            }
        );

        // Endpoint to delete an existing word
        group.MapDelete(
            "/{id}",
            async (string id, upwordContext dbContext) =>
            {
                var word = await dbContext.Words.FindAsync(id);
                if (word == null)
                {
                    return Results.NotFound();
                }

                dbContext.Words.Remove(word);
                await dbContext.SaveChangesAsync();

                return Results.Ok(word.toDto());
            }
        );

        return group;
    }
}
