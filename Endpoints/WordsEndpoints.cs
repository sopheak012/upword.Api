using upword.Api.Dtos;

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
            (CreateWordDto newWord) =>
            {
                // Find the maximum id in existing words
                int maxId = words.Max(word => int.Parse(word.id));

                // Generate new id by incrementing the maximum id
                string newId = (maxId + 1).ToString();

                // Create a new WordDto instance from the CreateWordDto
                WordDto word = new WordDto(
                    newId,
                    newWord.word,
                    newWord.definition,
                    newWord.partOfSpeech,
                    newWord.pronunciation,
                    newWord.exampleSentence,
                    DateOnly.FromDateTime(DateTime.Today) // Convert DateTime.Today to DateOnly
                );

                // Add the new WordDto to the words list
                words.Add(word);

                // Return a response indicating success and the location of the new resource
                return Results.CreatedAtRoute(GetWordEndpointName, new { id = word.id }, word);
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
