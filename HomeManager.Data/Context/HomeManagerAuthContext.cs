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
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

//Add-Migration HomeManagerAuth.PutNameHere -Context HomeManagerAuthContext -OutputDir "Migrations/Auth"
//Update-Database -Context HomeManagerAuthContext

namespace HomeManager.Data
{
    public class HomeManagerAuthContext : KeyApiAuthorizationDbContext<User, Role, Guid>
    {
        public HomeManagerAuthContext(DbContextOptions<HomeManagerAuthContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedAuthorrization();

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

    public class DesignTimeAuthDbContextFactory : IDesignTimeDbContextFactory<HomeManagerAuthContext>
    {
        private const string CONN_STRING = "Server=localhost\\SQLEXPRESS;Database=HomeManagerAuth;Trusted_Connection=True;MultipleActiveResultSets=true";
        public HomeManagerAuthContext CreateDbContext(string[] args)
        {
            IServiceCollection services = new ServiceCollection();


            services.AddDbContext<HomeManagerAuthContext>(options => options.UseSqlServer(CONN_STRING));
            services.Configure<IdentityServer4.EntityFramework.Options.OperationalStoreOptions>(x => { });

            var context = services.BuildServiceProvider().GetService<HomeManagerAuthContext>();

            return context;
        }

    }
}
