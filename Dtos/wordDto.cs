namespace upword.Api.Dtos;
public record class WordDto(
    string id,
    string word,
    string definition,
    string partOfSpeech,
    string pronunciation,
    string exampleSentence,
    DateOnly dateAdded
);
