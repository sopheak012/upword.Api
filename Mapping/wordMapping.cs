using upword.Api.Dtos;
using upword.Api.Entities;

namespace upword.Api.Mapping;

public static class WordMapping
{
    public static Word ToEntity(this CreateWordDto word, string newId)
    {
        return new Word
        {
            Id = newId,
            Value = word.word,
            Definition = word.definition,
            PartOfSpeech = word.partOfSpeech,
            Pronunciation = word.pronunciation,
            ExampleSentence = word.exampleSentence,
            DateAdded = DateOnly.FromDateTime(DateTime.UtcNow)
        };
    }

    public static WordDto toDto(this Word word)
    {
        return new WordDto(
            word.Id,
            word.Value,
            word.Definition,
            word.PartOfSpeech,
            word.Pronunciation,
            word.ExampleSentence,
            word.DateAdded
        );
    }

    public static Word ToEntity(this UpdateWordDto word, string id)
    {
        return new Word
        {
            Id = id,
            Value = word.word,
            Definition = word.definition,
            PartOfSpeech = word.partOfSpeech,
            Pronunciation = word.pronunciation,
            ExampleSentence = word.exampleSentence,
            DateAdded = DateOnly.FromDateTime(DateTime.UtcNow) // Generate the date here
        };
    }
}
