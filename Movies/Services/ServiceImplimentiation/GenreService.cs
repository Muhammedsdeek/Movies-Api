using Movies.Entities;
using Movies.Services.Interfaces;
using Movies.Services.ServiceInterfacse;

namespace Movies.Services.ServiceImplimentiation
{
	public class GenreService(Igeneric<Genre> gernreRepo):Igenreservice


	{
		//private readonly Igeneric<Genre> gernreRepo = gernreRepo;


		public async Task<IEnumerable<Genre?>> GetAll()
		{
			var genres = await gernreRepo.GetAllAsync();
			return genres;

		}
	}
}
