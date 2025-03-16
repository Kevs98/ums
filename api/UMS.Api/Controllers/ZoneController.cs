using Microsoft.AspNetCore.Mvc;
using UMS.Application.Services;
using UMS.Core.Entities;

namespace UMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private readonly ZoneService _zoneService;

        public ZoneController(ZoneService zoneService)
        {
            _zoneService = zoneService;
        }

        [HttpGet("getZones")]
        public async Task<IActionResult> GetZones()
        {
            try
            {
                var zones = await _zoneService.GetZones();
                return Ok(zones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("addZone")]
        public async Task<IActionResult> AddZone([FromBody] Zone zone)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _zoneService.AddZone(zone);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteZone")]
        public async Task<IActionResult> DeleteZone([FromBody] Zone zone)
        {
            try
            {
                await _zoneService.RemoveZone(zone);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
