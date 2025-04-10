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

    }
}
