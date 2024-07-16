using upword.Api.Dtos;
using upword.Api.Entities;

namespace upword.Api.Mapping;

public static class WordMapping
{
    public static Word ToEntity(this CreateWordDto wordDto, string newId)
    {
        return new Word
        {
            Id = newId,
            Value = wordDto.Value, // Changed from 'word' to 'Value'
            Definition = wordDto.Definition,
            PartOfSpeech = wordDto.PartOfSpeech,
            Pronunciation = wordDto.Pronunciation,
            ExampleSentences = wordDto.ExampleSentences, // Change to array of strings
            DateAdded = DateOnly.FromDateTime(DateTime.UtcNow)
        };
    }

    public static WordDto ToDto(this Word word) // Change to ToDto
    {
        return new WordDto(
            word.Id,
            word.Value,
            word.Definition,
            word.PartOfSpeech,
            word.Pronunciation,
            word.ExampleSentences,
            word.DateAdded
        );
    }

    public static Word ToEntity(this UpdateWordDto wordDto, string id)
    {
        return new Word
        {
            Id = id,
            Value = wordDto.Value,
            Definition = wordDto.Definition,
            PartOfSpeech = wordDto.PartOfSpeech,
            Pronunciation = wordDto.Pronunciation,
            ExampleSentences = wordDto.ExampleSentences,
            DateAdded = wordDto.DateAdded
        };
    }
}
