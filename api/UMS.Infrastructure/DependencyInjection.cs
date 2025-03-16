using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using UMS.Application.Common.Interfaces;
using UMS.Infrastructure.Services;

namespace UMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ITotpService, TotpService>();
            return services;
        }
    }
}
