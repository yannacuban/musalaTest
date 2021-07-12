using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GateWays.EntityFrameworkCore.Models;
using GateWays.Common.Pagination;
using GateWays.Common.Comunication;

namespace GateWays.Service.Services
{
    public interface IGatewayService
    {
        Task<PagedResponse<Gateway>> GetGatewayList(PaginationParams paginationParams);
        Task<BaseResponse<Gateway>> GetGatewayById(long id);
        Task<BaseResponse<Gateway>> AddGateway(Gateway gateway);
        Task<BaseResponse<Gateway>> UpdateGateway(Gateway gateway);
        Task<bool> RemoveGateway(long id);
    }
}
