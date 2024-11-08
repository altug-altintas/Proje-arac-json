using Proje_Dal.Context;
using Proje_Dal.Repositories.Interfaces.Abstract;
using Proje_model.Models.Abstract;
using Proje_model.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Proje_Dal.Repositories.Abstract
{
    public class BaseRepo<T> : IBaseRepo<T> where T : BaseEntity
    {
        protected readonly ProjectContext _context;
        private readonly DbSet<T> _table;

        public BaseRepo(ProjectContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public void Create(T entity)
        {
            _table.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            entity.Statu = Statu.Passive;
           
            _context.SaveChanges();
        }
        public void Active(T entity)
        {
            entity.Statu = Statu.Active;
            // _table.Remove(entity);   // aktif yapar
            _context.SaveChanges();
        }


        public TResult GetByDefault<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> expression,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _table;

            if (include != null) query = include(query);
            return query.Where(expression).Select(selector).FirstOrDefault();
        }

        public List<TResult> GetByDefaults<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> expression,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _table;
            if (include != null) query = include(query);
            if (orderBy != null) return orderBy(query).Where(expression).Select(selector).ToList();

            return query.Where(expression).Select(selector).ToList();

        }

        public T GetDefault(Expression<Func<T, bool>> expression)
        {
            return _table.Where(expression).FirstOrDefault();
        }

        public List<T> GetDefaults(Expression<Func<T, bool>> expression)
        {
            return _table.Where(expression).ToList();
        }

        public void Update(T entity)
        {
            entity.Statu = Statu.Modified;
            _table.Update(entity);
            _context.SaveChanges();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _table.FindAsync(id);
        }

    }
}
