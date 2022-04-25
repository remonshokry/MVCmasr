using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCmasr.Models;
using MVCmasr.ViewModels;
using MVCmasr.ViewModels.Order;

namespace MVCmasr.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole  , string >
    {

        #region CTORs
        public ApplicationDbContext() : base()
        {

        }
        
        public ApplicationDbContext( DbContextOptions options  ) : base(options)
        {

        }
        #endregion

        #region DBsets
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Genre> Genre { get; set; }

        public DbSet <OrderItemsSession> OrderItemsSessions { get; set; }
        #endregion


        #region Override
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<OrderItemsSession>().HasNoKey();

            builder.Entity<RegisterUserViewModel>().HasNoKey();

            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UsersClaims");
        }
        #endregion

    }
}
