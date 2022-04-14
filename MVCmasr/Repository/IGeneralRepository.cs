using System.Collections.Generic;

namespace MVCmasr.Repository
{
    public interface IGeneralRepository<T>
    {
        public List<T> GetAll(); 
        public T GetById(int _id); 
        public int Insert(T _obj ); 
        public int Update(int _id , T _obj ); 
        public int Delete(int _id);
    }
}
