using Businessdevweb.Extensions;
using Businessdevweb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Businessdevweb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.ContactUs = db.ContactUs.ToList();
            return View();
        }
        public ActionResult DeleteMessage(string Id)
        {
            try
            {
                db.ContactUs.Remove(db.ContactUs.Find(Id));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {

                throw;
            }

        }
        public ActionResult AboutMe()
        {
            if(db.AboutMe.Count()==0)
            {
                db.AboutMe.Add(new Businessdevweb.Models.AboutMe
                {
                    Content="",
                    Title ="درباره ما" 

                });
                db.SaveChanges();
            }
              
            return View(db.AboutMe.FirstOrDefault());
        }
        public ActionResult Setting()
        {
            if (db.Settings.Count() == 0)
            {
                db.Settings.Add(new Setting
                {
                    Description = "فروشگاه اینترنتی تاپ کالا",
                    Phone="02133333333" ,
                    Email="Email@tp.ir",
                    DescriptionCall= "هفت روز هفته ، 24 ساعت شبانه‌روز پاسخگوی شما هستیم.",
                     LogoTitle= "تاپ کالا",
                    Title = "فروشگاه اینترنتی تاپ کالا",
                    Copyright="کلیه حقوق مادی و معنوی سایت مربوط به فروشگاه تاپ کالا می باشد",
                    
                    IsActive = true

                }) ;
                db.SaveChanges();
            }

            return View(db.Settings.FirstOrDefault());
        }
        [HttpPost]
        public ActionResult ChangePicture(HttpPostedFileBase file, string id)
        {
            if (file == null)
            {
                return new JsonResult { Data = new { type = "error", result = "تصویری را انتخاب نکرده اید" } };
            }
            try
            {
                var setting = db.Settings.Find(id);
                if (setting.LogoFile != null)
                {
                    FileUploader.DeleteFile("/uploadFiles/Site/" + setting.LogoFile);
                }
                setting.LogoFile = FileUploader.SaveFile(file, "/uploadFiles/Site/", setting.LogoTitle);
                if (setting.LogoFile == null)
                {
                    return new JsonResult { Data = new { type = "error", result = "به دلیل خطایی نا مشخص فایل تغییر نکرد" } };
                }
                db.Entry(setting).State = EntityState.Modified;
                db.SaveChanges();

                return new JsonResult { Data = new { type = "success", result = "تصویر با موفقیت تغییر کرد." } };
            }
            catch (Exception e)
            {

                return new JsonResult { Data = new { type = "error", result = e.Message } };
            }

        }

        public ActionResult ContactUs()
        {
            if (db.ContactUsPage.Count() == 0)
            {
                db.ContactUsPage.Add(new Businessdevweb.Models.ContactUsPage
                {
                    Content = "",
                    Title = "ارتباط با ما"

                });
                db.SaveChanges();
            }

            return View(db.ContactUsPage.FirstOrDefault());
        }
          [HttpPost]
          [ValidateAntiForgeryToken]
        public ActionResult AboutMe(AboutMe model)
        {
            var ab = db.AboutMe.FirstOrDefault();
            ab.Title = model.Title;
            ab.Content = model.Content;
            ab.UpdateTime = DateTime.Now;
            db.Entry(ab).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactUs(ContactUsPage model)
        {
            var cp= db.ContactUsPage.FirstOrDefault();
            cp.Title = model.Title;
            cp.Content = model.Content;
            cp.UpdateTime = DateTime.Now;
            db.Entry(cp).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Setting(Setting model)
        {
            var cp = db.Settings.FirstOrDefault();
            cp.Title = model.Title;
            cp.Description = model.Description;
            cp.Copyright =model.Copyright;
            cp.DescriptionCall = model.DescriptionCall;
            cp.Email = model.Email;
            cp.Phone = model.Phone;
            cp.LogoTitle = model.LogoTitle;
            cp.IsActive = model.IsActive;
            cp.UpdateTime = DateTime.Now;
            db.Entry(cp).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
