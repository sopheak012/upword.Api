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
        }
    }
}
