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
    public class ProductFeaturesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();



        // GET: Admin/Categories
        public ActionResult Index(string id )
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Products.Find(id);
            if (product == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(product);


        }



        // GET: Admin/Categories/Create
        public ActionResult Create(string id )
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Products.Find(id);
            if (product == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewData["Product"] = product;
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Value,Name")] ProductFeatures model)

        {
            if (ModelState.IsValid)
            {
                try
                {
                    var feature = new ProductFeatures
                    {
                         ProductId=model.ProductId,
                         Value=model.Value,
                        Name = model.Name
                    };
                    db.ProductFeatures.Add(feature);
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

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
         
            var feature = db.ProductFeatures.Find(id);
           
            if (feature == null)
            {
                return HttpNotFound();
            }
            ViewData["Product"] = feature.Product;
            return View(feature);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,Value,Name")] ProductFeatures model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var feature = db.ProductFeatures.Find(model.Id);
                    feature.Name = model.Name;
                    feature.Value = model.Value;
      
                    feature.UpdateTime = DateTime.Now;
                    db.Entry(feature).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = model.ProductId });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    ViewData["Product"] = db.Products.Find(model.Product);
                    return View(model);
                }

            }
            ViewData["Product"] = db.Products.Find(model.Product);
            return View(model);

        }

        // GET: Admin/Categories/Delete/5
        [HttpPost]
        public JsonResult Delete(string id)
        {

            try
            {
                // TODO: Add delete logic here
                var feature = db.ProductFeatures.Find(id);
     
                db.ProductFeatures.Remove(feature);
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
