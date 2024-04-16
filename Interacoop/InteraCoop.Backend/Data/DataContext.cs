using InteraCoop.Shared.Entities;
using Microsoft.EntityFrameworkCore;
namespace InteraCoop.Backend.Data
{
    public class DataContext : DbContext 
    {        

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
