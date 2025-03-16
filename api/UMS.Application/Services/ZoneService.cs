using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Core.Entities;
using UMS.Core.Interfaces;

namespace UMS.Application.Services
{
    public class ZoneService
    {
        private readonly IZoneRepository _zoneRepository;

        public ZoneService(IZoneRepository zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }

        public async Task<IEnumerable<Zone>> GetZones()
        {
            return await _zoneRepository.GetZones();
        }

        public async Task AddZone(Zone zone)
        {
            await _zoneRepository.AddZone(zone);
        }

        public async Task RemoveZone(Zone zone)
        {
            await _zoneRepository.RemoveZone(zone);
        }
    }
}
