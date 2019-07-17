using Businessdevweb.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Businessdevweb.Controllers
{
    public partial class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public HomeController()
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
        public virtual ActionResult Index()

        {

            ViewBag.ShoppingCart = Session["ShoppingCart"] == null ? null : Session["ShoppingCart"] as List<ShopingCart>;
            ViewData["Slider"] = db.Sliders.OrderByDescending(m=>m.UpdateTime).FirstOrDefault(m => m.isDeleted == false && m.IsActive && m.IsHome);
            ViewData["Products"] = db.Products.OrderByDescending(m => m.UpdateTime).ToList();
            ViewData["ProductCategories"] = db.ProductCategories.Where(m => m.IsEndChild).ToList();
            return View();
        }

        public virtual ActionResult About()
        {
            ViewData["Setting"] = db.Settings.FirstOrDefault();
            return View(db.AboutMe.FirstOrDefault());
        }

        public virtual ActionResult Contact()
        {
            ViewData["Setting"] = db.Settings.FirstOrDefault();
            return View(db.ContactUsPage.FirstOrDefault());
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual ActionResult Contact([Bind(Include = "FullName,Email,Phone,Message")] ContactUs contactUs)
        {
            if (ModelState.IsValid)
            {
                ContactUs contact = new ContactUs
                {
                    FullName = contactUs.FullName,
                    Email = contactUs.Email,
                    Phone = contactUs.Phone,
                    Message = contactUs.Message
                };
                db.ContactUs.Add(contact);
                db.SaveChanges();
                ViewData["Categories"] = db.ProductCategories.Where(m => m.ParentId == null).ToList();
                ViewBag.Message = "پیام شما با موفقیت ارسال شد.";
                return View();
            }
            else
            {
                return View(contactUs);
            }
        }
    }
}