using System.Collections.Generic;
using System.Threading.Tasks;
using GateWays.EntityFrameworkCore.Models;

namespace Gateways.EntityFrameworkCore.Repositories
{
    public interface IPeripheralRepository
    {
        Task<List<Peripheral>> GetPeripheralListByGatewayId(long id);
        Task<Peripheral> GetPeripheralById(long id);
        Task<Peripheral> AddPeripheral(Peripheral peripheral);
        Task<Peripheral> UpdatePeripheral(Peripheral peripheral);
        Task<bool> RemovePeripheral(long id);
    }
}
