using Microsoft.EntityFrameworkCore;
using InteraCoop.Shared.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Metadata;
using Microsoft.Extensions.Hosting;

namespace InteraCoop.Backend.Data  
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<City> Cities { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Campaign>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Interaction>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Opportunity>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Country>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<City>().HasIndex(x => new { x.StateId, x.Name }).IsUnique();
            modelBuilder.Entity<State>().HasIndex(x => new { x.CountryId, x.Name }).IsUnique();
            modelBuilder.Entity<Client>().HasIndex(x => x.Document).IsUnique();
            modelBuilder.Entity<User>().HasIndex(x => x.Document).IsUnique();
            modelBuilder.Entity<User>().HasAlternateKey(x => x.Document);
            modelBuilder.Entity<Opportunity>()
                .HasOne(e => e.Campaign)
                .WithMany()
                .HasForeignKey(e => e.CampaignId)
                .IsRequired();
            modelBuilder.Entity<Opportunity>()
                .HasOne(e => e.Interaction)
                .WithOne();
            modelBuilder.Entity<Interaction>()
               .HasOne(i => i.Client)
               .WithMany()
               .HasForeignKey(i => i.ClientId)
               .IsRequired();
            modelBuilder.Entity<Interaction>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserDocument)
                .HasPrincipalKey(x => x.Document)
                .IsRequired();
            DisableCascadingDelete(modelBuilder);
        }

        private void DisableCascadingDelete(ModelBuilder modelBuilder)
        {
            var relationships = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
            foreach (var relationship in relationships)
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
