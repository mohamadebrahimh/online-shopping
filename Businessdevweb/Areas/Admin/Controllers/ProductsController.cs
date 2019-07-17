using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Businessdevweb.Extensions;
using Businessdevweb.Models;

namespace Businessdevweb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Helper
        private SelectList GetSelectListCategories(string selectedValue = null)
        {
            return new SelectList(db.ProductCategories
                       .Where(m => m.IsEndChild)
                       .Select(m => new
                       {
                           m.Title,
                           m.Id,
                           Group = (m.ParentId == null ? "دسته اصلی" : m.Parent.Title)
                       }).ToList(),
                       dataValueField: "Id",
                       dataTextField: "Title",
                       dataGroupField: "Group",
                       selectedValue: selectedValue);
        }

        #endregion

        // GET: Admin/Products/id =>ProductCategory id
        public ActionResult Index(string id)//ProductCategory id
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productCategory = db.ProductCategories.Find(id);
            if (productCategory==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(productCategory);
        }
        // GET: Admin/Products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var products = db.Products.Where(m => m.CategoryId == id).ToList();

            var Category = (from cat in db.ProductCategories
                            where cat.Id == id
                            select new
                            {
                                cat.Id,
                                cat.Title
                            }).SingleOrDefault();
            ViewData["CategoryTitle"] = Category.Title;
            ViewData["CategoryId"] = Category.Id;
            return View(products);
        }

        // GET: Admin/Products/Create
        public ActionResult Create(string id=null)
        {

       
            ViewData["Category"] = db.ProductCategories.Find(id);
            ViewBag.CategoryId = GetSelectListCategories(id);
            return View();
        }
        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IsDiscount,IsActive,DiscountPercent,Code,Qty,FirstTitle,SecondTitle,Description,Price,Brand,CategoryId,MetaDescription,MetaKeyWord")] Product model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                 var regx = new Regex("([\\W_]+)+?", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            
                db.Products.Add(new Product
                {
                    Brand = model.Brand,
                    Qty= model.Qty,
                    IsDiscount= model.Price>0?model.IsDiscount:false,
                    DiscountPercent= model.Price>0?model.DiscountPercent:0,
                    Price=model.Price,
                    CategoryId = model.CategoryId,
                    Code = model.Code,
                    FirstTitle = model.FirstTitle,
                    SecondTitle = model.SecondTitle,
                    Name = regx.Replace(model.FirstTitle.Trim(), "-"),
                    Description = model.Description,
                    MetaDescription = model.MetaDescription,
                    MetaKeyWord = model.MetaKeyWord,  
                     IsActive=model.IsActive
                });
                db.SaveChanges();
                return RedirectToAction("Index", new { Id = model.CategoryId });
                }
                catch (Exception e)
                {
                
                    ViewData["Category"] = db.ProductCategories.Find(model.CategoryId);
                    ModelState.AddModelError("", e.Message);
                    ViewBag.CategoryId = GetSelectListCategories(model.CategoryId);
                    return View(model);
                }

            }

            ViewData["Category"] = db.ProductCategories.Find(model.CategoryId);
            ViewBag.CategoryId = GetSelectListCategories(model.CategoryId);
            return View(model);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(string id)  //product Id
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Products.Find(id);
      
           
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewData["Category"] = product.Category;
            ViewBag.CategoryId = GetSelectListCategories(product.CategoryId);
            //ViewBag.CategoryId = Repos.SelectListOfCategories(product.CategoryId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IsActive,IsDiscount,DiscountPercent,Code,Qty,FirstTitle,SecondTitle,Description,Price,Brand,CategoryId,MetaDescription,MetaKeyWord")] Product model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var regx = new Regex("([\\W_]+)+?", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    var product = db.Products.Find(model.Id);
                    product.Code = model.Code;
                    product.Qty = model.Qty;
                    product.IsDiscount = model.Price > 0 ? model.IsDiscount : false;
                    product.DiscountPercent = model.Price > 0 ? model.DiscountPercent : 0;
                    product.Price = model.Price;
                    product.FirstTitle = model.FirstTitle;
                    product.Name = regx.Replace(model.FirstTitle.Trim(), "-");
                    product.SecondTitle = model.SecondTitle;
                    product.Description = model.Description;
                    product.MetaDescription = model.MetaDescription;
                    product.MetaKeyWord = model.MetaKeyWord;
                    product.Brand = model.Brand;
                    product.CategoryId = model.CategoryId;
                    product.IsActive = model.IsActive;
                    product.UpdateTime = DateTime.Now;
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { Id = model.CategoryId });
                }
                catch (Exception e)
                {
          
                    ViewData["Category"] = db.ProductCategories.Find(model.CategoryId);
                    ModelState.AddModelError("", e.Message);
                    ViewBag.CategoryId = GetSelectListCategories(model.CategoryId);
                    return View(model);
                }
              
            }

            ViewData["Category"] = db.ProductCategories.Find(model.CategoryId);
            ViewBag.CategoryId = GetSelectListCategories(model.CategoryId);
            return View(model);
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
                var product = db.Products.Find(id);
                if (product.ImageFile!=null)
                {
                        FileUploader.DeleteFile("/uploadFiles/Products/" + product.ImageFile);
                }
              
                product.ImageFile = FileUploader.SaveFile(file, "/uploadFiles/Products/",product.FirstTitle);
                if (product.ImageFile == null)
                {
                    return new JsonResult { Data = new { type = "error", result = "به دلیل خطایی نا مشخص فایل تغییر نکرد" } };
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                return new JsonResult { Data = new { type = "success", result = "تصویر با موفقیت تغییر کرد." } };
            }
            catch (Exception e)
            {

                return new JsonResult { Data = new { type = "error", result = e.Message } };
            }

        }
        // GET: Admin/Products/Delete/5
        [HttpPost]
        public JsonResult Delete(string id)
        {

            try
            {
                // TODO: Add delete logic here
                Product product = db.Products.Find(id);
                if (product.ImageFile!=null)
                {
                    FileUploader.DeleteFile("/uploadFiles/Products/" + product.ImageFile);

                }
                if (product.ProductImages.Count>0)
                {
                    foreach (var item in product.ProductImages)
                    {
                        FileUploader.DeleteFile("/uploadFiles/ProductImages/" + item.Name);
                    
                    }
                }
                db.ProductImages.RemoveRange(product.ProductImages);
                db.Products.Remove(product);
                db.SaveChanges();
                return new JsonResult { Data = new { type = "success", title = "صحیح انجام شد!", message = "آیتم مورد نظر حذف شد.", id } };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = new { type = "error", title = "خطا!", message = e.Message, id } };
            }

        }


        public JsonResult Active(string id)
        {

            try
            {
                // TODO: Add delete logic here
                Product product = db.Products.Find(id);
                product.IsActive = !product.IsActive;
                db.Entry(product).State=EntityState.Modified;
                db.SaveChanges();
                return new JsonResult { Data = new { type = "success", title = "صحیح انجام شد!", message = "آیتم مورد نظر "+(product.IsActive?"فعال":"غیر فعال")+" شد.", id } };
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
    }
}
