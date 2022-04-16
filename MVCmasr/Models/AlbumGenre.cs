using System.ComponentModel.DataAnnotations.Schema;

namespace MVCmasr.Models
{
	public class AlbumGenre
	{
		[ForeignKey("Album")]
		public int AlbumId { get; set; }
		public virtual Album Album { get; set; }

		[ForeignKey("Genre")]
		public int GenreId { get; set; }
		public virtual Genre Genre { get; set; }
	}
}
