using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepoPractice.Domain.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        T Get(int id);
        void Add(T entity);
        void Update(T entity);
        void AddRange(IEnumerable<T> entities);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
