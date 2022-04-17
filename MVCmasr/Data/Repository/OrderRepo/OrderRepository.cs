using MVCmasr.Context;
using MVCmasr.Models;

namespace MVCmasr.Data.Repository
{
    public class OrderRepository : GeneralRepository<Order> , IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
