using System.Threading.Tasks;
using AutoMapper;
using GateWays.Common.Comunication;
using GateWays.Common.Pagination;
using GateWays.Common.QueryResults;
using GateWays.Common.Resources;
using GateWays.Common.Utils;
using GateWays.Common.Validator;
using GateWays.EntityFrameworkCore.Models;
using GateWays.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GateWaysAPI.Controllers
{
    [Route("api/gateway")]
    [ApiController]
    public class GatewayController : ControllerBase
    { 
        private readonly IGatewayService _gatewayService;
        private readonly IMapper _mapper;

        public GatewayController(IMapper mapper, IGatewayService gatewayService)
        {
            _gatewayService = gatewayService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gateway list.
        /// </summary>
        /// <returns>
        /// Returns the list of gateways.
        /// </returns>
        /// <response code="200">Successful operation</response>
        /// <response code="204">No Content</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedDataContract<GatewayDTO>))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(PagedDataContract<GatewayDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDataContract))]
        [SwaggerOperation(Tags = new[] { "GatewayList" })]
        [HttpGet]
        public async Task<IActionResult> GetGatewayList([FromQuery] PaginationParams addPagination)
        {
            var validator = new PaginationValidator();
            var validationResult = await validator.ValidateAsync(addPagination);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var response = await _gatewayService.GetGatewayList(addPagination);
            if (response.Success)
            {
                var resources = _mapper.Map<PagedResponse<Gateway>, PagedDataContract<GatewayDTO>>(response);
                return Ok(resources);
            }
            return BadRequest(new ErrorDataContract(response.Message));
        }

        /// <summary>
        /// Create Gateway
        /// </summary>
        /// <param name="gatewayResource">Gateway to create</param>
        /// <returns>A newly created Gateway</returns>
        /// <response code="201">The newly created Gateway</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GatewayDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDataContract))]
        [SwaggerOperation(Tags = new[] { "GatewayList" })]
        [HttpPost]
        public async Task<IActionResult> PostGateway([FromBody] AddGatewayResource gatewayResource)
        {
            var validIp = Utils.ValidateIpAddress(gatewayResource.IpAddress);
            if (!validIp)
            {
                return BadRequest(new ErrorDataContract($"The ip address is not valid."));
            }
            var gateway = _mapper.Map<AddGatewayResource, Gateway>(gatewayResource);
            var response = await _gatewayService.AddGateway(gateway);
            if (!response.Success)
                return BadRequest(new ErrorDataContract(response.Message));

            var resources = _mapper.Map<Gateway, GatewayDTO>(response.Resource);
            return Ok(resources);
        }

        /// <summary>
        /// Update Gateway
        /// </summary>
        /// <param name="gatewayResource">Updated Gateway</param>
        /// <returns>Returns a boolean notifying if the Gateway has been updated properly</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Gateway Not Found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GatewayDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDataContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDataContract))]
        [SwaggerOperation(Tags = new[] { "GatewayList" })]
        [HttpPut]
        public async Task<IActionResult> PutGateway([FromBody] UpdateGatewayResource gatewayResource)
        {
            var validIp = Utils.ValidateIpAddress(gatewayResource.IpAddress);
            if (!validIp)
            {
                return BadRequest(new ErrorDataContract($"The ip address is not valid."));
            }

            var gateway = _mapper.Map<UpdateGatewayResource, Gateway>(gatewayResource);
            var response = await _gatewayService.UpdateGateway(gateway);
            if (!response.Success)
                return BadRequest(new ErrorDataContract(response.Message));

            var resources = _mapper.Map<Gateway, GatewayDTO>(response.Resource);
            return Ok(resources);
        }

        /// <summary>
        /// Get Gateway
        /// </summary>
        /// <param name="id">Gateway ID</param>
        /// <returns>Returns the Gateway</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Gateway Not Found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GatewayDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDataContract))]
        [SwaggerOperation(Tags = new[] { "GatewayList" })]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGateway(int id)
        {
            var response = await _gatewayService.GetGatewayById(id);

            if (!response.Success)
                return BadRequest(new ErrorDataContract(response.Message));

            var resource = _mapper.Map<Gateway, GatewayDTO>(response.Resource);
            return Ok(resource);
        }

        /// <summary>
        /// Delete Gateway
        /// </summary>
        /// <param name="id">Gateway ID</param>
        /// <returns>Boolean notifying if the Gateway has been deleted properly</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Asset Building Program Not Found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDataContract))]
        [SwaggerOperation(Tags = new[] { "GatewayList" })]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGateway(int id)
        {
            var result = await _gatewayService.RemoveGateway(id);
            if (!result)
                return BadRequest();

            return Ok(true);
        }
    }
}
