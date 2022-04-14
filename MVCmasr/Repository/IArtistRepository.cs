using MVCmasr.Models;

namespace MVCmasr.Repository
{

    public interface IArtistRepository : IGeneralRepository<Artist>
    {
        public int Insert(Artist _obj , int[] _roles);
        public int Update(int _id, Artist _obj , int[] _roles);
    }
}
