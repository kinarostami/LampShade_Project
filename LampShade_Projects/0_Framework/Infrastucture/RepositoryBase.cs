using System.Linq.Expressions;
using _0_Framework.Domain;
using Microsoft.EntityFrameworkCore;

namespace _0_Framework.Infrastucture
{
    public class RepositoryBase<Tkey,T> : IRepository<Tkey,T> where T : class
    {
        private readonly DbContext _context;

        public RepositoryBase(DbContext context)
        {
            _context = context;
        }

        public T Get(Tkey id)
        {
            return _context.Find<T>(id);
        }

        public void Create(T entity)
        {
            _context.Add(entity);
        }

        public List<T> GetAll()
        {
           return _context.Set<T>().ToList();
        }

        public bool Exist(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Any(expression);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
