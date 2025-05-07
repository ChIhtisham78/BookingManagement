using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shaghaf.Core.Entities.IdentityEntities;
using Shaghaf.Core.Services.Contract;
using Shaghaf.Infrastructure.Services;
using Talabat.APIs.Controllers;

namespace Shaghaf.API.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly IAuthService _authService;
        private readonly ISMSService _smsService;

        public AccountController(IAuthService authService, ISMSService smsService)
        {
            _authService = authService;
            _smsService = smsService;
        }

        #region Account Management

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(new { result.Username, result.Roles, result.Token, result.ExpiresOn });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(new { result.Username, result.Roles, result.Token, result.ExpiresOn });
        }

        [HttpPost("addrole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(new { Message = "Role added successfully" });
        }

        #endregion
            
        #region Password and Verification Code Management

        [HttpPost("send-verification")]
        public async Task<IActionResult> SendVerificationCodeAsync([FromBody] VerificationRequestModel model)
        {
            if (string.IsNullOrEmpty(model.PhoneNumber))
            {
                return BadRequest("Phone number is required.");
            }

            var success = await _authService.SendVerificationCodeAsync(model.PhoneNumber);

            if (!success)
            {
                return BadRequest("Failed to send verification code.");
            }

            return Ok("Verification code sent successfully.");
        }

        [HttpPost("verify-code")]
        public async Task<IActionResult> VerifyCodeAsync([FromBody] VerifyCodeRequestModel model)
        {
            if (string.IsNullOrEmpty(model.PhoneNumber) || string.IsNullOrEmpty(model.Code))
            {
                return BadRequest("Phone number and code are required.");
            }

            var isCodeValid = await _authService.VerifyCodeAsync(model.PhoneNumber, model.Code);

            if (!isCodeValid)
            {
                return BadRequest("Invalid verification code.");
            }

            return Ok("Verification code is valid.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequestModel model)
        {
            if (string.IsNullOrEmpty(model.PhoneNumber) || string.IsNullOrEmpty(model.NewPassword) || string.IsNullOrEmpty(model.ConfirmPassword))
            {
                return BadRequest("Phone number, new password, and confirmation password are required.");
            }

            var result = await _authService.ResetPasswordAsync(model.PhoneNumber, model.NewPassword, model.ConfirmPassword);

            if (!result)
            {
                return BadRequest("Failed to reset password.");
            }

            return Ok("Password has been reset successfully.");
        }

        #endregion
    }
}
