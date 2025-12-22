using System.ComponentModel.DataAnnotations;

namespace Movies.DTOs.Movies
{
	public class AddMovie
	{
		[Required]
		public string Title { get; set; }
		[Required]
		public string Director { get; set; }
		[Required]
		public int ReleaseYear { get; set; }

		public string? ImageUrl { get; set; }
		[Required]
		
		public int GenreId { get; set; }	
	}
}
