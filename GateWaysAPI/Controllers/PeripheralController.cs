using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GateWays.Common.Comunication;
using GateWays.Common.QueryResults;
using GateWays.Common.Resources;
using GateWays.EntityFrameworkCore.Models;
using GateWays.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GateWaysAPI.Controllers
{
    [Route("api/peripheral")]
    [ApiController]
    public class PeripheralController : ControllerBase
    {
        private readonly IPeripheralService _peripheralService;
        private readonly IMapper _mapper;

        public PeripheralController(IPeripheralService peripheralService, IMapper mapper)
        {
            _peripheralService = peripheralService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets Peripheral 
        /// </summary>
        /// <param name="gatewayId">Gateway Identifier</param>
        /// <returns>Returns all Peripheral</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Gateway Not Found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PeripheralDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDataContract))]
        [SwaggerOperation(Tags = new[] { "PeripheralList" })]
        [HttpGet]
        [Route("list/{gatewayId}")]
        public async Task<ActionResult<IEnumerable<PeripheralDTO>>> GetAllPeripheral([FromRoute] int gatewayId)
        {
            var result = await _peripheralService.GetPeripheralListByGatewayId(gatewayId);
            if (!result.Success)
                return NotFound(new ErrorDataContract(result.Message));

            var resources = _mapper.Map<List<Peripheral>, List<PeripheralDTO>>(result.Resource);

            return Ok(resources);
        }

        /// <summary>
        /// Get Peripheral
        /// </summary>
        /// <param name="id">Peripheral ID</param>
        /// <returns>Returns the Peripheral</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Peripheral Not Found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PeripheralDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDataContract))]
        [SwaggerOperation(Tags = new[] { "PeripheralList" })]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPeripheral(int id)
        {
            var response = await _peripheralService.GetPeripheralById(id);
            if (!response.Success)
                return NotFound(new ErrorDataContract(response.Message));

            var resource = _mapper.Map<Peripheral, PeripheralDTO>(response.Resource);
            return Ok(resource);
        }

        /// <summary>
        /// Create Peripheral
        /// </summary>
        /// <param name="peripheralResource">Peripheral to create</param>
        /// <returns>A newly created Peripheral</returns>
        /// <response code="201">The newly created Peripheral</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PeripheralDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDataContract))]
        [SwaggerOperation(Tags = new[] { "PeripheralList" })]
        [HttpPost]
        public async Task<IActionResult> PostPeripheral([FromBody] AddPeripheralResource peripheralResource)
        {
            var peripheral = _mapper.Map<AddPeripheralResource, Peripheral>(peripheralResource);
            var response = await _peripheralService.AddPeripheral(peripheral);

            if (!response.Success)
                return BadRequest(new ErrorDataContract(response.Message));

            var resources = _mapper.Map<Peripheral, PeripheralDTO>(response.Resource);
            return Ok(resources);
        }

        /// <summary>
        /// Update Peripheral
        /// </summary>
        /// <param name="peripheralResource">Updated Peripheral</param>
        /// <returns>Returns a boolean notifying if the Peripheral has been updated properly</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Peripheral Not Found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PeripheralDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDataContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDataContract))]
        [SwaggerOperation(Tags = new[] { "PeripheralList" })]
        [HttpPut]
        public async Task<IActionResult> PutPeripheral([FromBody] UpdatePeripheralResource peripheralResource)
        {
            var peripheral = _mapper.Map<UpdatePeripheralResource, Peripheral>(peripheralResource);
            var response = await _peripheralService.UpdatePeripheral(peripheral);

            if (!response.Success)
                return BadRequest(new ErrorDataContract(response.Message));

            var resources = _mapper.Map<Peripheral, PeripheralDTO>(response.Resource);
            return Ok(resources);
        }

        /// <summary>
        /// Delete Peripheral
        /// </summary>
        /// <param name="id">Peripheral ID</param>
        /// <returns>Boolean notifying if the Peripheral has been deleted properly</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Asset Building Program Not Found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDataContract))]
        [SwaggerOperation(Tags = new[] { "PeripheralList" })]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePeripheral(int id)
        {
            var result = await _peripheralService.RemovePeripheral(id);
            if (!result)
                return BadRequest();

            return Ok(true);
        }
    }
}
