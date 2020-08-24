
using OnlineShoppingStore.DAL;
using OnlineShoppingStore.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;



namespace OnlineShoppingStore.Models.Home
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Product> ListOfProducts { get; set; }
        OnlineShopingStoreEntities Context = new OnlineShopingStoreEntities();
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        public  HomeIndexViewModel CreateModel(string Search,int PageSize, int? Page)
        {
           
            SqlParameter[] Parameter = new SqlParameter[]
            {
                new SqlParameter("@Search",Search??(object)DBNull.Value)
            };
            IEnumerable<Product> Data = Context.Database.SqlQuery<Product>("GetBySearch", Parameter).ToList().ToPagedList(Page ?? 1, PageSize);

            return new HomeIndexViewModel
            {
                ListOfProducts = Data

            };
        }
    }
}