using MVCmasr.Context;
using MVCmasr.Models;

namespace MVCmasr.Data.Repository
{
    public class RolesRepository : GeneralRepository<Role> , IRolesRepository
    {
        public RolesRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
