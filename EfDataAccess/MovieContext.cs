using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccess
{
    public class MovieContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=movie-db;Integrated Security=True");
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(p => p.Title).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Id).IsRequired().HasMaxLength(128);
                entity.Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(p => p.IsDeleted).HasDefaultValue(false);
                entity.HasMany(p => p.MovieGenres)
                    .WithOne(p => p.Movie)
                    .HasForeignKey(p => p.MovieId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<MovieGenre>(entity => 
            {
                entity.HasKey(p => new { p.MovieId, p.GenreId });
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(p => p.Id).IsRequired().HasMaxLength(128);
                entity.HasIndex(p => p.Email);
                entity.Property(p => p.Firstname).IsRequired().HasMaxLength(50);
                entity.Property(p => p.Lastname).IsRequired().HasMaxLength(50);
                entity.Property(p => p.Email).IsRequired().HasMaxLength(50);
                entity.Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()");
            });
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(e => e.Movie)
                    .WithMany(m => m.Comments)
                    .HasForeignKey(e => e.MovieId);
                entity.HasOne(p => p.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(p => p.UserId);
                entity.Property(p => p.Id).IsRequired().HasMaxLength(128);
                entity.Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(p => p.IsDeleted).HasDefaultValue(false);
            });
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasMany(p => p.MovieGenres)
                    .WithOne(p => p.Genre)
                    .HasForeignKey(p => p.GenreId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.Property(p => p.Id).IsRequired().HasMaxLength(128);
                entity.Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(p => p.Name).IsRequired().HasMaxLength(50);
            });
        }
    }
}
