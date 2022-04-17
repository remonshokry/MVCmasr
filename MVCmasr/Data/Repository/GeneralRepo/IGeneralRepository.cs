using System.Collections.Generic;

namespace MVCmasr.Data.Repository
{
    public interface IGeneralRepository<T> where T : class
    {
        List<T> GetAll(); 
        T GetById(int _id); 
        int Insert(T _obj ); 
        int Update(int _id , T _obj ); 
        int Delete(int _id);
    }
}
