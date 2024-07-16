namespace upword.Api.Dtos;

public record class WordDto(
    string Id,
    string Value,
    string Definition,
    string PartOfSpeech,
    string Pronunciation,
    string[] ExampleSentences,
    DateOnly DateAdded
);
