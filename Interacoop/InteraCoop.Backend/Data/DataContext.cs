using InteraCoop.Shared.Entities;
using Microsoft.EntityFrameworkCore;
namespace InteraCoop.Backend.Data
{
    public class DataContext : DbContext 
    {        

        public DataContext(DbContextOptions<DataContext> options) : base(options) { 
        }

        public DbSet<Interaction> Interactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Interaction>().HasIndex(x => x.UserCode).IsUnique(); //Indice por el codigo del usuario
        }

    }
}
