using Businessdevweb.Extensions;
using Businessdevweb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Businessdevweb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SliderImagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

   
        // GET: Admin/Categories
        public ActionResult Index(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }

            return View(slider);


        }



        // GET: Admin/Categories/Create
        public ActionResult Create(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewData["Slider"] = db.Sliders.Find(id);
            return View();

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
                var img = db.SliderImages.Find(id);
                FileUploader.DeleteFile("/uploadFiles/SliderImages/" + img.Name);
                img.Name = FileUploader.SaveFile(file, "/uploadFiles/SliderImages/", img.Title);
                if (img.Name == null)
                {
                    return new JsonResult { Data = new { type = "error", result = "به دلیل خطایی نا مشخص فایل تغییر نکرد" } };
                }
                db.Entry(img).State = EntityState.Modified;
                db.SaveChanges();

                return new JsonResult { Data = new { type = "success", result = "تصویر با موفقیت تغییر کرد." } };
            }
            catch (Exception e)
            {

                return new JsonResult { Data = new { type = "error", result = e.Message } };
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, [Bind(Include = "URLPage,SliderId,Title")] SliderImage model)

        {
            if (file != null)
            {
                try
                {
                    var name = FileUploader.SaveFile(file, "/uploadFiles/SliderImages/", model.Title, true);
                    if (name==null)
                    {
                        throw new ArgumentException("فایل مورد نظر ذخیره نگردید دوباره امتحان نمایید");
                    }
                    var position = db.SliderImages.Where(m => m.SliderId == model.SliderId).OrderBy(m => m.InsertTime).Select(m => m.Position).ToList().Count();
                    db.SliderImages.Add(new SliderImage
                    {
                        URLPage = model.URLPage,
                        Name = name,
                        SliderId = model.SliderId,
                        Title = model.Title ,
                        Position=position
                    });
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = model.SliderId });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    ViewData["Slider"] = db.Sliders.Find(model.SliderId);
                    return View();
                }

            }

            ModelState.AddModelError("", "باید تصویر انتخاب شود");
            ViewData["Slider"] = db.Sliders.Find(model.SliderId);
            return View();
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = db.SliderImages.Find(id);

            if (model == null)
            {
                return HttpNotFound();
            }
            ViewData["Slider"] = db.Sliders.Find(model.SliderId);
            return View(model);
        }

     [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase file, [Bind(Include = "Id,URLPage,SliderId,Title")] SliderImage model)
        {
            if (file == null)
            {
                ViewData["Slider"] = db.Sliders.Find(model.SliderId);
                ModelState.AddModelError("", "تصویر انتخاب نگردیده است");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var imge = db.SliderImages.Find(model.Id);
                    FileUploader.DeleteFile("/uploadFiles/SliderImages/" + imge.Name);
                    var name = FileUploader.SaveFile(file, "/uploadFiles/ProductImages/", model.Title, true);
                    if (name==null)
                    {
                        throw new ArgumentException("تصویر به دلیل نا معلومی ذخیره نشد");
                    }
                    imge.Title = model.Title;
                    imge.URLPage = model.URLPage;
                    imge.SliderId = model.SliderId;
                    imge.Name = name;
                    imge.UpdateTime = DateTime.Now;
                    db.Entry(imge).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = model.SliderId });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    ViewData["Slider"] = db.Sliders.Find(model.SliderId);
                    return View(model);
                }

            }
            ViewData["Slider"] = db.Sliders.Find(model.SliderId);
            return View(model);

        }

        // GET: Admin/Categories/Delete/5
        [HttpPost]
        public JsonResult Delete(string id)
        {

            try
            {
                // TODO: Add delete logic here
                var img = db.SliderImages.Find(id);
                FileUploader.DeleteFile("/uploadFiles/SliderImages/" + img.Name);
                db.SliderImages.Remove(img);
                db.SaveChanges();
                return new JsonResult { Data = new { type = "success", title = "صحیح انجام شد!", message = "آیتم مورد نظر حذف شد.", id } };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = new { type = "error", title = "خطا!", message = e.Message, id } };
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Helper

        #endregion
    }
}