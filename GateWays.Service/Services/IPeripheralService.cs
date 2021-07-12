using System.Collections.Generic;
using System.Threading.Tasks;
using GateWays.Common.Comunication;
using GateWays.EntityFrameworkCore.Models;

namespace GateWays.Service.Services
{
    public interface IPeripheralService
    {
        Task<BaseResponse<List<Peripheral>>> GetPeripheralListByGatewayId(long id);
        Task<BaseResponse<Peripheral>> GetPeripheralById(long id);
        Task<BaseResponse<Peripheral>> AddPeripheral(Peripheral peripheral);
        Task<BaseResponse<Peripheral>> UpdatePeripheral(Peripheral peripheral);
        Task<bool> RemovePeripheral(long id);
    }
}