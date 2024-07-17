using Microsoft.EntityFrameworkCore;
using upword.Api.Entities;

namespace upword.Api.Data
{
    public class upwordContext : DbContext
    {
        public upwordContext(DbContextOptions<upwordContext> options)
            : base(options) { }

        public DbSet<Word> Words => Set<Word>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ensure the 'Value' (word) column is unique
            modelBuilder.Entity<Word>().HasIndex(w => w.Value).IsUnique();

            // Specify that ExampleSentences is stored as JSON
            modelBuilder.Entity<Word>().Property(w => w.ExampleSentences).HasColumnType("TEXT"); // Store as TEXT in SQLite

            // Add a manual entry if the database is empty
            modelBuilder
                .Entity<Word>()
                .HasData(
                    new Word
                    {
                        Id = "1",
                        Value = "Serendipity",
                        Definition =
                            "The occurrence and development of events by chance in a happy or beneficial way.",
                        PartOfSpeech = "Noun",
                        Pronunciation = "/ˌserənˈdipədi/",
                        ExampleSentences = new[] { "Example 1", "Example 2" },
                        DateAdded = new DateOnly(2024, 7, 16) // Set to yesterday
                    }
                );
        }
    }
}
