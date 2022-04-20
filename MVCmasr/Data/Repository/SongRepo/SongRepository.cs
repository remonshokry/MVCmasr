using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MVCmasr.Context;
using MVCmasr.Models;

namespace MVCmasr.Data.Repository 
{
    public class SongRepository : GeneralRepository<Song> , ISongRepository
    {
        private readonly ApplicationDbContext context;

        public SongRepository(ApplicationDbContext _context) : base (_context)
        {
            context = _context;
        }

        public List<Song> GetAllWithAllData()
        {
            return context.Songs.Include(a => a.Artists).ToList();
        }

        public Song GetByIdWithAllData(int id)
        {
            return context.Songs.Include(a => a.Artists).SingleOrDefault(a => a.Id == id);
        }

        public Song GetByName(string _title)
        {
            return context.Songs.FirstOrDefault(s => s.Title == _title);
        }

        public override int Update(int _id, Song song)
        {
            var oldSong = GetByIdWithAllData(_id);

            if (song is not null)
            {
                if (song.AudioFile is not null)
                {
                    oldSong.Audio = song.Audio;
                }
                oldSong.Id = song.Id;
                oldSong.Title = song.Title;
                oldSong.ReleaseDate = song.ReleaseDate;
                oldSong.Album = song.Album;
                oldSong.Artists = song.Artists;
                oldSong.Genre = song.Genre;
                oldSong.IsFeatured = song.IsFeatured;

            }
            return context.SaveChanges();
        }
    }
}
