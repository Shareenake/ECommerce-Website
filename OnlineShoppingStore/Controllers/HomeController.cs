using OnlineShoppingStore.Models;
using OnlineShoppingStore.Models.Home;
using OnlineShoppingStore.Repository;
using OnlineShoppingStore.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class HomeController : Controller
    {
        OnlineShopingStoreEntities Context = new OnlineShopingStoreEntities();
        public ActionResult Index(string Search, int? Page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(Search,3,Page));
        }

        public ActionResult AddToCart(int ProductId)
        {
            if(Session["Cart"]==null)
            {
                List<Item> Cart = new List<Item>();
                var Product = Context.Products.Find(ProductId);
                Cart.Add(new Item()
                {
                    Products = Product,
                    Quantity = 1
                });
                Session["Cart"] = Cart;
            }

            else
            {
                List<Item> Cart = (List<Item>)Session["Cart"];
                var Product = Context.Products.Find(ProductId);
                foreach (var Item in Cart)
                {
                    if (Item.Products.ProductId == ProductId)
                    {
                        int PreQty = Item.Quantity;
                        Cart.Remove(Item);
                        Cart.Add(new Item()
                        {
                            Products = Product,
                            Quantity = PreQty + 1
                        });
                        break;
                    }
                    else
                    {
                        Cart.Add(new Item()
                        {
                            Products = Product,
                            Quantity = 1
                        });
                    }
                    
                }
                Session["Cart"] = Cart;

            }
           
            return Redirect("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult RemoveProductFromCart(int ProductId)
        {

            List<Item> Cart = (List<Item>)Session["Cart"];
            //var Product = Context.Products.Find(ProductId);
            foreach (var item in Cart)
            {
                if (item.Products.ProductId == ProductId)
                {
                    Cart.Remove(item);
                    break;
                }
            }

            Session["Cart"] = Cart;
            return Redirect("Index");
        }

        public ActionResult ShoppingCart()
        {
            return View();
        }
    
        public ActionResult CheckOut()
        {
            return View();
        }
        public ActionResult CheckoutDetails()
        {
            return View();
        }
    }
}