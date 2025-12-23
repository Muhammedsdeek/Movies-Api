using System.ComponentModel.DataAnnotations;

namespace Movies.DTOs.Account
{
	public class Login
	{
		[EmailAddress]
		public string Email { get; set; }

		[MaxLength(16)]
		[MinLength(6)]

		public string Password { get; set; }
	}
}
