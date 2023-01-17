using Core.Security.Entities;
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
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

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

            modelBuilder.Entity<OperationClaim>(buildAction =>
            {
                buildAction.ToTable("OperationClaims").HasKey(o => o.Id);
                buildAction.Property(o => o.Id).HasColumnName("Id");
                buildAction.Property(o => o.Name).HasColumnName("Name");

            });

            modelBuilder.Entity<User>(buildAction =>
            {
                buildAction.ToTable("Users").HasKey(u => u.Id);
                buildAction.Property(u => u.Id).HasColumnName("Id");
                buildAction.Property(u => u.FirstName).HasColumnName("FirstName");
                buildAction.Property(u => u.LastName).HasColumnName("LastName");
                buildAction.Property(u => u.Email).HasColumnName("Email");
                buildAction.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt");
                buildAction.Property(u => u.PasswordHash).HasColumnName("PasswordHash");
                buildAction.Property(u => u.Status).HasColumnName("Status");
                buildAction.Property(u => u.AuthenticatorType).HasColumnName("AuthenticatorType");
                buildAction.HasMany(u => u.UserOperationClaims);
            });

            modelBuilder.Entity<UserOperationClaim>(buildAction =>
            {
                buildAction.ToTable("UserOperationClaims").HasKey(u => u.Id);
                buildAction.Property(u => u.Id).HasColumnName("Id");
                buildAction.Property(u => u.UserId).HasColumnName("UserId");
                buildAction.Property(u => u.OperationClaimId).HasColumnName("OperationClaimId");
                buildAction.HasOne(u => u.User);
                buildAction.HasOne(u => u.OperationClaim);
            });
        }
    }
}

