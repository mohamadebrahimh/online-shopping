using Businessdevweb.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Businessdevweb.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ProductsController()
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
        [HttpPost]
        public ActionResult AddComment(string Message, string ReplyId, string ProductId, string returnUrl)
        {
            db.Comments.Add(new Comment
            {
                Message = Message,
                ReplyId = (ReplyId == "" ? null : ReplyId),
                ProductId = ProductId,
                UserId = User.Identity.GetUserId()
            });
            db.SaveChanges();
            return RedirectToLocal(returnUrl);
        }

        public ActionResult Index(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = db.Products.FirstOrDefault(m => m.Name == id);
            if (product == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(product);
        }
        public ActionResult Category(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = db.ProductCategories.FirstOrDefault(m => m.Name == id);
            if (category == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewData["ProductCategory"] = category;
            return View();
        }

        public ActionResult AddToCart(string id, string returnUrl=null)
        {
           
            if (Session["ShoppingCart"] == null)
            {
                var product = db.Products.Find(id);
                var shopingCart = new List<ShopingCart>();
                shopingCart.Add(new ShopingCart
                {
                    ImageFile = product.ImageFile,
                    OrderId = null,
                    Price = product.IsDiscount && product.DiscountPercent > 0 ? product.PriceWithDiscount : product.Price,
                    ProductId = product.Id,
                    ProductTitle = product.FirstTitle  ,
                    Qty = 1,
                }) ;
                Session["ShoppingCart"] = shopingCart;


            }
            else
            {

                var shoppingcart = Session["ShoppingCart"] as List<ShopingCart>;
                if (shoppingcart.Any(m => m.ProductId == id))
                {
                    var mdl = shoppingcart.FirstOrDefault(m => m.ProductId == id);
                    shoppingcart.Remove(mdl);
                    mdl.Qty++;
                    shoppingcart.Add(mdl);
                    Session["ShoppingCart"] = shoppingcart;
                }
                else
                {
                    var orderId = Session["OrderId"] as string;
                    var product = db.Products.Find(id);
                    shoppingcart.Add(new ShopingCart
                    {
                        ImageFile = product.ImageFile,
                        OrderId = orderId??null,
                        Price = product.IsDiscount && product.DiscountPercent > 0 ? product.PriceWithDiscount : product.Price,
                        ProductId = product.Id,
                        ProductTitle = product.FirstTitle,
                        Qty = 1,
                    });
                    Session["ShoppingCart"] = shoppingcart;

                }
            }



            return RedirectToLocal(returnUrl);
        }
 
        public ActionResult DeleteItemCart(string id, string returnUrl = null)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Session["ShoppingCart"] == null)
            {
     


            }
            else
            {

                var shoppingcart = Session["ShoppingCart"] as List<ShopingCart>;
                if (shoppingcart.Any(m => m.ProductId == id))
                {
                    var mdl = shoppingcart.FirstOrDefault(m => m.ProductId == id);
                    shoppingcart.Remove(mdl);
        
                    Session["ShoppingCart"] = shoppingcart;
                }
      
            }



            return RedirectToLocal(returnUrl);
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}