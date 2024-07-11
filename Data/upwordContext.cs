using Microsoft.EntityFrameworkCore;
using upword.Api.Entities;

namespace upword.Api.Data;

public class upwordContext : DbContext
{
    public upwordContext(DbContextOptions<upwordContext> options) : base(options)
    {
    }

    public DbSet<Word> Words => Set<Word>();

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Word>().HasData(
    //         new Word
    //         {
    //             Id = "1",
    //             Value = "Serendipity",
    //             Definition = "The occurrence and development of events by chance in a happy or beneficial way.",
    //             PartOfSpeech = "Noun",
    //             Pronunciation = "/ˌserənˈdipədē/",
    //             ExampleSentence = "The discovery of penicillin was a serendipity.",
    //             DateAdded = new DateOnly(2024, 7, 11)
    //         },
    //         new Word
    //         {
    //             Id = "2",
    //             Value = "Ephemeral",
    //             Definition = "Lasting for a very short time.",
    //             PartOfSpeech = "Adjective",
    //             Pronunciation = "/əˈfem(ə)rəl/",
    //             ExampleSentence = "The beauty of cherry blossoms is ephemeral, lasting only a few days.",
    //             DateAdded = new DateOnly(2024, 7, 11)
    //         },
    //         new Word
    //         {
    //             Id = "3",
    //             Value = "Ubiquitous",
    //             Definition = "Present, appearing, or found everywhere.",
    //             PartOfSpeech = "Adjective",
    //             Pronunciation = "/yo͞oˈbikwədəs/",
    //             ExampleSentence = "In the modern world, smartphones have become ubiquitous.",
    //             DateAdded = new DateOnly(2024, 7, 11)
    //         },
    //         new Word
    //         {
    //             Id = "4",
    //             Value = "Eloquent",
    //             Definition = "Fluent or persuasive in speaking or writing.",
    //             PartOfSpeech = "Adjective",
    //             Pronunciation = "/ˈeləkwənt/",
    //             ExampleSentence = "Her eloquent speech moved the entire audience.",
    //             DateAdded = new DateOnly(2024, 7, 11)
    //         },
    //         new Word
    //         {
    //             Id = "5",
    //             Value = "Perseverance",
    //             Definition = "Persistence in doing something despite difficulty or delay in achieving success.",
    //             PartOfSpeech = "Noun",
    //             Pronunciation = "/ˌpərsəˈvirəns/",
    //             ExampleSentence = "His perseverance in the face of adversity was truly inspiring.",
    //             DateAdded = new DateOnly(2024, 7, 11)
    //         }
    //     );
    // }

}