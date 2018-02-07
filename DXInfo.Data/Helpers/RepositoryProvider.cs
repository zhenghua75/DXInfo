using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using DXInfo.Data.Contracts;

namespace DXInfo.Data
{
    //public class RepositoryProvider : IRepositoryProvider
    //{
    //    public RepositoryProvider()
    //    {
    //        Repositories = new Dictionary<Type, object>();
    //    }
    //    public DbContext DbContext { get; set; }


    //    public IRepository<T> GetRepositoryForEntityType<T>() where T : class
    //    {
    //        return GetRepository < IRepository<T>>(dbContext => new EFRepository<T>(dbContext));
    //    }
    //    public virtual T GetRepository<T>(Func<DbContext, object> factory) where T : class
    //    {
    //        object repoObj;
    //        Repositories.TryGetValue(typeof(T), out repoObj);
    //        if (repoObj != null)
    //        {
    //            return (T)repoObj;
    //        }
    //        var repo = (T)factory(DbContext);
    //        Repositories[typeof(T)] = repo;
    //        return repo;
    //    }
    //    protected Dictionary<Type, object> Repositories { get; private set; }

    //    public void SetRepository<T>(T repository)
    //    {
    //        Repositories[typeof(T)] = repository;
    //    }

    //    #region IDisposable
    //    private bool disposed = false;
    //    ~RepositoryProvider()
    //    {
    //        Dispose(false);
    //    }
    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (!disposed)
    //        {
    //            if (disposing)
    //            {
    //                //释放非托管资源
    //            }
    //            if (Repositories != null)
    //            {
    //                Repositories.Clear();
    //                Repositories = null;
    //            }
    //            //if (_repositoryFactories != null)
    //            //{                    
    //            //    _repositoryFactories = null;
    //            //}
    //            if (DbContext != null)
    //            {
    //                DbContext.Dispose();
    //                DbContext = null;
    //            }
    //            disposed = true;
    //        }
    //    }
    //    public void Dispose()
    //    {
    //        Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }
    //    #endregion

    //}    
}
