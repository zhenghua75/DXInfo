using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
using System.Data;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core;
//using System.Data.Objects;
//using System.Data.Objects.DataClasses;
//using System.Data.Metadata.Edm;
namespace DXInfo.Data
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        public EFRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");
            DbContext = dbContext;            
            DbSet = DbContext.Set<T>();
            
        }

        protected DbContext DbContext { get; set; }

        protected DbSet<T> DbSet { get; set; }

        public virtual IQueryable<T> GetAll()
        {
            return DbSet.AsNoTracking<T>();
            //return DbSet;
        }
        public virtual T GetById(Expression<Func<T, bool>> keySelector)
        {
            return DbSet.AsNoTracking<T>().FirstOrDefault(keySelector);
        }
        //public virtual T GetById(params object[] id)
        //{
        //    return DbSet.Find(id);
        //}
        private readonly Dictionary<Type, string[]> _dict = new Dictionary<Type, string[]>();

        public virtual void Add(T entity)
        {            
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
        }
        public void Detach(T entity)
        {
            ObjectStateEntry entry = null;
            ObjectContext objCtx = ((IObjectContextAdapter)DbContext).ObjectContext;

            EntityContainer container = objCtx.MetadataWorkspace.GetEntityContainer(objCtx.DefaultContainerName, DataSpace.CSpace);
            EntitySetBase entitySet = container.BaseEntitySets.Where(item => item.ElementType.Name.Equals(typeof(T).Name)).FirstOrDefault();
            EntityKey key = objCtx.CreateEntityKey(entitySet.Name, entity);

            if (objCtx.ObjectStateManager.TryGetObjectStateEntry(key, out entry))
            {
                objCtx.Detach(entry.Entity);
            }
                
        }
        public virtual void Update(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            
            if (dbEntityEntry.State == EntityState.Detached)
            {
                Detach(entity);
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                //dbEntityEntry.State = EntityState.Deleted;
                Detach(entity);
                DbSet.Attach(entity);
            }
            //else
            //{
                //DbSet.Attach(entity);
                DbSet.Remove(entity);
            //}
        }

        //public virtual void Delete(object id)
        //{
        //    var entity = GetById(id);
        //    if (entity == null) return;
        //    Delete(entity);
        //}

        #region IDisposable
        //private bool disposed = false;
        //~EFRepository()
        //{
        //    Dispose(false);
        //}
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!disposed)
        //    {
        //        if (disposing)
        //        {
        //            //释放托管资源
        //            //DbSet = null;                        
        //        }
        //        if (DbSet != null)
        //        {                    
        //            DbSet = null;
        //        }
        //        //是否非托管资源
        //        //if (DbContext != null)
        //        //{                    
        //        //    DbContext.Dispose();
        //        //    DbContext = null;
        //        //}


        //        disposed = true;
        //    }
        //}

        #endregion
    }
}
