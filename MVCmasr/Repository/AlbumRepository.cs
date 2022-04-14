using MVCmasr.Context;
using MVCmasr.Models;
using System.Collections.Generic;
using System.Linq;

namespace MVCmasr.Repository
{
	public class AlbumRepository : IAlbumRepository<Album>
	{
		private readonly ApplicationDbContext context;

		public AlbumRepository(ApplicationDbContext _context)
		{
			context = _context;
		}
		public int Delete(int _id)
		{
			context.Albums.Remove(GetById(_id));
			return context.SaveChanges();
		}

		public List<Album> GetAll()
		{
			return context.Albums.ToList();
		}

		public Album GetById(int _id)
		{
			return context.Albums.FirstOrDefault(album => album.Id == _id);
		}

		public Album GetByName(string _title)
		{
			return context.Albums.FirstOrDefault(album => album.Title == _title);
		}

		public int Insert(Album _obj)
		{
			context.Albums.Add(_obj);
			return context.SaveChanges();
		}

		public int Update(int _id, Album _obj)
		{
			Album album = GetById(_id);
			if (album != null)
			{
				album.Id = _id;
				album.Title = _obj.Title;
				album.Price = _obj.Price;
				album.SongsNumber = _obj.SongsNumber;
				album.ReleaseDate = _obj.ReleaseDate;
				album.CountInStock = _obj.CountInStock;
				
				return context.SaveChanges();
			}
			return 0;
		}

		public int Insert(Album _obj, int[] _genres)
		{
			context.Albums.Add(_obj);
			
			foreach (var _genre in _genres)
			{
				var AlbumGenre = from genre in _obj.Genre
								where genre.Id == _genre
								select genre;

				Genre g = AlbumGenre.FirstOrDefault(g => g.Id == _genre);
				if (g != null)
				{
					g.Id = _genre;
				}
			}
			return context.SaveChanges();
		}
		public int Update(int id, Album _obj, int[] _genres = null)
		{
			context.Albums.Add(_obj);

			foreach (var _genre in _genres)
			{
				var AlbumGenre = from genre in _obj.Genre
								 where genre.Id == _genre
								 select genre;

				Genre g = AlbumGenre.FirstOrDefault(g => g.Id == _genre);
				if (g != null)
				{
					g.Id = _genre;
				}
			}
			return context.SaveChanges();
		}
	}
}
