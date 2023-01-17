using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(buildAction =>
            {
                buildAction.ToTable("Products").HasKey(p => p.Id);
                buildAction.Property(p => p.Id).HasColumnName("Id");
                buildAction.Property(p => p.Name).HasColumnName("Name");
                buildAction.Property(p => p.Price).HasColumnName("Price");
                buildAction.Property(p => p.Stock).HasColumnName("Stock");
                buildAction.Property(p => p.Description).HasColumnName("Description");
                buildAction.HasOne(p => p.Category);
            });

            modelBuilder.Entity<Category>(buildAction =>
            {
                buildAction.ToTable("Categories").HasKey(c => c.Id);
                buildAction.Property(c => c.Id).HasColumnName("Id");
                buildAction.Property(c => c.Name).HasColumnName("Name");
                buildAction.HasMany(c => c.Products);
            });
        }
    }
}

