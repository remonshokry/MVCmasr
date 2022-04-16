using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCmasr.Models;
using MVCmasr.ViewModels;

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

            builder.Entity<AlbumGenre>(e => e.HasKey(el => new { el.AlbumId, el.GenreId }));
            builder.Entity<AlbumGenre>()
                .HasOne(ag => ag.Album)
                .WithMany(g => g.AlbumGenre)
                .HasForeignKey(ag => ag.GenreId);
            builder.Entity<AlbumGenre>()
                .HasOne(ag => ag.Genre)
                .WithMany(a => a.AlbumGenre)
                .HasForeignKey(ag => ag.AlbumId);

            builder.Entity<AlbumGenreViewModel>(e => e.HasNoKey());

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
        public DbSet<AlbumGenre> AlbumGenres { get; set; }
        public DbSet<MVCmasr.ViewModels.AlbumGenreViewModel> AlbumGenreViewModel { get; set; }


    }
}
