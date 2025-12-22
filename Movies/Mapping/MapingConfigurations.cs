using AutoMapper;
using Movies.DTOs.Movies;
using Movies.Entities;

namespace Movies.Mapping
{
	public class MapingConfigurations: Profile
	{

		public MapingConfigurations()
		{
			CreateMap<AddMovie, Movie>().ReverseMap();
			CreateMap<Movie,GetMovie>().ReverseMap();
		}
	}
}
