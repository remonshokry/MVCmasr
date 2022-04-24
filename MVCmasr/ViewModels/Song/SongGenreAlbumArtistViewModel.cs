using System.Collections.Generic;
using MVCmasr.Models;

namespace MVCmasr.ViewModels
{
    public class SongGenreAlbumArtistViewModel
    {
		public Song Song { get; set; }
		public ICollection<Genre> Genres { get; set; }
		public ICollection<Artist> Artists { get; set; }
		public ICollection<Album> Albums { get; set; }
		public int SelectedGenreId { get; set; } = new();
		public int SelectedAlbumId { get; set; } = new();
		public List<int> SelectedArtistsIds { get; set; } = new();
	
	}
}
