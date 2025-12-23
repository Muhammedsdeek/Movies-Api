using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DTOs.Genre;
using Movies.Entities;
using Movies.Services.Interfaces;
using System.Threading.Tasks;

namespace Movies.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class genreController : ControllerBase
	{
		private readonly Igeneric<Genre> genreREpo;
		private readonly IMapper mapper;

		public genreController(Igeneric<Genre> genreREpo ,IMapper mapper)
		{
			this.genreREpo = genreREpo;
			this.mapper = mapper;
		}
		[HttpGet("All")]

		public async Task<IActionResult> GetAllGenres()
		{

			var genres = await genreREpo.GetAllAsync();
		
			return(!genres.Any()) ? NotFound("No genres found.") : Ok(genres);	
		}

		[HttpPost("Add")]
		public async Task<IActionResult> AddGenre(AddGenre genre)
		{
			if(!ModelState.IsValid)
				return BadRequest(ModelState);

			var MappedGenre=mapper.Map<Genre>(genre);

			await genreREpo.AddAsync(MappedGenre);
			return StatusCode(201, MappedGenre);
		}
		[HttpPut("Update/{id}")]

		public async Task<IActionResult> UpdateGenre(int id, AddGenre genre)
		{
			if(!ModelState.IsValid)
				return BadRequest(ModelState);
			var existingGenre = await genreREpo.GetByIdAsync(id);
			if(existingGenre == null)
				return NotFound($"Genre with ID {id} not found.");
			mapper.Map(genre, existingGenre);
			await genreREpo.UpdateAsync(existingGenre);
			return Ok("Genre updated successfully.");
		}

		[HttpDelete("Delete/{id}")]

		public async Task<IActionResult> DeleteGenre(int id)
		{
			var existingGenre = await genreREpo.GetByIdAsync(id);
			if(existingGenre == null)
				return NotFound($"Genre with ID {id} not found.");
			await genreREpo.DeleteAsync(id);
			return Ok("Genre deleted successfully.");
		}


	}
}
