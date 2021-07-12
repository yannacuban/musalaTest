using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class PeripheralControllerTest
    {
        private readonly Mock<IPeripheralService> _mockPeripheralService;
        private readonly PeripheralController _controller;


        public List<BaseResponse<Peripheral>> Peripherals { get; set; }
        public PeripheralControllerTest()
        {
            _mockPeripheralService = new Mock<IPeripheralService>();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfiles(new List<Profile>
            {
                new MappingProfile()
            }));
            IMapper mapper = mapperConfiguration.CreateMapper();
            _controller = new PeripheralController(_mockPeripheralService.Object, mapper);

            this.Peripherals = new List<BaseResponse<Peripheral>>
            {
                new BaseResponse<Peripheral>(new Peripheral
                {
                    Id = 1,
                    Status = "offline",
                    UID = 12,
                    Created = DateTime.Now,
                    Vendor = "VendorTest",
                    GatewayId = 1
                }),
                new BaseResponse<Peripheral>(new Peripheral
                {
                    Id = 2,
                    Status = "online",
                    UID = 12,
                    Created = DateTime.Now,
                    Vendor = "VendorTest",
                    GatewayId = 1
                }),
                new BaseResponse<Peripheral>(new Peripheral
                {
                    Id = 3,
                    Status = "offline",
                    UID = 12,
                    Created = DateTime.Now,
                    Vendor = "VendorTest",
                    GatewayId = 1
                })
            };
        }

        [Fact]
        public async Task GetPeripheral_ExistingIdPassed_ReturnsAPeripheral()
        {
            // Arrange
            var peripheralId = 1;
            _mockPeripheralService.Setup(svc => svc.GetPeripheralById(peripheralId))
                .ReturnsAsync(Peripherals.Single(m => m.Resource.Id.Equals(peripheralId)));

            // Act
            var result = await _controller.GetPeripheral(peripheralId);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var viewResult = result as OkObjectResult;

            Assert.IsType<PeripheralDTO>(viewResult?.Value);
        }

        [Fact]
        public async Task Add_ValidPeripheral_ReturnsCreatedResponse()
        {
            var peripheral = new Peripheral
            {
                Id = 4,
                Status = "online",
                UID = 12,
                Created = DateTime.Now,
                Vendor = "VendorTest",
                GatewayId = 1
            };
            var baseResponse = new BaseResponse<Peripheral>(peripheral);
            _mockPeripheralService.Setup(svc => svc.AddPeripheral(It.IsAny<Peripheral>()))
                .Callback((Peripheral basePeripheral) =>
                {
                    this.Peripherals.Add(baseResponse);
                })
                .ReturnsAsync(baseResponse);

            var peripheralDto = await _controller.PostPeripheral(new AddPeripheralResource
            {
               UID = peripheral.UID,
               Status = peripheral.Status,
               Vendor = peripheral.Vendor,
               GatewayId = peripheral.GatewayId
            });

            Assert.NotNull(peripheralDto);
            Assert.IsType<OkObjectResult>(peripheralDto);

            var viewResult = peripheralDto as OkObjectResult;

            Assert.IsType<PeripheralDTO>(viewResult?.Value);
        }
    }
}
