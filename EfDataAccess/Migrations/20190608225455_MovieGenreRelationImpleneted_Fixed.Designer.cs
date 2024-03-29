﻿// <auto-generated />
using System;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EfDataAccess.Migrations
{
    [DbContext(typeof(MovieContext))]
    [Migration("20190608225455_MovieGenreRelationImpleneted_Fixed")]
    partial class MovieGenreRelationImpleneted_Fixed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Comment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("ModifiedAt");

                    b.Property<string>("MovieId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Domain.Genre", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedAt");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("Domain.Movie", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<double>("ImdbRating");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("ModifiedAt");

                    b.Property<string>("Plot");

                    b.Property<DateTime>("Released");

                    b.Property<int>("Runtime");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Domain.MovieGenre", b =>
                {
                    b.Property<string>("MovieId");

                    b.Property<string>("GenreId");

                    b.HasKey("MovieId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("MovieGenre");
                });

            modelBuilder.Entity("Domain.Comment", b =>
                {
                    b.HasOne("Domain.Movie", "Movie")
                        .WithMany("Comments")
                        .HasForeignKey("MovieId");
                });

            modelBuilder.Entity("Domain.MovieGenre", b =>
                {
                    b.HasOne("Domain.Genre", "Genre")
                        .WithMany("MovieGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Movie", "Movie")
                        .WithMany("MovieGenres")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
