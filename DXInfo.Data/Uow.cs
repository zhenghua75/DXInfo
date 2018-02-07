using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using System.Data.Entity;

namespace DXInfo.Data
{
    public class Uow<T>:IUow where T:DbContext,new()
    {
        //public DbContext Context { get; private set; }
        private T DbContext { get; set; }
        public Database Db { get; set; }
        public Uow()
        {
            CreateDbContext();
            this.Db = DbContext.Database;   
            //Context = DbContext;
        }
        public void Commit()
        {            
            DbContext.SaveChanges();            
        }

        protected void CreateDbContext()
        {
            DbContext = new T();

            // Do NOT enable proxied entities, else serialization fails
            DbContext.Configuration.ProxyCreationEnabled = false;

            // Load navigation properties explicitly (avoid serialization trouble)
            DbContext.Configuration.LazyLoadingEnabled = false;

            // Because Web API will perform validation, we don't need/want EF to do so
            DbContext.Configuration.ValidateOnSaveEnabled = false;

            DbContext.Configuration.AutoDetectChangesEnabled = false;
            // We won't use this performance tweak because we don't need 
            // the extra performance and, when autodetect is false,
            // we'd have to be careful. We're not being that careful.
            //this.Context = DbContext;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new EFRepository<TEntity>(DbContext);
        }

        #region IDisposable
        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~Uow()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //释放非托管资源
                }
                //释放托管资源
                //if (RepositoryProvider != null)
                //{
                //    RepositoryProvider.Dispose();
                //    RepositoryProvider = null;
                //}
                if (Db != null)
                {
                    Db = null;
                }
                if (DbContext != null)
                {
                    DbContext.Dispose();
                    DbContext = null;
                }                
                disposed = true;
            }
        }

        #endregion
    }
}
