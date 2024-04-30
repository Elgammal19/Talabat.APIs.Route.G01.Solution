using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;

namespace Talabat.APIs.Controllers
{
	public class AccountController : BaseApiController
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IAuthService _authService;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager ,IAuthService authService)
        {
			_userManager = userManager;
			_signInManager = signInManager;
			_authService = authService;
		}

		#region Sign Up

		[HttpPost("register")] // Post --> BaseUrl/api/Account
		public async Task<ActionResult<UserDto>> Register(RegisterDto model)
		{
			var user = new ApplicationUser()
			{
				DispalyName = model.DisplayName ,
				Email = model.Email,
				UserName = model.Email.Split('@')[0],
				PhoneNumber = model.Phone
			};

			var result = await _userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded) return BadRequest(new ApiValidationErrorResponse()
			{
				Errors = result.Errors.Select(error => error.Description)
			});

			return Ok(new UserDto()
			{
				DisplayName = model.DisplayName,
				Email = model.Email,
				Token = await _authService.CreateTokenAsync(user, _userManager)
			}); ;
		}

		#endregion

		#region Sign In

		[HttpPost("Login")] // Post --> BaseUrl/api/Account
		public async Task<ActionResult<UserDto>> Login(LoginDto model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);

			if (user is null) return Unauthorized(new ApiResponse(401 , "Invalid Login"));

			var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

			if (!result.Succeeded) return BadRequest(new ApiResponse(401, "Invalid Login"));

			return Ok(new UserDto()
			{
				DisplayName = user.DispalyName,
				Email= user.Email,
				Token = await _authService.CreateTokenAsync(user, _userManager)
			});
		}

		#endregion

		[Authorize]
		[HttpGet]
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);

			var user = await _userManager.FindByEmailAsync(email);

			return Ok(new UserDto()
			{
				DisplayName = user.DispalyName,
				Email = user.Email,
				Token = await _authService.CreateTokenAsync(user, _userManager)
			});
		}

		[Authorize]
		[HttpGet("address")]
		public async Task<ActionResult<Address>> GetUserAddress()
		{
			var user = await _userManager.FindUSerWithAddressAsync(User);

			return Ok(user.Address);
		}

	}
}
