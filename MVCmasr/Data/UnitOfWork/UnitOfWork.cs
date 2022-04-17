using MVCmasr.Data.Repository;

namespace MVCmasr.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            IAlbumRepository albumRepository,
            IArtistRepository artistRepository,
            IGenreRepository genreRepository,
            IOrderItemRepository orderItemRepository,
            IOrderRepository orderRepository,
            IRolesRepository rolesRepository)
        {
            AlbumRepository = albumRepository;
            ArtistRepository = artistRepository;
            GenreRepository = genreRepository;
            OrderItemRepository = orderItemRepository;
            OrderRepository = orderRepository;
            RolesRepository = rolesRepository;

        }

        public IAlbumRepository AlbumRepository { get; }

        public IArtistRepository ArtistRepository { get; }

        public IGenreRepository GenreRepository { get; }

        public IOrderItemRepository OrderItemRepository { get; }

        public IOrderRepository OrderRepository { get; }

        public IRolesRepository RolesRepository { get; }
    }
}
