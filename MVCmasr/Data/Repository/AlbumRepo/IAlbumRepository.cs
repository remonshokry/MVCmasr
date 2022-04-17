using System.Collections.Generic;
using MVCmasr.Models;

namespace MVCmasr.Data.Repository
{
    public interface IAlbumRepository : IGeneralRepository<Album>
    {
        Album GetByName(string _title);
        Album GetByIdWithAllData(int id);
        List<Album> GetAllWithAllData();
    }
}
