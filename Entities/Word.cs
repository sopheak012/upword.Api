using System;

namespace upword.Api.Entities;

public class Word
{
    public string Id { get; set; }
    public required string Value { get; set; }
    public string? Definition { get; set; }
    public string? PartOfSpeech { get; set; }
    public string? Pronunciation { get; set; }
    public string[]? ExampleSentences { get; set; } // Change to string array for multiple sentences
    public DateOnly DateAdded { get; set; }
}
