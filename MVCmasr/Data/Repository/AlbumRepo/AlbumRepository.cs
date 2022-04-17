using Microsoft.EntityFrameworkCore;
using MVCmasr.Context;
using MVCmasr.Models;
using System.Collections.Generic;
using System.Linq;

namespace MVCmasr.Data.Repository
{
	public class AlbumRepository : GeneralRepository<Album> , IAlbumRepository
	{
		private readonly ApplicationDbContext _context;

        public AlbumRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Album GetByIdWithAllData(int id)
        {
            return _context.Albums.Include(a => a.Genres).Include(a => a.Artists).SingleOrDefault(a => a.Id == id);
        }
        public List<Album> GetAllWithAllData()
        {
            return _context.Albums.Include(a => a.Genres)
                                    .Include(a => a.Artists)
                                    .Include(a=> a.Songs)
                                    .ToList();
        }

        public Album GetByName(string _title)
		{
			return _context.Albums.FirstOrDefault(album => album.Title == _title);
		}

        public override int Update(int _id, Album album)
        {
            var oldAlbum = GetByIdWithAllData(_id);

            if(album is not null)
            {
                oldAlbum.Id = album.Id;
                oldAlbum.Title = album.Title;
                oldAlbum.ReleaseDate = album.ReleaseDate;
                oldAlbum.SongsNumber = album.SongsNumber;
                oldAlbum.Artists = album.Artists;
                oldAlbum.Price = album.Price;
                oldAlbum.Genres = album.Genres;

            }
            return _context.SaveChanges();
        }

    }
}
