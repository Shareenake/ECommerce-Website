using Newtonsoft.Json;
using OnlineShoppingStore.DAL;
using OnlineShoppingStore.Models;
using OnlineShoppingStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Categories()
        {
            List<Category> AllCategories = _unitOfWork.GetRepositoryInstance<Category>().GetAllRecordsQueryable().Where(i => i.IsDelete == false).ToList();
            return View(AllCategories);
        }
        public ActionResult AddCategory()
        {
           // return UpdateCategory(0);
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category tblCategory)
        {
            _unitOfWork.GetRepositoryInstance<Category>().Add(tblCategory);
            return RedirectToAction("Categories");
        }
        public ActionResult UpdateCategory(int CategoryId)
        {
            CategoryDetails CD; 
                if (CategoryId != null)
                {
                    CD = JsonConvert.DeserializeObject<CategoryDetails>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Category>().GetFirstOrDefault(CategoryId)));

                }
                else
                {
                    CD = new CategoryDetails();
                }
                return View("UpdateCategory", CD);
           
        }
        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> List = new List<SelectListItem>();
            var Category = _unitOfWork.GetRepositoryInstance<Category>().GetAllRecords();
            foreach(var item in Category)
            {
                List.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            return List;
        }
       
        
        public ActionResult CategoryEdit(int CategoryId)
        {
           
            return View(_unitOfWork.GetRepositoryInstance<Category>().GetFirstOrDefault(CategoryId));
        }
        [HttpPost]
        public ActionResult CategoryEdit(Category tblCategory)
        {

            _unitOfWork.GetRepositoryInstance<Category>().Update(tblCategory);
            return RedirectToAction("Categories");
        }



        public ActionResult Product()
        {

            return View(_unitOfWork.GetRepositoryInstance<Product>().GetProduct());
        }
        public ActionResult ProductEdit(int ProductId)
        {
            ViewBag.CategoryList = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<Product>().GetFirstOrDefault(ProductId));
        }
        [HttpPost]
        public ActionResult ProductEdit(Product tblProduct, HttpPostedFileBase file)
        {
            string Pic = null;
            if (file != null)
            {
                Pic = System.IO.Path.GetFileName(file.FileName);
                string Path = System.IO.Path.Combine(Server.MapPath("~/ProductImage"), Pic);
                file.SaveAs(Path);
            }
            tblProduct.ProductImage =file!=null? Pic: tblProduct.ProductImage;
            tblProduct.ModifiedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Product>().Update(tblProduct);
            return RedirectToAction("Product");
        }
        public ActionResult ProductAdd()
        {
            ViewBag.CategoryList = GetCategory();
            return View();
        }
        [HttpPost]
        public ActionResult ProductAdd(Product tblProduct,HttpPostedFileBase file)
        {
            string Pic=null;
            if (file!=null)
            {
                Pic = System.IO.Path.GetFileName(file.FileName);
                string Path = System.IO.Path.Combine(Server.MapPath("~/ProductImage"), Pic);
                file.SaveAs(Path);
            }
            tblProduct.ProductImage = Pic;
            tblProduct.CreatedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Product>().Add(tblProduct);
            return RedirectToAction("Product");
        }
    }
}