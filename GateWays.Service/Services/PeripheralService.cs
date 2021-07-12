using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GateWays.Common.Comunication;
using Gateways.EntityFrameworkCore.Repositories;
using GateWays.EntityFrameworkCore.Models;

namespace GateWays.Service.Services
{
    public class PeripheralService : IPeripheralService
    {
        private readonly IPeripheralRepository _peripheralRepository;
        private readonly IGatewayRepository _gatewayRepository;

        public PeripheralService(IPeripheralRepository peripheralRepository, IGatewayRepository gatewayRepository)
        {
            _peripheralRepository = peripheralRepository;
            _gatewayRepository = gatewayRepository;
        }

        public async Task<BaseResponse<Peripheral>> AddPeripheral(Peripheral peripheral)
        {
            try
            {
                var baseGateway = await _gatewayRepository.GetGatewayById(peripheral.GatewayId);
                if (baseGateway == null)
                    return new BaseResponse<Peripheral>("No Gateway was found");

                var basePeripheral = await _peripheralRepository.AddPeripheral(peripheral);
                return basePeripheral == null ? new BaseResponse<Peripheral>("An error occurred when saving the Peripheral.") : new BaseResponse<Peripheral>(basePeripheral);
            }
            catch (Exception ex)
            {
                return new BaseResponse<Peripheral>($"An error occurred when saving the Peripheral: {ex.Message}");
            }
        }

        public async Task<BaseResponse<Peripheral>> GetPeripheralById(long id)
        {
            try
            {
                var basePeripheral = await _peripheralRepository.GetPeripheralById(id);
                return basePeripheral == null ? new BaseResponse<Peripheral>("No Peripheral was found.") : new BaseResponse<Peripheral>(basePeripheral);
            }
            catch (Exception ex)
            {
                return new BaseResponse<Peripheral>($"No Peripheral was found: {ex.Message}");
            }
        }

        public async Task<BaseResponse<List<Peripheral>>> GetPeripheralListByGatewayId(long id)
        {
            try
            {
                var basePeripheralList = await _peripheralRepository.GetPeripheralListByGatewayId(id);
                if (basePeripheralList == null || !basePeripheralList.Any())
                    return new BaseResponse<List<Peripheral>>("No Peripheral was found");

                return new BaseResponse<List<Peripheral>>(basePeripheralList);
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Peripheral>>(ex.Message);
            }
        }

        public async Task<bool> RemovePeripheral(long id)
        {
            try
            {
                var removed = await _peripheralRepository.RemovePeripheral(id);

                return removed;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<BaseResponse<Peripheral>> UpdatePeripheral(Peripheral peripheral)
        {
            try
            {
                var baseGateway = await _gatewayRepository.GetGatewayById(peripheral.GatewayId);
                if (baseGateway == null)
                    return new BaseResponse<Peripheral>("No Gateway was found");

                var item = await _peripheralRepository.GetPeripheralById(peripheral.Id);
                if (item == null)
                    return new BaseResponse<Peripheral>("Invalid Peripheral");

                item.GatewayId = peripheral.GatewayId;
                if (!string.IsNullOrEmpty(peripheral.Status)) item.Status = peripheral.Status;
                if (!string.IsNullOrEmpty(peripheral.Vendor)) item.Vendor = peripheral.Vendor;
                item.UID = peripheral.UID;
                var basePeripheral = await _peripheralRepository.UpdatePeripheral(item);

                return basePeripheral == null ? new BaseResponse<Peripheral>("An error occurred when updating the Peripheral.") : new BaseResponse<Peripheral>(basePeripheral);
            }
            catch (Exception ex)
            {
                return new BaseResponse<Peripheral>($"An error occurred when updating the Peripheral: {ex.Message}");
            }
        }
    }
}