using Microsoft.AspNetCore.Mvc;
using UMS.Application.Common.Interfaces;
using UMS.Application.Services;

namespace UMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ITotpService _otpService;

        public AuthenticationController(IAuthenticationService authenticationService, ITotpService otpService)
        {
            _authenticationService = authenticationService;
            _otpService = otpService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var otpKey = await _authenticationService.AuthenticateAsync(login.username, login.password);
            if (otpKey == null)
                return Unauthorized();

            return Ok(new { message = "Enter OTP Code", otpSecret = otpKey }); ;
        }

        [HttpPost("validate-otp")]
        public async Task<IActionResult> ValidateOtp([FromBody] OtpRequest otpRequest)
        {
            var validOtp = _otpService.ValidateOtpCode(otpRequest.SecretKey, otpRequest.OtpCode);
            if (!validOtp)
            {
                return Unauthorized();  
            }

            var token = await _authenticationService.GenerateJwtAsync(otpRequest.Username);
            return Ok(new { token });
        }

        public class LoginRequest
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        public class OtpRequest
        {
            public string SecretKey { get; set; }
            public int OtpCode { get; set; }
            public string Username { get; set; }
        }
    }
}
