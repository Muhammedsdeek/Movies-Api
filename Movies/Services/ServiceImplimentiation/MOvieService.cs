using Movies.Entities;
using Movies.Services.Interfaces;
using Movies.Services.ServiceInterfacse;

namespace Movies.Services.ServiceImplimentiation
{
	public class MOvieService:ImovieService
	{
		private readonly Igeneric<Movie> movieservice;

		public MOvieService(Igeneric<Movie> movieservice)
		{
			this.movieservice = movieservice;
		}

		public async Task<IEnumerable<Movie?>> GetAllMovieswithName(string Name)

		{

			return await movieservice.GetAllWithName(Name, "Title");
		}
	}
}
