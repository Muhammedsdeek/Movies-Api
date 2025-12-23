using Microsoft.AspNetCore.Identity;

namespace Movies.Seeders
{
	public static class RolesSeeder
	{
		private static readonly string[] Roles =
		{
			"Admin",
			"User"
		};

		public static async Task SeedAsync(IServiceProvider serviceProvider)
		{
			using var scope = serviceProvider.CreateScope();

			var roleManager = scope.ServiceProvider
				.GetRequiredService<RoleManager<IdentityRole>>();

			foreach (var role in Roles)
			{
				if (!await roleManager.RoleExistsAsync(role))
				{
					await roleManager.CreateAsync(new IdentityRole(role));
				}
			}
		}
	}
}
