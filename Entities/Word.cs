using System;

namespace upword.Api.Entities;

public class Word
{
    public required string Id { get; set; }
    public required string Value { get; set; }
    public string? Definition { get; set; }
    public string? PartOfSpeech { get; set; }
    public string? Pronunciation { get; set; }
    public string? ExampleSentence { get; set; }
    public DateOnly DateAdded { get; set; }
}
