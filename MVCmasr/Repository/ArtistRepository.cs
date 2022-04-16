using System.Collections.Generic;
using System.Linq;
using MVCmasr.Context;
using MVCmasr.Models;

namespace MVCmasr.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        public ApplicationDbContext context;

        public ArtistRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public int Delete(int _id)
        {
            context.Artists.Remove(GetById(_id));
            return context.SaveChanges();

        }

        public List<Artist> GetAll()
        {
            return  context.Artists.ToList();
        }

        public Artist GetById(int _id)
        {
            return context.Artists.FirstOrDefault(ar => ar.Id == _id);
        }

        public int Insert(Artist _obj , int[] _roles)
        {
            context.Add(_obj);
            return context.SaveChanges();
        }

        public int Update(int _id, Artist _obj , int[] _roles)
        {
            Artist oldArtist = GetById(_id);
            if(oldArtist != null)
            {
                oldArtist.Id = _obj.Id;
                oldArtist.Name = _obj.Name;
                oldArtist.DateOfBirth = _obj.DateOfBirth;
                return context.SaveChanges();
            }
            return 0;
        }

        public int Insert(Artist _obj) => 0;
        public int Update(int _id, Artist _obj) => 0;

        
    }
}
