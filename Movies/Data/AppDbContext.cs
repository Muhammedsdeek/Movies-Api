using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movies.Entities;

namespace Movies.Data
{
	public class AppDbContext:    IdentityDbContext<AppUser>       //DbContext
	{

	

		public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

		



			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Movie> Movies { get; set; }
		public DbSet<Genre> Genres { get; set; }
	}
}
