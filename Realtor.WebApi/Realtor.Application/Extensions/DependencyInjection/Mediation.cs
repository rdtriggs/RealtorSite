using System;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using Realtor.Application.Behaviors;


namespace Realtor.Application.Extensions.DependencyInjection
{
    public static class Mediation
    {
        public static void AddMediation(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>));

            //TODO: RT- Replace on validation fix has been added.
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
