using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using upword.Api.Data; // Ensure this is included
using upword.Api.Dtos;
using upword.Api.Entities;

namespace upword.Api.Services
{
    public class WordService
    {
        private readonly List<Word> _words;
        private readonly upwordContext _dbContext; // Inject your DbContext

        public WordService(string filePath, upwordContext dbContext)
        {
            _words = LoadWordsFromFile(filePath);
            _dbContext = dbContext;
        }

        private List<Word> LoadWordsFromFile(string filePath)
        {
            try
            {
                var jsonString = File.ReadAllText(filePath);
                var wordDtos = JsonSerializer.Deserialize<List<WordDto>>(
                    jsonString,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                return wordDtos
                        ?.Select(dto => new Word
                        {
                            Id = dto.Id, // Assuming Id should be populated from DTO
                            Value = dto.Value,
                            Definition = dto.Definition,
                            PartOfSpeech = dto.PartOfSpeech,
                            Pronunciation = dto.Pronunciation,
                            ExampleSentences = dto.ExampleSentences,
                            DateAdded = dto.DateAdded
                        })
                        .ToList() ?? new List<Word>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
                return new List<Word>();
            }
        }

        public List<Word> GetWords()
        {
            return _words;
        }

        public Word GetRandomWord()
        {
            var random = new Random();
            int index = random.Next(_words.Count);
            return _words[index];
        }

        public async Task<bool> AddUniqueWordAsync()
        {
            var word = GetRandomWord();

            try
            {
                // Add the word to the database
                _dbContext.Words.Add(word);
                await _dbContext.SaveChangesAsync();
                return true; // Word added successfully
            }
            catch (DbUpdateException ex)
            {
                // Handle the case where the word already exists
                if (ex.InnerException is DbUpdateException)
                {
                    return false; // Word is a duplicate
                }

                throw; // Re-throw other exceptions
            }
        }
    }
}
