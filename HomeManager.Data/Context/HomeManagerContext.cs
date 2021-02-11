using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using HomeManager.Models;
using Type = HomeManager.Models.Type;
using HomeManager.Data.Context;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Linq;

//Add-Migration HomeManager.PutNameHere -Context HomeManagerContext -OutputDir "Migrations"

namespace HomeManager.Data
{
    public class HomeManagerContext : IdentityDbContext<User, Role, Guid>
    {
        public HomeManagerContext(DbContextOptions<HomeManagerContext> options) : base(options)
        {
        }

        public DbSet<Payments> Payments { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Payment_Template> Payment_Templates { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Ingredient> Ingredients{  get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Favorites> Favorites { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
       .SelectMany(t => t.GetForeignKeys())
       .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<Type>()
            .Property(e => e.ExtraInput)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Payments>()
            .Property(e => e.Description_Tax)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Payments>()
            .Property(e => e.Amount_TaxList)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Payments>()
            .Property(e => e.TaxList)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Recipe>()
            .Property(e => e.Instructions)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Payments>().ToTable("Payments");
            modelBuilder.Entity<Type>().ToTable("Types");
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Status>().ToTable("Statuses");
            modelBuilder.Entity<Payment_Template>().ToTable("Payment_Templates");
            modelBuilder.Entity<Tag>().ToTable("Tags");
            modelBuilder.Entity<Ingredient>().ToTable("Ingredients");
            modelBuilder.Entity<Recipe>().ToTable("Recipes");

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}
