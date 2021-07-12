using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GateWays.EntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Gateways.EntityFrameworkCore.Repositories
{
    public class PeripheralRepository : IPeripheralRepository
    {
        private readonly SqlModelContext _context;

        public PeripheralRepository(SqlModelContext context)
        {
            this._context = context;
        }

        public async Task<Peripheral> AddPeripheral(Peripheral peripheral)
        {
            peripheral.Created = DateTime.Now;
            await _context.Peripherals.AddAsync(peripheral);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1 ? peripheral : null;
        }

        public async Task<Peripheral> GetPeripheralById(long id)
        {
            var item = await this._context.Peripherals.FindAsync(id);

            return item;
        }

        public async Task<List<Peripheral>> GetPeripheralListByGatewayId(long id)
        {
            var periphericalList = await this._context.Peripherals.Where(a => a.GatewayId == id).ToListAsync();
            return periphericalList;
        }

        public async Task<bool> RemovePeripheral(long id)
        {
            var peripherical = await this._context.Peripherals.FindAsync(id);
            if (peripherical == null)
            {
                return false;
            }
            _context.Peripherals.Remove(peripherical);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<Peripheral> UpdatePeripheral(Peripheral peripheral)
        {
            _context.Peripherals.Update(peripheral);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 0 ? null : peripheral;
        }
    }
}
