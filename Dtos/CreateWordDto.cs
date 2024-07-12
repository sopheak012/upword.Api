using System.ComponentModel.DataAnnotations;

namespace upword.Api.Dtos;

public record class CreateWordDto(
    [Required] string word,
    [StringLength(1000)] string definition,
    [StringLength(50)] string partOfSpeech,
    [StringLength(100)] string pronunciation,
    [StringLength(500)] string exampleSentence
);
