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
    [Migration("20240716202246_UpdateWordSchema")]
    partial class UpdateWordSchema
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
                });
#pragma warning restore 612, 618
        }
    }
}
