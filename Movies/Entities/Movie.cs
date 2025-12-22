using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Entities
{
	public class Movie
	{

		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Director { get; set; }
		public int? ReleaseYear { get; set; }

		public string?	 ImageUrl { get; set; }
		public bool? IsFavorite { get; set; }= false;

		[ForeignKey("Genre")]
		public int GenreId { get; set; }
		public virtual	Genre? Genre { get; set; }
	}
}
