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

        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Genre> Genre { get; set; }

        //public DbSet<MVCmasr.ViewModels.AlbumGenreViewModel> AlbumGenreViewModel { get; set; }


    }
}
