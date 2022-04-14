using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCmasr.Models;

namespace MVCmasr.Context
{
    public class ApplicationDbContext : IdentityDbContext< IdentityUser , IdentityRole  , string >
    {
        public ApplicationDbContext() : base()
        {

        }
        
        public ApplicationDbContext( DbContextOptions options  ) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SongArtist>(e => e.HasKey(el => new { el.ArtistId, el.SongId }));

            base.OnModelCreating(builder);
        }


        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists  { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<SongArtist> SongArtists { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Genre> Genre { get; set; }


    }
}
