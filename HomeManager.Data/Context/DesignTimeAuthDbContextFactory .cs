using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Data.Context
{
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
