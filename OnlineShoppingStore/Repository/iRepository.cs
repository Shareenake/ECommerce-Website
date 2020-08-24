using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace OnlineShoppingStore.Repository
{
    public interface iRepository<tbl_Enity> where tbl_Enity : class
    {
        IEnumerable<tbl_Enity> GetProduct();
        IEnumerable<tbl_Enity> GetAllRecords();
        //IEnumerable<tbl_Enity> GetProductById(int ProductId);
        IQueryable<tbl_Enity> GetAllRecordsQueryable();
        int GetAllRecordCount();
        void Add(tbl_Enity entity);

        void Update(tbl_Enity entity);
        void UpdateByWhereClause(Expression<Func<tbl_Enity, bool>> wherepredict, Action<tbl_Enity> ForEachPredict);
        tbl_Enity GetFirstOrDefault(int RecordId);
        void Remove(tbl_Enity entity);
        void RemoveByWhereClause(Expression<Func<tbl_Enity, bool>> WherePredict);
        void RemoveRangeByWhereClause(Expression<Func<tbl_Enity, bool>> WherePredict);
        void InActiveAndDeleteMarkByWhereClause(Expression<Func<tbl_Enity, bool>> WherePredict, Action<tbl_Enity> ForEachPredict);
        tbl_Enity GetFirstOrderOrDefaultByParameter(Expression<Func<tbl_Enity, bool>> WherePredict);
        IEnumerable<tbl_Enity> GetListParmeter(Expression<Func<tbl_Enity, bool>> WherePredict);
        IEnumerable<tbl_Enity> GetResultBySQLProcedure(string query, params object[] parameter);

        IEnumerable<tbl_Enity> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<tbl_Enity, bool>> wherePedict, Expression<Func<tbl_Enity, int>> orderByPredict);

    }
}