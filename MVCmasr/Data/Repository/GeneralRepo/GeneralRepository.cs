using System.Collections.Generic;
using System.Linq;
using MVCmasr.Context;

namespace MVCmasr.Data.Repository
{
    public class GeneralRepository<T> : IGeneralRepository<T>
        where T : class
    {
        private readonly ApplicationDbContext _context;

        public GeneralRepository( ApplicationDbContext context )
        {
            _context = context;
        }


        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int _id)
        {
            return _context.Set<T>().Find(_id);
        }

        public int Delete(int _id)
        {
            var entityToDelete = GetById(_id);
            if (entityToDelete is not null)
            {
                _context.Set<T>().Remove(entityToDelete);
            }
            return _context.SaveChanges();
        }

        public int Insert(T _obj)
        {
            _context.Set<T>().Add(_obj);
            return _context.SaveChanges();
        }

        public virtual int Update(int _id, T _obj)
        {
            return 0;
        }


    }
}
