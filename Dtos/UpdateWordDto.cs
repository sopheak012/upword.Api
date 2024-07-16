using System.ComponentModel.DataAnnotations;

namespace upword.Api.Dtos;

public record class UpdateWordDto(
    string Id,
    string Value,
    [StringLength(1000)] string? Definition,
    [StringLength(50)] string? PartOfSpeech,
    [StringLength(100)] string? Pronunciation,
    [Required, MinLength(1)] string[] ExampleSentences, // Array for multiple example sentences
    DateOnly DateAdded
);
