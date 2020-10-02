using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Realtor.Api.Infrastructure.Extensions.DependencyInjection
{
    public static class UserDependencyInjectionExtensions
    {
        public static void AddUser(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<IPrincipal>(sp => sp.GetService<IHttpContextAccessor>()?.HttpContext?.User);
        }
    }
}