using Microsoft.AspNetCore.Mvc;
using UMS.Application.Services;
using UMS.Core.Entities;

namespace UMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly ApplicationsService _applicationsService;

        public ApplicationController(ApplicationsService applicationsService)
        {
            _applicationsService = applicationsService;
        }

        [HttpGet("getApplications")]
        public async Task<IActionResult> GetApplications()
        {
            try
            {
                var applications = await _applicationsService.GetApplications();
                return Ok(applications);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getApplication")]
        public async Task<IActionResult> GetApplication([FromQuery] int applicatioId)
        {
            try
            {
                var application = await _applicationsService.GetApplication(applicatioId);
                return Ok(application);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("addApplication")]
        public async Task<IActionResult> AddApplication([FromBody] Applications application)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _applicationsService.AddApplication(application);
                return CreatedAtAction(nameof(GetApplication), new { id = application.id }, application);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("updateApplication")]
        public async Task<IActionResult> UpdateApplication([FromBody] Applications application)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (application.updated_at.HasValue)
                {
                    application.updated_at = application.updated_at.Value.ToUniversalTime();
                }

                await _applicationsService.UpdateApplication(application);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getApplicationTypes")]
        public async Task<IActionResult> GetApplicationTypes()
        {
            try
            {
                var types = await _applicationsService.GetApplicationTypes();
                return Ok(types);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
