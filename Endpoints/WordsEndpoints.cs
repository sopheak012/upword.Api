using Microsoft.EntityFrameworkCore;
using upword.Api.Data;
using upword.Api.Dtos;
using upword.Api.Entities;
using upword.Api.Mapping;

namespace upword.Api.Endpoints;

public static class WordsEndpoints
{
    const string GetWordEndpointName = "GetWord";

    const string GetWordOfTheDayEndpointName = "GetWordOfTheDay";

    public static RouteGroupBuilder MapWordsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("words").WithParameterValidation();

        // Endpoint to get all words
        group.MapGet(
            "/",
            async (upwordContext dbContext) =>
            {
                var words = await dbContext.Words.ToListAsync();
                return Results.Ok(words.Select(w => w.ToDto()));
            }
        );

        // Endpoint to get a word by its value
        group
            .MapGet(
                "/{value}",
                async (string value, upwordContext dbContext) =>
                {
                    var word = await dbContext
                        .Words.Where(w => w.Value.ToLower() == value.ToLower())
                        .FirstOrDefaultAsync();

                    if (word == null)
                        return Results.NotFound("Word not found.");

                    return Results.Ok(word.ToDto());
                }
            )
            .WithName(GetWordEndpointName);

        // Endpoint to create a new word
        group.MapPost(
            "/",
            async (CreateWordDto newWord, upwordContext dbContext) =>
            {
                // Check if the Value already exists (case-insensitive)
                if (
                    await dbContext.Words.AnyAsync(w =>
                        w.Value.ToLower() == newWord.Value.ToLower()
                    )
                )
                {
                    return Results.Conflict("A word with this value already exists.");
                }

                // Generate new ID
                var highestId = dbContext.Words.Any()
                    ? dbContext.Words.Max(w => int.Parse(w.Id))
                    : 0;
                string newId = (highestId + 1).ToString();

                Word word = newWord.ToEntity(newId);
                dbContext.Words.Add(word);
                await dbContext.SaveChangesAsync();

                return Results.CreatedAtRoute(
                    GetWordEndpointName,
                    new { value = word.Value },
                    word.ToDto()
                );
            }
        );

        // Endpoint to update an existing word by its value
        group.MapPut(
            "/{value}",
            async (string value, UpdateWordDto updatedWord, upwordContext dbContext) =>
            {
                var existingWord = await dbContext
                    .Words.Where(w => w.Value.ToLower() == value.ToLower())
                    .FirstOrDefaultAsync();

                if (existingWord == null)
                {
                    return Results.NotFound("Word not found.");
                }

                // Update existing word properties
                existingWord.Value = updatedWord.Value;
                existingWord.Definition = updatedWord.Definition;
                existingWord.PartOfSpeech = updatedWord.PartOfSpeech;
                existingWord.Pronunciation = updatedWord.Pronunciation;
                existingWord.ExampleSentences = updatedWord.ExampleSentences;
                existingWord.DateAdded = updatedWord.DateAdded; // Ensure date is provided

                await dbContext.SaveChangesAsync();

                return Results.Ok(existingWord.ToDto());
            }
        );

        // Endpoint to delete an existing word by its value
        group.MapDelete(
            "/{value}",
            async (string value, upwordContext dbContext) =>
            {
                var word = await dbContext
                    .Words.Where(w => w.Value.ToLower() == value.ToLower())
                    .FirstOrDefaultAsync();

                if (word == null)
                {
                    return Results.NotFound("Word not found.");
                }

                dbContext.Words.Remove(word);
                await dbContext.SaveChangesAsync();

                return Results.Ok(word.ToDto());
            }
        );

        // Endpoint to get the word of the day
        group
            .MapGet(
                "/wordoftheday",
                async (upwordContext dbContext) =>
                {
                    var today = DateOnly.FromDateTime(DateTime.UtcNow.Date); // Use Date to ignore time part

                    var wordOfTheDay = await dbContext
                        .Words.Where(w => w.DateAdded == today)
                        .FirstOrDefaultAsync();

                    if (wordOfTheDay == null)
                    {
                        return Results.NotFound("No word found for today.");
                    }

                    // Convert the retrieved Word entity to DTO before returning
                    var wordDto = wordOfTheDay.ToDto();
                    return Results.Ok(wordDto);
                }
            )
            .WithName(GetWordOfTheDayEndpointName);

        return group;
    }
}
