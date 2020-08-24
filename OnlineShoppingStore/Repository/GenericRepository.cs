using OnlineShoppingStore.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace OnlineShoppingStore.Repository
{
    public class GenericRepository<tbl_Enity> : iRepository<tbl_Enity> where tbl_Enity : class
    {
        DbSet<tbl_Enity> _dbSet;
        private OnlineShopingStoreEntities _DBEntity;
        public GenericRepository(OnlineShopingStoreEntities DBEntity)
        {
            _DBEntity = DBEntity;
            _dbSet = _DBEntity.Set<tbl_Enity>();
        }
       public  IEnumerable<tbl_Enity> GetProduct()
        {

            return _dbSet.ToList();
        }
        //public IEnumerable<tbl_Enity>
        public void Add(tbl_Enity entity)
        {
            _dbSet.Add(entity);
            _DBEntity.SaveChanges();
        }

        public int GetAllRecordCount()
        {
            return _dbSet.Count();
        }

        public IEnumerable<tbl_Enity> GetAllRecords()
        {
            return _dbSet.ToList();
        }

        public IQueryable<tbl_Enity> GetAllRecordsQueryable()
        {
            return _dbSet;
        }

        public tbl_Enity GetFirstOrDefault(int RecordId)
        {
            return _dbSet.Find(RecordId);
        }

        public tbl_Enity GetFirstOrderOrDefaultByParameter(Expression<Func<tbl_Enity, bool>> WherePredict)
        {
            return _dbSet.Where(WherePredict).FirstOrDefault();
        }

        public IEnumerable<tbl_Enity> GetListParmeter(Expression<Func<tbl_Enity, bool>> WherePredict)
        {
            return _dbSet.Where(WherePredict).ToList();
        }

        public IEnumerable<tbl_Enity> GetResultBySQLProcedure(string query, params object[] parameter)
        {
            if (parameter != null)
            {
                return _DBEntity.Database.SqlQuery<tbl_Enity>(query, parameter).ToList();
            }
            else
            {
                return _DBEntity.Database.SqlQuery<tbl_Enity>(query).ToList();
            }
        }

        public void InActiveAndDeleteMarkByWhereClause(Expression<Func<tbl_Enity, bool>> WherePredict, Action<tbl_Enity> ForEachPredict)
        {
            _dbSet.Where(WherePredict).ToList().ForEach(ForEachPredict);
        }

        public void Remove(tbl_Enity entity)
        {
            if (_DBEntity.Entry(entity).State == EntityState.Detached) _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }

        public void RemoveByWhereClause(Expression<Func<tbl_Enity, bool>> WherePredict)
        {
            tbl_Enity entity = _dbSet.Where(WherePredict).FirstOrDefault();
                

        }

        public void RemoveRangeByWhereClause(Expression<Func<tbl_Enity, bool>> WherePredict)
        {
            List<tbl_Enity> entity = _dbSet.Where(WherePredict).ToList();
            foreach(var ent in entity)
            {
                Remove(ent);
            }
        }

        public void Update(tbl_Enity entity)
        {
            _dbSet.Attach(entity);
            _DBEntity.Entry(entity).State = EntityState.Modified;
            _DBEntity.SaveChanges();
        }

        public void UpdateByWhereClause(Expression<Func<tbl_Enity, bool>> wherepredict, Action<tbl_Enity> ForEachPredict)
        {
            _dbSet.Where(wherepredict).ToList().ForEach(ForEachPredict);
        }

        public IEnumerable<tbl_Enity> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<tbl_Enity, bool>> wherePedict, Expression<Func<tbl_Enity, int>> orderByPredict)
        {
            if(wherePedict!=null)
                
            {
                return _dbSet.OrderBy(orderByPredict).Where(wherePedict).ToList();
            }
            else
            {
                return _dbSet.OrderBy(orderByPredict).ToList();
            }
        }
    }
}