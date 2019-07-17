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
    public class ProductCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Helper
        private SelectList GetSelectListCategories(string selectedValue=null,string excludeValue=null)
        {
            return new SelectList(db.ProductCategories
                       .Where(m => !m.IsEndChild&&((excludeValue==null)?true:m.Id!=excludeValue&&m.ParentId!=excludeValue))
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

        // GET: Admin/Categories
        public ActionResult Index(string id = null)
        {

            var categories = db.ProductCategories.Where(m => m.ParentId == id).ToList();
            return View(categories);


        }



        // GET: Admin/Categories/Create
        public ActionResult Create(string id = null)
        {

            ViewBag.ParentId = GetSelectListCategories(id);
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
               var category= db.ProductCategories.Find(id);
                if (category.ImageFile!=null)
                {
                    FileUploader.DeleteFile("/uploadFiles/ProductCategories/" + category.ImageFile);
                }
                category.ImageFile = FileUploader.SaveFile(file, "/uploadFiles/ProductCategories/",  category.Title);
                if (category.ImageFile==null)
                {
                    return new JsonResult { Data = new { type = "error", result = "به دلیل خطایی نا مشخص فایل تغییر نکرد" } };
                }
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();

                return new JsonResult { Data=new { type = "success", result = "تصویر با موفقیت تغییر کرد." } };
            }
            catch (Exception e)
            {

                return new JsonResult { Data=new { type = "error", result = e.Message } };
            }

        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code,Title,Description,MetaDescription,MetaKeyWord,ParentId,IsEndChild")] ProductCategory model)

        {
            if (ModelState.IsValid)
            {
                try
                {
                    var regx = new Regex("([\\W_]+)+?", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    var position = db.ProductCategories.Where(m => m.ParentId == model.ParentId).OrderBy(m => m.InsertTime).Select(m => m.Position).ToList().Count();
                    ProductCategory category = new ProductCategory
                    {
                        Code = model.Code,
                        Title = model.Title,
                        Name = regx.Replace(model.Title.Trim(), "-"),
                        Description = model.Description,
                        MetaDescription = model.MetaDescription,
                        MetaKeyWord = model.MetaKeyWord,
                        Position = position + 1,
                        ParentId = model.ParentId,
                        IsEndChild = model.IsEndChild,

                    };
                    db.ProductCategories.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = model.ParentId });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    ViewBag.ParentId = GetSelectListCategories(model.ParentId);
                    return View(model);
                }

            }

            ViewBag.ParentId = GetSelectListCategories(model.ParentId);
            return View(model);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
         
            var category = db.ProductCategories.Find(id);
           
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentId = GetSelectListCategories(category.ParentId,id);
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name,Title,Description,MetaDescription,MetaKeyWord,ParentId,IsEndChild")] ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProductCategory category = db.ProductCategories.Find(model.Id);
                    category.Title = model.Title;
                    category.Description = model.Description;
                    category.MetaDescription = model.MetaDescription;
                    category.MetaKeyWord = model.MetaKeyWord;
                    category.ParentId = model.ParentId;
                    category.IsEndChild = model.IsEndChild;
                    category.UpdateTime = DateTime.Now;
                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = model.ParentId });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    ViewBag.ParentId = GetSelectListCategories(model.ParentId,model.Id);
                    return View(model);
                }

            }
            ViewBag.ParentId = GetSelectListCategories(model.ParentId,model.Id);
            return View(model);

        }

        // GET: Admin/Categories/Delete/5
        [HttpPost]
        public JsonResult Delete(string id)
        {

            try
            {
                // TODO: Add delete logic here
                ProductCategory category = db.ProductCategories.Find(id);
                FileUploader.DeleteFile("/uploadFiles/ProductCategories/" + category.ImageFile);
                db.ProductCategories.Remove(category);
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
