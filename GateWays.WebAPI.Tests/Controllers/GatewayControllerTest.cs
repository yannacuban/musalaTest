using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GateWays.Common.Comunication;
using GateWays.Common.QueryResults;
using GateWays.Common.Resources;
using GateWays.EntityFrameworkCore.Models;
using GateWays.Service.Services;
using GateWaysAPI.Configuration;
using GateWaysAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GateWays.WebAPI.Tests.Controllers
{
    public class GatewayControllerTest
    {
        private readonly Mock<IGatewayService> _mockGatewayService;
        private readonly GatewayController _controller;

        public GatewayControllerTest()
        {
            _mockGatewayService = new Mock<IGatewayService>();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfiles(new List<Profile>
            {
                new MappingProfile()
            }));
            IMapper mapper = mapperConfiguration.CreateMapper();
            _controller = new GatewayController(mapper, _mockGatewayService.Object);
            Gateways = new List<BaseResponse<Gateway>>
            {
                new BaseResponse<Gateway>(new Gateway
                {
                    Id = 1,
                    IpAddress = "123.36.25.24",
                    Name = "Gateway1",
                    SerialNumber = "123AB"
                }),
                new BaseResponse<Gateway>(new Gateway
                {
                    Id = 2,
                    IpAddress = "123.36.25.24",
                    Name = "Gateway1",
                    SerialNumber = "123A"
                }),
                new BaseResponse<Gateway>(new Gateway
                {
                    Id = 3,
                    IpAddress = "123.36.25.24",
                    Name = "Gateway1",
                    SerialNumber = "123B"
                })
            };
        }

        private List<BaseResponse<Gateway>> Gateways { get; }

        [Fact]
        public async Task GetGateway_ExistingIdPassed_ReturnsAGateway()
        {
            // Arrange
            var gatewayId = 1;
            _mockGatewayService.Setup(svc => svc.GetGatewayById(gatewayId))
                .ReturnsAsync(Gateways.Single(m => m.Resource.Id.Equals(gatewayId)));

            // Act
            var result = await _controller.GetGateway(gatewayId);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var viewResult = result as OkObjectResult;

            Assert.IsType<GatewayDTO>(viewResult?.Value);
        }

        [Fact]
        public async Task Add_ValidGateway_ReturnsCreatedResponse()
        {
            var gateway = new Gateway
            {
                Id = 4,
                IpAddress = "13.50.79.67",
                Name = "GatewayName",
                SerialNumber = "1413"
            };
            var baseResponse = new BaseResponse<Gateway>(gateway);
            _mockGatewayService.Setup(svc => svc.AddGateway(It.IsAny<Gateway>()))
                .Callback((Gateway baseGateway) =>
                {
                    this.Gateways.Add(baseResponse);
                })
                .ReturnsAsync(baseResponse);

            var gatewayDto = await _controller.PostGateway(new AddGatewayResource
            {
                IpAddress = gateway.IpAddress,
                Name = gateway.Name,
                SerialNumber = gateway.SerialNumber
            });

            Assert.NotNull(gatewayDto);
            Assert.IsType<OkObjectResult>(gatewayDto);

            var viewResult = gatewayDto as OkObjectResult;

            Assert.IsType<GatewayDTO>(viewResult?.Value);
        }
    }
}
