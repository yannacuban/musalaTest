using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Gateways.EntityFrameworkCore.QueryExtensions;
using GateWays.Common.Pagination;
using GateWays.EntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Gateways.EntityFrameworkCore.Repositories
{
    public class GatewayRepository : IGatewayRepository
    {
        private readonly SqlModelContext _context;

        public GatewayRepository(SqlModelContext context)
        {
            this._context = context;
        }

        public async Task<Gateway> AddGateway(Gateway gateway)
        {
            await _context.Gateways.AddAsync(gateway);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1 ? gateway : null;
        }

        public async Task<Gateway> GetGatewayById(long id)
        {
            var item = await this._context.Gateways
                .Include(b => b.PeripheralList)
                .FirstOrDefaultAsync(b => b.Id == id); ;

            return item;
        }

        public async Task<(IEnumerable<Gateway>, int)> GetGatewayList(PaginationParams paginationParams)
        {
            var data = this._context.Gateways.AsQueryable();
            data = paginationParams.Order == ESortOrder.Ascending ? data.OrderBy(x => x.Name) : data.OrderByDescending(x => x.Name);
            var (page, countTotal) = data.QueryPage(paginationParams, null, source => source
                .Include(a => a.PeripheralList), project: x => x);

            var result = await page.ToListAsync();
            return (result, countTotal());
        }

        public async Task<bool> RemoveGateway(long id)
        {
            var item = await this._context.Gateways.FindAsync(id);

            if (item == null)
            {
                return false;
            }
            _context.Gateways.Remove(item);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<Gateway> UpdateGateway(Gateway gateway)
        {
            _context.Gateways.Update(gateway);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 0 ? null : gateway;
        }
    }
}
