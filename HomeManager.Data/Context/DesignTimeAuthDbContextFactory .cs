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
    public class DesignTimeAuthDbContextFactory : IDesignTimeDbContextFactory<HomeManagerContextApi>
    {
        private const string CONN_STRING = "Server=localhost\\SQLEXPRESS;Database=HomeManagerDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        public HomeManagerContextApi CreateDbContext(string[] args)
        {
            IServiceCollection services = new ServiceCollection();


            services.AddDbContext<HomeManagerContextApi>(options => options.UseSqlServer(CONN_STRING));
            services.Configure<IdentityServer4.EntityFramework.Options.OperationalStoreOptions>(x => { });

            var context = services.BuildServiceProvider().GetService<HomeManagerContextApi>();

            return context;
        }

    }
}
