using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UMS.Core.Entities;
using UMS.Core.Interfaces;
using UMS.Infrastructure.Persistence;

namespace UMS.Infrastructure.Repositories
{
    public class ZoneRepository : IZoneRepository
    {
        private readonly UmsDbContext _context;

        public ZoneRepository(UmsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Zone>> GetZones()
        {
            return await _context.Zones.ToListAsync();
        }

        public async Task AddZone(Zone zone)
        {
            await _context.Zones.AddAsync(zone);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveZone(Zone zone)
        {
            _context.Zones.Remove(zone);
            await _context.SaveChangesAsync();
        }
    }
}
