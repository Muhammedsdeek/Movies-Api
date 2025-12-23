using Microsoft.AspNetCore.Identity;

namespace Movies.Data
{
	public class AppUser:IdentityUser
	{
		public string? FullName { get; set; }
	}
}
