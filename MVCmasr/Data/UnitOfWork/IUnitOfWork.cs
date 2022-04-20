using MVCmasr.Data.Repository;


namespace MVCmasr.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IAlbumRepository AlbumRepository { get; }
        public ISongRepository SongRepository { get; }
        public IArtistRepository ArtistRepository { get; }
        public IGenreRepository GenreRepository { get; }
        public IOrderItemRepository OrderItemRepository{ get; }
        public IOrderRepository OrderRepository{ get; }
        public IRolesRepository RolesRepository { get; }
    
    }
}
