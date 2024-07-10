using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using upword.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetWordEndpointName = "GetWord";
List<WordDto> words = new List<WordDto>
{
    new WordDto(
        "1",
        "Apple",
        "A round fruit with red or green skin and a whitish interior.",
        "Noun",
        "/ˈæpəl/",
        "She took a bite of the juicy apple.",
        new DateOnly(2024, 7, 8) // Use DateOnly to represent date without time
    ),
    new WordDto(
        "2",
        "Book",
        "A set of written or printed pages, usually bound with a protective cover.",
        "Noun",
        "/bʊk/",
        "He bought a new book at the bookstore.",
        new DateOnly(2024, 7, 9) // Use DateOnly to represent date without time
    ),
    new WordDto(
        "3",
        "Run",
        "To move swiftly on foot so that both feet leave the ground during each stride.",
        "Verb",
        "/rʌn/",
        "She likes to run in the park every morning.",
        new DateOnly(2024, 7, 10) // Use DateOnly to represent date without time
    )
};

// Endpoint to get all words
app.MapGet("words", () => words);

// Endpoint to get a word by id
app.MapGet("words/{id}", (string id) => words.Find(word => word.id == id))
    .WithName(GetWordEndpointName);

// Endpoint to create a new word
app.MapPost(
    "games",
    (CreateWordDto newWord) =>
    {
        // Find the maximum id in existing words
        int maxId = words.Max(word => int.Parse(word.id));

        // Generate new id by incrementing the maximum id
        string newId = (maxId + 1).ToString();

        // Create a new WordDto instance from the CreateWordDto
        WordDto word = new WordDto(
            newId,
            newWord.word,
            newWord.definition,
            newWord.partOfSpeech,
            newWord.pronunciation,
            newWord.exampleSentence,
            DateOnly.FromDateTime(DateTime.Today) // Convert DateTime.Today to DateOnly
        );

        // Add the new WordDto to the words list
        words.Add(word);

        // Return a response indicating success and the location of the new resource
        return Results.CreatedAtRoute(GetWordEndpointName, new { id = word.id }, word);
    }
);

app.Run();
