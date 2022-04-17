using MVCmasr.Context;
using MVCmasr.Data.Repository;
using MVCmasr.Models;

namespace MVCmasr.Data.Repository
{
    public class OrderItemRepository : GeneralRepository<OrderItem> , IOrderItemRepository
    {
        public OrderItemRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
