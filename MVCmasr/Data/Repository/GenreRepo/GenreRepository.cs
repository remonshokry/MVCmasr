using MVCmasr.Context;
using MVCmasr.Models;

namespace MVCmasr.Data.Repository
{
    public class GenreRepository : GeneralRepository<Genre> , IGenreRepository
    {
        public GenreRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
