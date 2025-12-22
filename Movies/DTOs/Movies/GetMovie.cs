using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.DTOs.Movies
{
	public class GetMovie
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Director { get; set; }
		public int? ReleaseYear { get; set; }

		public string? ImageUrl { get; set; }
		public bool? IsFavorite { get; set; } = false;

		public int GenreId { get; set; }
	}
}
