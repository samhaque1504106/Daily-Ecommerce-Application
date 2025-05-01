using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace Mango.Services.ShoppingCartAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)  //t passes those options to the base DbContext class using base(options).
        {
            //might include things like the connection string and DB provider (e.g., SQL Server, PostgreSQL, etc.).
        }

        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }



    }
}
