using System;
using BusinessLayer;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class FlashcardsDbContext : DbContext
    {
        public FlashcardsDbContext() : base()
        {
        }

        public FlashcardsDbContext(DbContextOptions<FlashcardsDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=FlashcardsDb;Username=postgres;Password=11");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Flashcard> Flashcards { get; set; }
    }
}
