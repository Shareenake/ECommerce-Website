using OnlineShoppingStore.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingStore.Repository
{
    public class GenericUnitOfWork:IDisposable
    {
        private OnlineShopingStoreEntities DBEntity = new OnlineShopingStoreEntities();
        public iRepository<tbl_EntityType> GetRepositoryInstance<tbl_EntityType>() where tbl_EntityType : class
        {
            return new GenericRepository<tbl_EntityType>(DBEntity);
        }

        public void SaveChanges()
        {
            DBEntity.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    DBEntity.Dispose();
                }
            }
            this.disposed=true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
    }
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
           
}