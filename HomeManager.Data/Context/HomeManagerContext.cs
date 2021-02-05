using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using HomeManager.Models;
using Type = HomeManager.Models.Type;
using HomeManager.Data.Context;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

//Add-Migration HomeManager.PutNameHere -Context HomeManagerContext -OutputDir "Migrations"

namespace HomeManager.Data
{
    public class HomeManagerContext : IdentityDbContext<User, Role, Guid>
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
            modelBuilder.Entity<Payment>().ToTable("Payments");
            modelBuilder.Entity<Type>().ToTable("Types");
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Status>().ToTable("Statuses");
            modelBuilder.Entity<Payment_Template>().ToTable("Payment_Templates");

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}
