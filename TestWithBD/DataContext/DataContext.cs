using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using TestWithBD.Models;
using TestWithBD;

namespace TestWithBD
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }
        public DataContext(DbContextOptions options) : base(options)
        {
            if (!(this.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
            {
                Database.EnsureCreated();
            }
        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var productRootGuid = Guid.NewGuid();
            var categoryRootGuid = Guid.NewGuid();

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = productRootGuid,
                Name = "admin",
                CategoryId = categoryRootGuid,
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = categoryRootGuid,
                Name = "admin",
            });

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TestWithDB;Username=sa;Password=sa");
        }
    }
}
