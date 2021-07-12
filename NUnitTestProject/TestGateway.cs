using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GateWays.Common.QueryResults;
using GateWays.Common.Resources;
using GateWaysAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace NUnitTestProject
{
    public class TestGateway
    {
        private GatewayController _gatewayController;

        [SetUp]
        public void Setup()
        {
            this._gatewayController = Utils.CreateGatewayController();
        }

        [Test]
        public async Task TestCreateGateway()
        {
            var gatewayRequest = new AddGatewayResource
            {
               IpAddress = "123.25.26.23",
               Name = "Name",
               SerialNumber = "123AB"
            };

            var response = await this._gatewayController.PostGateway(gatewayRequest);

            Assert.IsNotNull(response);
            Assert.IsInstanceOf<ObjectResult>(response);

            var viewResult = response as ObjectResult;
            Assert.AreEqual(200, viewResult?.StatusCode);

            Assert.IsInstanceOf<GatewayDTO>(viewResult?.Value);
        }
    }
}
