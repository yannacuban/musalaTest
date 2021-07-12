using System;
using System.Collections.Generic;
using System.Text;
using GateWays.Service.Services;
using GateWaysAPI.Configuration;
using GateWaysAPI.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AutoMapper;
using Gateways.EntityFrameworkCore;
using Moq;

namespace NUnitTestProject
{
    public class Utils
    {
        private static Mock<IGatewayService> _gatewayService;
      
        private static IMapper _mapper;

        public static ServiceProvider serviceProvider;

        public static void Initialize()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var services = new ServiceCollection();
            services.ConfigureBusinessServices();
            services.AddDbContext<SqlModelContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            serviceProvider = services.BuildServiceProvider();

            _gatewayService = new Mock<IGatewayService>();
            var profileList = new List<Profile>
            {
                new MappingProfile()
            };

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfiles(profileList));
            _mapper = mapperConfiguration.CreateMapper();
        }

        public static GatewayController CreateGatewayController()
        {
            Initialize();
            return new GatewayController(_mapper, _gatewayService.Object);
        }
    }
}
