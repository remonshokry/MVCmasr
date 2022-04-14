using MVCmasr.Models;

namespace MVCmasr.Repository
{
	public interface IAlbumRepository<Album> : IGeneralRepository<Album>
	{
		public int Insert(Album _obj, int[] _params = null);
		public int Update(int id, Album _obj, int[] _params = null);
	}
}
