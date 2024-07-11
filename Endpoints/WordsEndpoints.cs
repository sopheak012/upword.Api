using Microsoft.EntityFrameworkCore;
using upword.Api.Data;
using upword.Api.Dtos;
using upword.Api.Entities;

namespace upword.Api.Endpoints;

public static class WordsEndpoints
{
    const string GetWordEndpointName = "GetWord";

    private static readonly List<WordDto> words = new List<WordDto>
    {
        new WordDto(
            "1",
            "Apple",
            "A round fruit with red or green skin and a whitish interior.",
            "Noun",
            "/ˈæpəl/",
            "She took a bite of the juicy apple.",
            new DateOnly(2024, 7, 8)
        ),
        new WordDto(
            "2",
            "Book",
            "A set of written or printed pages, usually bound with a protective cover.",
            "Noun",
            "/bʊk/",
            "He bought a new book at the bookstore.",
            new DateOnly(2024, 7, 9)
        ),
        new WordDto(
            "3",
            "Run",
            "To move swiftly on foot so that both feet leave the ground during each stride.",
            "Verb",
            "/rʌn/",
            "She likes to run in the park every morning.",
            new DateOnly(2024, 7, 10)
        )
    };

    public static RouteGroupBuilder MapWordsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("words").WithParameterValidation();
        // Endpoint to get all words
        group.MapGet("/", () => words);

        // Endpoint to get a word by id
        group
            .MapGet("/{id}", (string id) => words.Find(word => word.id == id))
            .WithName(GetWordEndpointName);

        // Endpoint to create a new word

        group.MapPost(
    "/",
    async (CreateWordDto newWord, upwordContext dbContext) =>
    {
        // Fetch all IDs and find the maximum on the client side
        var ids = await dbContext.Words.Select(w => w.Id).ToListAsync();
        var maxId = ids.Count > 0 ? ids.Max(id => int.Parse(id)) : 0;

        // Generate new id by incrementing the maximum id
        var newId = (maxId + 1).ToString();

        // Create a new Word entity from the CreateWordDto
        var word = new Word
        {
            Id = newId,
            Value = newWord.word,
            Definition = newWord.definition,
            PartOfSpeech = newWord.partOfSpeech,
            Pronunciation = newWord.pronunciation,
            ExampleSentence = newWord.exampleSentence,
            DateAdded = newWord.dateAdded != default ? newWord.dateAdded : DateOnly.FromDateTime(DateTime.UtcNow)
        };

        // Add the new Word to the context
        dbContext.Words.Add(word);

        // Save changes to the database
        await dbContext.SaveChangesAsync();

        // Create a WordDto from the saved Word entity
        var wordDto = new WordDto(
            word.Id,
            word.Value,
            word.Definition,
            word.PartOfSpeech,
            word.Pronunciation,
            word.ExampleSentence,
            word.DateAdded
        );

        // Return a response indicating success and the location of the new resource
        return Results.CreatedAtRoute(GetWordEndpointName, new { id = wordDto.id }, wordDto);
    }
);
        // Endpoint to update an existing word
        group.MapPut(
            "/{id}",
            (string id, CreateWordDto updatedWord) =>
            {
                // Find the word by id
                var existingWord = words.Find(word => word.id == id);
                if (existingWord == null)
                {
                    return Results.NotFound();
                }

                // Create a new WordDto instance with the updated values
                WordDto updatedWordDto = new WordDto(
                    existingWord.id,
                    updatedWord.word,
                    updatedWord.definition,
                    updatedWord.partOfSpeech,
                    updatedWord.pronunciation,
                    updatedWord.exampleSentence,
                    DateOnly.FromDateTime(DateTime.Today)
                );

                // Replace the old WordDto with the new one
                int index = words.IndexOf(existingWord);
                words[index] = updatedWordDto;

                return Results.Ok(updatedWordDto);
            }
        );

        // Endpoint to delete an existing word
        group.MapDelete(
            "/{id}",
            (string id) =>
            {
                // Find the word by id
                var word = words.Find(word => word.id == id);
                if (word == null)
                {
                    return Results.NotFound();
                }

                // Remove the word from the list
                words.Remove(word);

                return Results.NoContent();
            }
        );
        return group;
    }
}
