using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Repository.Class
{
    class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly Contexto _context;
        public GenericRepository(Contexto context)
        {
            this._context = context;
        }
        public List<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return this._context.Set<T>().Where(predicate).ToList();
        }

        public T GetOne(int id)
        {
            return this._context.Set<T>().Find(id);
        }

        public void Insert(T entity)
        {
            this._context.Set<T>().Add(entity);
            this._context.SaveChanges();
        }

        public void Remove(int id)
        {
            T entity = GetOne(id);
            this._context.Set<T>().Remove(entity);
            this._context.SaveChanges();
        }

        public void Update(T entity)
        {
            this._context.Set<T>().Attach(entity);
            this._context.Entry(entity).State = EntityState.Modified;
            this._context.SaveChanges();
        }
    }
}
