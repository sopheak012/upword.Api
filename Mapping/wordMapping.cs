using System;
using upword.Api.Dtos;
using upword.Api.Entities;

namespace upword.Api.Mapping
{
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
                DateAdded = GetPacificStandardTimeDate()
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

        private static DateOnly GetPacificStandardTimeDate()
        {
            // Define Pacific Standard Time (PST) timezone
            TimeZoneInfo pacificZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");

            // Convert current UTC time to PST
            DateTime pacificTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, pacificZone);

            // Return only the date part
            return DateOnly.FromDateTime(pacificTime);
        }
    }
}
