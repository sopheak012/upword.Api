using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using upword.Api.Entities;

namespace upword.Api.Data
{
    public class upwordContext : IdentityDbContext<ApplicationUser>
    {
        public upwordContext(DbContextOptions<upwordContext> options)
            : base(options) { }

        public DbSet<Word> Words => Set<Word>();
        public DbSet<UserWord> UserWords => Set<UserWord>(); // Add this line

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Existing Word configuration
            modelBuilder.Entity<Word>().HasIndex(w => w.Value).IsUnique();
            modelBuilder.Entity<Word>().Property(w => w.ExampleSentences).HasColumnType("TEXT");

            // ApplicationUser configuration
            modelBuilder.Entity<ApplicationUser>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<ApplicationUser>().Property(u => u.Email).IsRequired();
            // Removed FirstName and LastName configurations

            // Existing Word seed data
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
                        ExampleSentences = new[] { "She found her old friend by sheer serendipity.", "Their serendipity led to a fruitful collaboration." },
                        DateAdded = new DateOnly(2024, 7, 16)
                    }
                );
        }
    }
}
