using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Realtor.Data;

namespace Realtor.Extensions.DependencyInjection
{
    public static class Database
    {
        public static void AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString,
                builder => builder.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)));
        }
    }
}
