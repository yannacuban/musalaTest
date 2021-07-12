using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GateWays.EntityFrameworkCore.Models;
using Gateways.EntityFrameworkCore.Repositories;
using GateWays.Common.Pagination;
using GateWays.Common.Comunication;
using Microsoft.EntityFrameworkCore;

namespace GateWays.Service.Services
{
    public class GatewayService : IGatewayService
    {
        private readonly IGatewayRepository _gatewayRepository;


        public GatewayService(IGatewayRepository gatewayRepository)
        {
            _gatewayRepository = gatewayRepository;
        }

        public async Task<BaseResponse<Gateway>> AddGateway(Gateway gateway)
        {
            try
            {
                var baseGateway = await _gatewayRepository.AddGateway(gateway);
                return baseGateway == null
                    ? new BaseResponse<Gateway>("An error occurred when saving the Gateway.")
                    : new BaseResponse<Gateway>(baseGateway);
            }
            catch (DbUpdateException ex)
            {
                return new BaseResponse<Gateway>($"An error occurred when saving the Gateway: {ex.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                return new BaseResponse<Gateway>($"An error occurred when saving the Gateway: {ex.InnerException}");
            }
        }

        public async Task<BaseResponse<Gateway>> GetGatewayById(long id)
        {
            try
            {
                var baseGateway = await _gatewayRepository.GetGatewayById(id);
                return baseGateway == null
                    ? new BaseResponse<Gateway>("An error occurred while looking for Gateway.")
                    : new BaseResponse<Gateway>(baseGateway);
            }
            catch (Exception ex)
            {
                return new BaseResponse<Gateway>($"An error occurred while looking for Gateway: {ex.InnerException}");
            }
        }

        public async Task<PagedResponse<Gateway>> GetGatewayList(PaginationParams paginationParams)
        {
            try
            {
                var data = await _gatewayRepository.GetGatewayList(paginationParams);
                return new PagedResponse<Gateway>(data.Item1, paginationParams.PageSize, data.Item2);
            }
            catch (Exception ex)
            {
                return new PagedResponse<Gateway>($"An error occurred while looking for Gateways: {ex}");
            }
        }

        public async Task<bool> RemoveGateway(long id)
        {
            try
            {
                var removed = await _gatewayRepository.RemoveGateway(id);

                return removed;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<BaseResponse<Gateway>> UpdateGateway(Gateway gateway)
        {
            try
            {
                var item = await _gatewayRepository.GetGatewayById(gateway.Id);
                if (item == null)
                    return new BaseResponse<Gateway>("Invalid Gateway");

                if(!string.IsNullOrEmpty(gateway.SerialNumber)) item.SerialNumber = gateway.SerialNumber;
                if (!string.IsNullOrEmpty(gateway.Name)) item.Name = gateway.Name;
                if (!string.IsNullOrEmpty(gateway.IpAddress)) item.IpAddress = gateway.IpAddress;
               
                var baseGateway = await _gatewayRepository.UpdateGateway(item);

                return baseGateway == null ? new BaseResponse<Gateway>("An error occurred when updating the Gateway.") : new BaseResponse<Gateway>(baseGateway);
            }
            catch (DbUpdateException ex)
            {
                return new BaseResponse<Gateway>($"An error occurred when saving the Gateway: {ex.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                return new BaseResponse<Gateway>($"An error occurred when saving the Gateway: {ex.InnerException}");
            }
        }
    }
}