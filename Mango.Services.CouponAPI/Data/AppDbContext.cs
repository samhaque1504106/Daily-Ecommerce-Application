using Mango.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace Mango.Services.CouponAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)  //t passes those options to the base DbContext class using base(options).
        {
            //might include things like the connection string and DB provider (e.g., SQL Server, PostgreSQL, etc.).
        }

        public DbSet<Coupon> Coupons { get; set; }  // creates table of Coupon model

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 1,
                CouponCode = "10OFF",
                DiscountAmount = 10,
                MinAmount = 20,
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 2,
                CouponCode = "20OFF",
                DiscountAmount = 20,
                MinAmount = 40,
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 3,
                CouponCode = "30OFF",
                DiscountAmount = 30,
                MinAmount = 60,
            });
        }

    }
}
