using System;
using Gateways.EntityFrameworkCore.Repositories;
using GateWays.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace GateWaysAPI.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureBusinessServices(this IServiceCollection services)
        {
            if (services != null)
            {
                #region Repositories
                services.AddScoped<IGatewayRepository, GatewayRepository>();
                services.AddScoped<IPeripheralRepository, PeripheralRepository>();
                #endregion

                #region Services
                services.AddScoped<IGatewayService, GatewayService>();
                services.AddScoped<IPeripheralService, PeripheralService>();
                #endregion
            }
        }
    }
}
