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
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using IdentityServer4.EntityFramework.Options;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Entities;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Extensions;

//Add-Migration HomeManager.PutNameHere -Context HomeManagerContextApi -OutputDir "Migrations"

namespace HomeManager.Data
{
    public class HomeManagerContextApi : KeyApiAuthorizationDbContext<User, Role, Guid>
    {
        public HomeManagerContextApi(DbContextOptions<HomeManagerContextApi> options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Payment> FinancePayments { get; set; }
        public DbSet<Type> FinanceTypes { get; set; }
        public DbSet<Category> FinanceCategories { get; set; }
        public DbSet<Status> FinanceStatuses { get; set; }
        public DbSet<Template> FinanceTemplates { get; set; }
        public DbSet<Repeating> FinanceRepeatings { get; set; }
        public DbSet<Wallet> FinanceWallets { get; set; }
        public DbSet<Tag> CookingTags { get; set; }
        public DbSet<Ingredient> CookingIngredients { get; set; }
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
            .Property(e => e.DetailedTaxDescription)
            .HasConversion(
                v => string.Join(',', v).Trim().Replace("%20", " "),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Payment>()
            .Property(e => e.DetailedTaxAmount)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Payment>()
            .Property(e => e.DetailedTaxRate)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Payment>()
            .Property(e => e.ExtraCostDescription)
            .HasConversion(
                v => string.Join(',', v).Trim().Replace("%20", " "),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));


            modelBuilder.Entity<Payment>()
            .Property(e => e.ExtraCostAmount)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));


            modelBuilder.Entity<Template>()
            .Property(e => e.DetailedTaxDescription)
            .HasConversion(
                v => string.Join(',', v).Trim().Replace("%20", " "),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Template>()
            .Property(e => e.DetailedTaxAmount)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Template>()
            .Property(e => e.DetailedTaxRate)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Template>()
            .Property(e => e.ExtraCostDescription)
            .HasConversion(
                v => string.Join(',', v).Trim().Replace("%20", " "),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));


            modelBuilder.Entity<Template>()
            .Property(e => e.ExtraCostAmount)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));


            modelBuilder.Entity<Repeating>()
            .Property(e => e.DetailedTaxDescription)
            .HasConversion(
                v => string.Join(',', v).Trim().Replace("%20", " "),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Repeating>()
            .Property(e => e.DetailedTaxAmount)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Repeating>()
            .Property(e => e.DetailedTaxRate)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Repeating>()
            .Property(e => e.ExtraCostDescription)
            .HasConversion(
                v => string.Join(',', v).Trim().Replace("%20", " "),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));


            modelBuilder.Entity<Repeating>()
            .Property(e => e.ExtraCostAmount)
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
            modelBuilder.Entity<Wallet>().ToTable("FinanceWallets");
            modelBuilder.Entity<Tag>().ToTable("CookingTags");
            modelBuilder.Entity<Ingredient>().ToTable("CookingIngredients");
            modelBuilder.Entity<Recipe>().ToTable("CookingRecipes");
            modelBuilder.Entity<Favorites>().ToTable("CookingFavorites");

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }

    public class KeyApiAuthorizationDbContext<TUser, TRole, TKey> : IdentityDbContext<TUser, TRole, TKey>, IPersistedGrantDbContext
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;

        public KeyApiAuthorizationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options)
        {
            _operationalStoreOptions = operationalStoreOptions;
        }

        public DbSet<PersistedGrant> PersistedGrants { get; set; }

        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

        Task<int> IPersistedGrantDbContext.SaveChangesAsync() => base.SaveChangesAsync();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value);
        }
    }

    public class ApiAuthorizationDbContext<TUser> : KeyApiAuthorizationDbContext<TUser, IdentityRole, string>
        where TUser : IdentityUser
    {
        public ApiAuthorizationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {
        }
    }
}
