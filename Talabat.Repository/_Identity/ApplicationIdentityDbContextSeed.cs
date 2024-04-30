using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Infrastructure._Identity
{
	public static class ApplicationIdentityDbContextSeed
	{
		public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager)
		{
			if(!userManager.Users.Any())
			{
				var user = new ApplicationUser()
				{
					DispalyName = "Mohammed Elgammal",
					Email = "mohamadalgamal1919@gmail.com",
					PhoneNumber = "1234567890",
					UserName = "Elgammal"
				};

				await userManager.CreateAsync(user , "P@ssw0r");
			}
		}
	}
}
