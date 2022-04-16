using Microsoft.AspNetCore.Mvc.Rendering;
using MVCmasr.Models;
using System.Collections.Generic;

namespace MVCmasr.ViewModels
{
	public class AlbumGenreViewModel
	{
		public Album Album { get; set; }
		public IEnumerable<SelectListItem> Genre { get; set; }
	}
}
