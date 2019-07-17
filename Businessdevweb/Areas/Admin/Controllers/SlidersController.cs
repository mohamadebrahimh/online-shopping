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
    public class SlidersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();



        public ActionResult Index()
        {

            var sliders = db.Sliders.ToList();

            return View(sliders);


        }




        public ActionResult Create()
        {

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IsHome,IsActive,IsPage,IsProduct,Title")] Slider model)

        {
            if (ModelState.IsValid)
            {
                try
                {
                    var slider = new Slider
                    {
                        IsHome = model.IsHome,
                        IsActive = model.IsActive,
                        IsPage = model.IsPage,
                        IsProduct = model.IsProduct,
                  
                        Title = model.Title

                    };
                    db.Sliders.Add(slider);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);

                    return View(model);
                }

            }

            return View(model);
        }


        public ActionResult Edit(string id)
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

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,IsHome,IsActive,IsPage,IsProduct")] Slider model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var slider = db.Sliders.Find(model.Id);
                    slider.IsHome = model.IsHome;
                    slider.IsActive = model.IsActive;
                    slider.IsPage = model.IsPage;
                    slider.IsProduct = model.IsProduct;
      
                    slider.Title = model.Title;

                    slider.UpdateTime = DateTime.Now;
                    db.Entry(slider).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                
                    return View(model);
                }

            }

            return View(model);

        }


        [HttpPost]
        public JsonResult Delete(string id)
        {

            try
            {
                // TODO: Add delete logic here
                var slider = db.Sliders.Find(id);
                if (slider.SliderImages.Count>0)
                {
                    foreach (var item in slider.SliderImages)
                    {
                        FileUploader.DeleteFile("/uploadFiles/SliderImages/" + item.Name);
                    }
                    db.SliderImages.RemoveRange(slider.SliderImages);
                }
                db.Sliders.Remove(slider);
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