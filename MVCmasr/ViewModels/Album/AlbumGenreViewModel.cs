using Microsoft.AspNetCore.Mvc.Rendering;
using MVCmasr.Models;
using System.Collections.Generic;

namespace MVCmasr.ViewModels
{
	public class AlbumGenreViewModel
	{
		public Album Album { get; set; }
		public ICollection<Genre> Genres { get; set; }
		public ICollection<Artist> Artists { get; set; }
		public List<int> SelectedGenresIds { get; set; } = new() ;
		public List<int> SelectedArtistsIds { get; set; } = new() ;
	}
}
