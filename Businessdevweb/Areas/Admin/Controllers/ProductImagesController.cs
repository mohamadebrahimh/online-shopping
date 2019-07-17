using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Businessdevweb.Extensions;
using Businessdevweb.Models;

namespace Businessdevweb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductImagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

    

        // GET: Admin/Categories
        public ActionResult Index(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product= db.Products.Find(id);
  
          
            return View(product);


        }



        // GET: Admin/Categories/Create
        public ActionResult Create(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewData["Product"] = db.Products.Find(id); 
            return View();

        }
        [HttpPost]
        public ActionResult ChangePicture(HttpPostedFileBase file, string id)
        {
            if (file == null)
            {
                return new JsonResult {Data= new { type = "error", result = "تصویری را انتخاب نکرده اید" } };
            }
            try
            {
               var img= db.ProductImages.Find(id);
                FileUploader.DeleteFile("/uploadFiles/ProductImages/" + img.Name);
                img.Name = FileUploader.SaveFile(file, "/uploadFiles/ProductImages/",  img.Title);
                if (img.Name==null)
                {
                    return new JsonResult { Data = new { type = "error", result = "به دلیل خطایی نا مشخص فایل تغییر نکرد" } };
                }
                db.Entry(img).State = EntityState.Modified;
                db.SaveChanges();

                return new JsonResult { Data=new { type = "success", result = "تصویر با موفقیت تغییر کرد." } };
            }
            catch (Exception e)
            {

                return new JsonResult { Data=new { type = "error", result = e.Message } };
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, string productId,string title)

        {
            if (file!=null)
            {
                try
                {     
                    var name = FileUploader.SaveFile(file, "/uploadFiles/ProductImages/", title,true);

                    db.ProductImages.Add(new ProductImage {
                    Name=name,
                    ProductId=productId,
                    Title= title
                    });
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = productId });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    ViewData["Product"] = db.Products.Find(productId);
                    return View();
                }

            }

            ModelState.AddModelError("", "باید تصویر انتخاب شود");
            ViewData["Product"] = db.Products.Find(productId);
            return View();
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
         
            var model = db.ProductImages.Find(id);
           
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewData["Product"] = db.Products.Find(model.ProductId);
            return View(model);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase file, [Bind(Include = "Id,ProductId,Title")] ProductImage model)
        {
            if (file == null)
            {
                ViewData["Product"] = db.Products.Find(model.ProductId);
                ModelState.AddModelError("", "تصویر انتخاب نگردیده است");
                return View(model);
            }

                if (ModelState.IsValid)
            {
                try
                {

                    var imge = db.ProductImages.Find(model.Id);
                    FileUploader.DeleteFile("/uploadFiles/ProductImages/" + imge.Name);
                    var name = FileUploader.SaveFile(file, "/uploadFiles/ProductImages/", model.Title, true);
                    if (name==null)
                    {
                        throw new ArgumentException("تصویر به دلیل نا معلومی ذخیره نشد");
                    }
                    imge.Title = model.Title;
                    imge.ProductId = model.ProductId;
                    imge.Name = name;
                    imge.UpdateTime = DateTime.Now;
                    db.Entry(imge).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = model.ProductId });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    ViewData["Product"] = db.Products.Find(model.ProductId);
                    return View(model);
                }

            }
            ViewData["Product"] = db.Products.Find(model.ProductId);
            return View(model);

        }

        // GET: Admin/Categories/Delete/5
        [HttpPost]
        public JsonResult Delete(string id)
        {

            try
            {
                // TODO: Add delete logic here
                ProductImage img = db.ProductImages.Find(id);
                FileUploader.DeleteFile("/uploadFiles/ProductImages/" + img.Name);
                db.ProductImages.Remove(img);
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
