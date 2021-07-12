using System.Collections.Generic;
using System.Threading.Tasks;
using GateWays.Common.Pagination;
using GateWays.EntityFrameworkCore.Models;

namespace Gateways.EntityFrameworkCore.Repositories
{
    public interface IGatewayRepository
    {
        Task<(IEnumerable<Gateway>, int)> GetGatewayList(PaginationParams paginationParams);
        Task<Gateway> GetGatewayById(long id);
        Task<Gateway> AddGateway(Gateway gateway);
        Task<Gateway> UpdateGateway(Gateway gateway);
        Task<bool> RemoveGateway(long id);

    }
}
