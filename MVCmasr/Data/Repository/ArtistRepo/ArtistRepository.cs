using MVCmasr.Context;
using MVCmasr.Models;

namespace MVCmasr.Data.Repository
{
    public class ArtistRepository : GeneralRepository<Artist> , IArtistRepository
    {

        public ArtistRepository(ApplicationDbContext context) : base(context)
        {
        }
        
    }
}
