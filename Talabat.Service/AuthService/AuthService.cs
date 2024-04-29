using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;

namespace Talabat.Application.AuthService
{
	public class AuthService : IAuthService
	{
		private readonly IConfiguration _configuration;

		public AuthService(IConfiguration  configuration)
        {
			_configuration = configuration;
		}

        public async Task<string> CreateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager)
		{
			// 1. Payload --> Claim(Private Cliams) : For information exchange
			var authClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name , user.DispalyName),
				new Claim(ClaimTypes.Email , user.Email),
			};

			var Roles = await userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
				authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

			// 2. Craete Security Key
			var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:AuthKey"] ?? string.Empty));

			// 3. Create Token Object
			var token = new JwtSecurityToken(
					audience: _configuration["JWT:ValidAudience"],
					issuer: _configuration["JWT:ValidIssure"],
					expires: DateTime.Now.AddDays( double.Parse( _configuration["JWT:DurationInDays"] ?? "0")),
					claims: authClaims,
					signingCredentials: new SigningCredentials(authKey , SecurityAlgorithms.HmacSha256Signature)
				);

			// 4. Return Token
			return new JwtSecurityTokenHandler().WriteToken(token);

        }
	}
}
