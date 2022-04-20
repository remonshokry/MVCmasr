using System.Collections.Generic;
using MVCmasr.Models;

namespace MVCmasr.Data.Repository
{
    public interface ISongRepository : IGeneralRepository<Song>
    {
        Song GetByName(string _title);
        Song GetByIdWithAllData(int id);
        List<Song> GetAllWithAllData();
    }
}
