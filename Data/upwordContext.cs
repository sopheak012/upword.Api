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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Existing Word configuration
            modelBuilder.Entity<Word>().HasIndex(w => w.Value).IsUnique();
            modelBuilder.Entity<Word>().Property(w => w.ExampleSentences).HasColumnType("TEXT");

            // ApplicationUser configuration
            modelBuilder.Entity<ApplicationUser>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<ApplicationUser>().Property(u => u.Email).IsRequired();
            modelBuilder.Entity<ApplicationUser>().Property(u => u.FirstName).HasMaxLength(50);
            modelBuilder.Entity<ApplicationUser>().Property(u => u.LastName).HasMaxLength(50);

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
                        ExampleSentences = new[] { "Example 1", "Example 2" },
                        DateAdded = new DateOnly(2024, 7, 16)
                    }
                );
        }
    }
}
