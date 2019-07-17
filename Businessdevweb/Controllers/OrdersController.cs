using Businessdevweb.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Businessdevweb.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        public OrdersController()
        {
            var setting = db.Settings.FirstOrDefault();
            if (setting != null)
            {
                ViewBag.SiteTitle = setting.Title;
                ViewBag.LogoFile = setting.LogoFile;
                ViewBag.LogoTitle = setting.LogoTitle;
                ViewBag.SiteDescription = setting.Description;
                ViewBag.DescriptionCall = setting.DescriptionCall;
                ViewBag.Phone = setting.Phone;
                ViewBag.Email = setting.Email;
                ViewBag.Copyright = setting.Copyright;
            }
            ViewBag.Categories = db.ProductCategories.Where(m => m.ParentId == null).ToList();
        }
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Orders
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Payment(string id)
        {
            return View(db.Orders.Find(id));
        }
        public ActionResult Success(string id,long? price)
        {
            var userid = User.Identity.GetUserId();
            var order = db.Orders.Find(id);
            db.Payments.Add(new Models.Payment
            {
                Bank = "ملت",
                OrderId = order.Id,
                UserId = userid,
                Price = price,
                Status = 2,
                RefId = "1233",

            });
            order.Status = 1;
            db.Entry(order).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
         [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Address,PhoneNumber")] Order model)
        {
            var shops= Session["ShoppingCart"] as List<Businessdevweb.Models.ShopingCart>;
            if (shops==null)
            {
                return HttpNotFound();
            }
            var userid = User.Identity.GetUserId();
            var orderId = Session["OrderId"] as string;
            var order = new Order
            {
                UserId = userid,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Status = 0,
               
            };
         
            if (orderId==null)
            {
               
                  db.Orders.Add(order);
                orderId = order.Id;
            }

            var items = new List<OrderItem>();
            foreach (var item in shops)
            {
                var product = db.Products.Find(item.ProductId);
                items.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    IsDiscount = product.IsDiscount,

                    DiscountPercent = product.DiscountPercent,
                    Price = product.Price,
                    OrderId = orderId,
                    Qty = item.Qty ,
                     
                      
                });
            }
            if (items.Count()>0)
            {
                db.OrderItems.AddRange(items);
            }
            db.SaveChanges();
            Session["ShoppingCart"] = new List<Businessdevweb.Models.ShopingCart>();
            return RedirectToAction("Payment",new { id =orderId});
        }

    }
}