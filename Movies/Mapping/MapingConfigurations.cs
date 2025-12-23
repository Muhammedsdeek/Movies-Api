using AutoMapper;
using Movies.Data;
using Movies.DTOs.Account;
using Movies.DTOs.Genre;
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
			CreateMap<Register, AppUser>().ReverseMap();
			CreateMap<AddGenre, Genre>().ReverseMap();
		}
	}
}
