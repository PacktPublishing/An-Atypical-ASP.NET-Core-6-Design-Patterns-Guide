using OpaqueFacadeSubSystem;
using OpaqueFacadeSubSystem.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddOpaqueFacadeSubSystem(this IServiceCollection services)
        {
            services.AddSingleton<IOpaqueFacade>(serviceProvider 
                => new OpaqueFacade(new ComponentA(), new ComponentB(), new ComponentC()));
            return services;
        }
    }
}
