using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using HomeManager.Models;
using Type = HomeManager.Models.Type;

namespace HomeManager.Data
{
    public class HomeManagerContext : DbContext
    {
        public HomeManagerContext(DbContextOptions<HomeManagerContext> options) : base(options)
        {
        }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Payment_Template> Payment_Templates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>().ToTable("Payment");
            modelBuilder.Entity<Type>().ToTable("Type");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Status>().ToTable("Status");
            modelBuilder.Entity<Payment_Template>().ToTable("Payment_Template");
        }
    }
}
