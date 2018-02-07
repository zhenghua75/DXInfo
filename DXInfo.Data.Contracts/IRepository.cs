using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace DXInfo.Data.Contracts
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        //T GetById(params object[] id);
        T GetById(Expression<Func<T, bool>> keySelector);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        //void Delete(object id);
    }
}
