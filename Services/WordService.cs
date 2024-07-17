using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using upword.Api.Data;
using upword.Api.Dtos;
using upword.Api.Entities;

namespace upword.Api.Services
{
    public class WordService
    {
        private readonly List<Word> _words;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public WordService(
            string filePath,
            IHttpClientFactory httpClientFactory,
            IServiceScopeFactory serviceScopeFactory
        )
        {
            Console.WriteLine("Initializing WordService...");
            _words = LoadWordsFromFile(filePath);
            _httpClientFactory = httpClientFactory;
            _serviceScopeFactory = serviceScopeFactory;
            Console.WriteLine($"Loaded {_words.Count} words from file.");
        }

        private List<Word> LoadWordsFromFile(string filePath)
        {
            try
            {
                Console.WriteLine($"Loading words from file: {filePath}");
                var jsonString = File.ReadAllText(filePath);
                var wordDtos = JsonSerializer.Deserialize<List<WordDto>>(
                    jsonString,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                return wordDtos
                        ?.Select(dto => new Word
                        {
                            Id = dto.Id,
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
            Console.WriteLine($"Returning {_words.Count} words.");
            return _words;
        }

        public Word GetRandomWord()
        {
            Console.WriteLine("Getting a random word...");
            var random = new Random();
            var validWords = _words.Where(w => !string.IsNullOrWhiteSpace(w.Value)).ToList();

            if (validWords.Count == 0)
            {
                Console.WriteLine("No valid words found.");
                return null;
            }

            int index = random.Next(validWords.Count);
            var selectedWord = validWords[index];
            Console.WriteLine($"Selected word: {selectedWord.Value}");
            return selectedWord;
        }

        public async Task<bool> AddUniqueWordAsync()
        {
            Console.WriteLine("Attempting to add a unique word...");
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<upwordContext>();

                // Check if any word has been added today
                bool wordAddedToday = await dbContext.Words.AnyAsync(w => w.DateAdded == today);

                if (wordAddedToday)
                {
                    Console.WriteLine($"A word has already been added for today ({today}).");
                    return false; // A word has already been added today
                }

                // If no word has been added today, proceed to add a new word
                var word = GetRandomWord();
                if (word == null || string.IsNullOrWhiteSpace(word.Value))
                {
                    Console.WriteLine("No valid word found.");
                    return false; // No valid word found
                }

                Console.WriteLine($"Attempting to add word: {word.Value} for date: {today}");

                // Get the highest ID and generate a new one
                var highestId = await dbContext.Words.Select(w => w.Id).ToListAsync();
                int newId = highestId.Any() ? highestId.Max(id => int.Parse(id)) + 1 : 1;
                word.Id = newId.ToString(); // Set the generated ID
                word.DateAdded = today; // Ensure the date added is set

                // Add the word to the database
                dbContext.Words.Add(word);
                await dbContext.SaveChangesAsync();
                Console.WriteLine(
                    $"Word '{word.Value}' added successfully with ID '{word.Id}' for date '{today}'."
                );
                return true; // Successfully added
            }
        }
    }
}
