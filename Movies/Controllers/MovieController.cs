using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DTOs.Movies;
using Movies.Entities;
using Movies.Services.Interfaces;
using Movies.Services.ServiceInterfacse;
using System.Threading.Tasks;

namespace Movies.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MovieController(Igeneric<Movie> MovieREpo ,IMapper mapper,ImovieService movieService) : ControllerBase
	{

		[HttpGet("all")]

		public async Task<IActionResult> GetAllMovies()
		{

			var movies = await MovieREpo.GetAllAsync();

			var mappedMovies = mapper.Map<IEnumerable<GetMovie>>(movies);
			return (!movies.Any()) ? NotFound("No movies found.") : Ok(mappedMovies);


		}

		[HttpGet("{id}")]
		public async Task< IActionResult> GetMovie(int id) {

			var movie = await MovieREpo.GetByIdAsync(id);

			var mappedMovie = mapper.Map<GetMovie>(movie);

			return (movie == null) ? NotFound($"Movie with ID {id} not found.") : Ok(mappedMovie);



		}

		[HttpPost("add")]
		public async Task<IActionResult> AddMovie(AddMovie addMovie)
		{
		

			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var movieEntity = mapper.Map<Movie>(addMovie);
			var Result= await	MovieREpo.AddAsync( movieEntity);
			
			return CreatedAtAction(nameof(GetMovie), new { id = movieEntity.Id }, movieEntity);
		}

		[HttpPut("update/{id}")]
		public async Task<IActionResult> UpdateMovie(int id, AddMovie addMovie)
		{

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			
			var existingMovie = await MovieREpo.GetByIdAsync(id);
			if (existingMovie == null)
			{
				return NotFound($"Movie with ID {id} not found.");
			}
			existingMovie=mapper.Map(addMovie, existingMovie);
			var UpdateingREsult=  await MovieREpo.UpdateAsync(existingMovie);
			return  (UpdateingREsult>0)? Ok(existingMovie) : StatusCode(500, "An error occurred while updating the movie.");


		}
		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> DeleteMovie(int id)
		{
			var existingMovie =  MovieREpo.GetByIdAsync(id).Result;
			if (existingMovie == null)
			{
				return NotFound($"Movie with ID {id} not found.");
			}
			  var deleteResult=await MovieREpo.DeleteAsync(id);

			return (deleteResult > 0) ? Ok($"Movie with ID {id} deleted successfully.") : StatusCode(500, "An error occurred while deleting the movie.");


		}

		[HttpGet("search/{title}")]

		public async Task<IActionResult> SearchMoviesByTitle(string title)
		{
			var movies = await movieService.GetAllMovieswithName(title);
			var mappedMovies = mapper.Map<IEnumerable<GetMovie>>(movies);
			return (!movies.Any()) ? NotFound($"No movies found with title containing '{title}'.") : Ok(mappedMovies);
		}
	}

		
}
