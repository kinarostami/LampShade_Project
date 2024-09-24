using System.Linq.Expressions;

namespace _0_Framework.Domain
{
    //Tkey = Id , T = entity
    public interface IRepository<Tkey,T> where T : class
    {
        T Get(Tkey id);
        void Create(T entity);
        List<T> GetAll();
        bool Exist(Expression<Func<T,bool>> expression);
        void SaveChanges();
    }
}
