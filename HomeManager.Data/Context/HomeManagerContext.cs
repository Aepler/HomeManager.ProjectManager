using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Cooking;
using HomeManager.Models.Entities.Finance;
using HomeManager.Data.Context;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Type = HomeManager.Models.Entities.Finance.Type;

//Add-Migration HomeManager.PutNameHere -Context HomeManagerContext -OutputDir "Migrations"

namespace HomeManager.Data
{
    public class HomeManagerContext : IdentityDbContext<User, Role, Guid>
    {
        public HomeManagerContext(DbContextOptions<HomeManagerContext> options) : base(options)
        {
        }

        public DbSet<Payment> FinancePayments { get; set; }
        public DbSet<Type> FinanceTypes { get; set; }
        public DbSet<Category> FinanceCategories { get; set; }
        public DbSet<Status> FinanceStatuses { get; set; }
        public DbSet<Template> FinanceTemplates { get; set; }
        public DbSet<Repeating> FinanceRepeatings { get; set; }
        public DbSet<Tag> CookingTags { get; set; }
        public DbSet<Ingredient> CookingIngredients {  get; set; }
        public DbSet<Recipe> CookingRecipes { get; set; }
        public DbSet<Favorites> CookingFavorites { get; set; }


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

            modelBuilder.Entity<Payment>()
            .Property(e => e.Description_TaxList)
            .HasConversion(
                v => string.Join(',', v).Trim().Replace("%20", " "),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Payment>()
            .Property(e => e.Amount_TaxList)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Payment>()
            .Property(e => e.TaxList)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Payment>()
            .Property(e => e.Description_ExtraCosts)
            .HasConversion(
                v => string.Join(',', v).Trim().Replace("%20", " "),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));


            modelBuilder.Entity<Payment>()
            .Property(e => e.Amount_ExtraCosts)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));


            modelBuilder.Entity<Repeating>()
            .Property(e => e.Description_TaxList)
            .HasConversion(
                v => string.Join(',', v).Trim().Replace("%20", " "),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Repeating>()
            .Property(e => e.Amount_TaxList)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Repeating>()
            .Property(e => e.TaxList)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Repeating>()
            .Property(e => e.Description_ExtraCosts)
            .HasConversion(
                v => string.Join(',', v).Trim().Replace("%20", " "),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));


            modelBuilder.Entity<Repeating>()
            .Property(e => e.Amount_ExtraCosts)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));



            modelBuilder.Entity<Recipe>()
            .Property(e => e.Instructions)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Ingredient>()
    .HasMany(left => left.Recipes)
    .WithMany(right => right.Ingredients)
    .UsingEntity(join => join.ToTable("CookingIngredientRecipes"));

            modelBuilder.Entity<Ingredient>()
    .HasMany(left => left.Tags)
    .WithMany(right => right.Ingredients)
    .UsingEntity(join => join.ToTable("CookingIngredientTags"));

            modelBuilder.Entity<Recipe>()
    .HasMany(left => left.Tags)
    .WithMany(right => right.Recipes)
    .UsingEntity(join => join.ToTable("CookingRecipeTags"));

            modelBuilder.Entity<Type>()
                .Property(c => c.EndTaxType)
                .HasConversion<int>();

            modelBuilder.Entity<Type>()
                .Property(c => c.TransactionType)
                .HasConversion<int>();

            modelBuilder.Entity<Payment>().ToTable("FinancePayments");
            modelBuilder.Entity<Type>().ToTable("FinanceTypes");
            modelBuilder.Entity<Category>().ToTable("FinanceCategories");
            modelBuilder.Entity<Status>().ToTable("FinanceStatuses");
            modelBuilder.Entity<Template>().ToTable("FinanceTemplates");
            modelBuilder.Entity<Repeating>().ToTable("FinanceRepeatings");
            modelBuilder.Entity<Tag>().ToTable("CookingTags");
            modelBuilder.Entity<Ingredient>().ToTable("CookingIngredients");
            modelBuilder.Entity<Recipe>().ToTable("CookingRecipes");
            modelBuilder.Entity<Favorites>().ToTable("CookingFavorites");

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}
