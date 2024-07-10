namespace upword.Api.Dtos;

public record class CreateWordDto(
    string id,
    string word,
    string definition,
    string partOfSpeech,
    string pronunciation,
    string exampleSentence,
    DateOnly dateAdded
);
