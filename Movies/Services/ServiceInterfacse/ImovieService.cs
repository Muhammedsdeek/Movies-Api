using Movies.Entities;

namespace Movies.Services.ServiceInterfacse
{
	public interface ImovieService
	{
		Task<IEnumerable<Movie?>> GetAllMovieswithName(string Name);

	}
}
