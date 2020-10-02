using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Realtor.Api.Infrastructure.Extensions.DependencyInjection
{
    public static class Mapper
    {
        public static void AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
