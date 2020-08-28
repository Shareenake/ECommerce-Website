using OnlineShoppingStore;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult PaymentWithPaypal()
        {
            APIContext Apicontext = PayPalConfiguration.GetAPIContext();
            try
            {
                string PayerId = Request.Params["PayerID"];
                if(string.IsNullOrEmpty(PayerId))
                {
                    string baseUri = Request.Url.Scheme + "://" + Request.Url.Authority + "PaymentWithPaypal/PaymentWithPaypal?";
                    var Guid = Convert.ToString(new Random().Next(100000000));
                    var CreatePayment = this.CreatePayment(Apicontext, baseUri + "guid=" + Guid);
                    var Link = CreatePayment.links.GetEnumerator();
                    string PaypalRedirectUrl=null;

                    while(Link.MoveNext())
                    {
                        Links lnk = Link.Current;
                        if(lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            PaypalRedirectUrl = lnk.href;
                        }
                    }

                }
                else
                {
                    var Guid = Request.Params["guid"];
                    var executePayment=ExecutePayment(Apicontext,PayerId,Session[Guid]as string);
                    if (executePayment.ToString().ToLower() != "approved")
                    {
                        return View("FailureView");
                    }

                }
            }
            catch(Exception ex)
            { return View("FailureView"); }
            return View("SuccessView");
        }

        private object ExecutePayment(APIContext apicontext, string payerId, string v)
        {
            var PaymentExecution = new PaymentExecution() { payer_id = payerId };
        }

        private Payment CreatePayment(APIContext apicontext, string RedirectUrl)
        {
            var item_list = new ItemList() { items = new List<Item>() };
            if(Session["Cart"]!=null)
            {
                List<Models.Home.Item> Cart = (List<Models.Home.Item>)Session["Cart"];
                foreach (var item in Cart)
                {
                    item_list.items.Add(new Item()
                    {
                        name = item.Products.productName.ToString(),
                        currency="QR",
                        price=item.Products.Price.ToString(),
                        quantity=item.Products.Quantity.ToString(),
                        sku="sku"
                    });
                    
                }
                var Payer = new Payer { payment_method = "paypal" };
                var RedirUrl = new RedirectUrls()
                {
                    cancel_url = RedirectUrl = "&cancel=true",
                    return_url = RedirectUrl
                };
                var details = new Details()
                {
                    tax = "1",
                    shipping = "1",
                    subtotal = "1"

                };
                var Amount = new Amount()
                {
                    currency = "USD",
                    total = Session["SessGotal"].ToString(),
                    details = details
                };

                var TransactionList = new List<Transaction>();
                TransactionList.Add(new Transaction()
                {
                    description = "Transaction description",
                    invoice_number = "#1000000",
                    amount = Amount,
                    item_list=item_list
                });
                this.Pay = new Payment()
                {
                    intent="sale",
                    payer=Payer,
                    transactions=TransactionList,
                    redirect_urls=RedirUrl
                });


            }

            return this.payment.create

        }
    }
}