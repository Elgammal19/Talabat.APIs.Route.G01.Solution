using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs
{
	public class RegisterDto
	{
		[Required]
        public string DisplayName { get; set; } = null!;

		[Required]
		[EmailAddress]
        public string Email { get; set; } = null!;

		[Required]
		public string Phone { get; set; } = null!;

		[Required]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$", 
			               ErrorMessage = "Password must have : uppercase char & lowercase char & numbers & minimum lenght 6\r\n\r\n")]
        public string Password { get; set; } = null!;


	}
}
