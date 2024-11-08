using Proje_model.Models.Abstract;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Proje_Dal.Repositories.Interfaces.Abstract
{
    public interface IBaseRepo<T> where T : BaseEntity  //generik yaptık
    {


        // CRUD OPERASYON = CREATE READ UPDATE DELETE 
        void Create(T entity);

        void Update(T entity);
        void Delete(T entity);

        void Active(T entity);

        Task<T> GetByIdAsync(int id);
        T GetDefault(Expression<Func<T, bool>> expression);    // tek bir T nesnesini bir expression sonucu döner

        List<T> GetDefaults(Expression<Func<T, bool>> expression);   // expressiona uyan tüm T tipindeki nesneleri getirir.


        TResult GetByDefault<TResult>(Expression<Func<T, TResult>> selector, // select seçim
                                     Expression<Func<T, bool>> expression, // filtreleme - where
                                     Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);


        List<TResult> GetByDefaults<TResult>(
                                         Expression<Func<T, TResult>> selector,                  // select seçim
                                         Expression<Func<T, bool>> expression,                   // filtreleme - where
                                         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null // sıralama - order by
                                         );

    }
}
