using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        void Update(T entity);
        void Insert(T entity);
        T GetOne(int id);
        List<T> GetAll(Expression<Func<T, bool>> predicate);
        void Remove(int id);
    }
}
