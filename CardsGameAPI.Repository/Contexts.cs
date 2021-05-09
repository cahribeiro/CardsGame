using CardsGameAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace CardsGameAPI.Repository
{
    public partial class Contexts : DbContext
    {
        public Contexts()
        {
        }
        public Contexts(DbContextOptions<Contexts> options)
           : base(options)
        { }
        public DbSet<Room> Room { get; set; }
        public DbSet<Cards> Cards { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>(entity =>
            {

            });

            modelBuilder.Entity<Cards>(entity =>
            {

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
