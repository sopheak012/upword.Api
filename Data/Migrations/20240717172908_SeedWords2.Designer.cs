﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using upword.Api.Data;

#nullable disable

namespace upword.Api.Data.Migrations
{
    [DbContext(typeof(upwordContext))]
    [Migration("20240717172908_SeedWords2")]
    partial class SeedWords2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("upword.Api.Entities.Word", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("DateAdded")
                        .HasColumnType("TEXT");

                    b.Property<string>("Definition")
                        .HasColumnType("TEXT");

                    b.Property<string>("ExampleSentences")
                        .HasColumnType("TEXT");

                    b.Property<string>("PartOfSpeech")
                        .HasColumnType("TEXT");

                    b.Property<string>("Pronunciation")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Value")
                        .IsUnique();

                    b.ToTable("Words");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            DateAdded = new DateOnly(2024, 7, 16),
                            Definition = "The occurrence and development of events by chance in a happy or beneficial way.",
                            ExampleSentences = "[\"Example 1\",\"Example 2\"]",
                            PartOfSpeech = "Noun",
                            Pronunciation = "/ˌserənˈdipədi/",
                            Value = "Serendipity"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
