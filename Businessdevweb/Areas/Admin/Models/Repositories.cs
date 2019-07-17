using Businessdevweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Businessdevweb.Areas.Admin.Models
{

    public class Repositories
    {
        /// <summary>
        /// منطقه سازنده
        /// </summary>
        #region CTOR

        /// <summary>
        ///  سازنده
        /// </summary>
        public Repositories()
        {

        }

        #endregion CTOR

        /// <summary>
        /// منطقه پراپرتی ها
        /// </summary>
        #region Properties


        private ApplicationDbContext db = new ApplicationDbContext();

        #endregion Properties


        /// <summary>
        /// مخزن توابع کاربردی
        /// </summary>
        #region Methods

        /// <summary>
        /// چک می کنه که شناسه داده شده دسته ای از دسته محصولات است. که در صورت صحیح بودن درست را بر می گرداند 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>bool</returns>
        public bool IsCategoryId(string Id)
        {
            return db.ProductCategories.Find(Id) == null ? false : true;
        }

        /// <summary>
        /// لیستی از دسته ها را بر می گرداند
        /// </summary>
        /// <returns>SelectList</returns>
        public SelectList SelectListOfCategories()
        {
            return new SelectList((from C in db.ProductCategories
                                   select new { C.Id, C.Title, Group = C.Parent.Title }).ToList(), dataValueField: "Id", dataTextField: "Title", dataGroupField: "Group", selectedValue: null);
        }

        /// <summary>
        /// مقدار را برای انتخاب آیتم می گیرد و لیستی از دسته را برمی گرداند
        /// </summary>
        /// <param name="SelectValue"></param>
        /// <returns></returns>
        public SelectList SelectListOfCategories(string SelectValue)
        {
            return new SelectList((from C in db.ProductCategories
                                   select new { C.Id, C.Title, Group = C.Parent.Title }).ToList(), dataValueField: "Id", dataTextField: "Title", dataGroupField: "Group", selectedValue: SelectValue);
        }

        /// <summary>
        /// آپلود فایل در سرور
        /// </summary>
        /// <param name="file">فایل برای آپلود</param>
        /// <param name="PathCombine">مسیر ترکیب شده با مسیر بر روی حافظه </param>
        /// <param name="FileName">نام فایل به همراه پسوند</param>
        /// <returns></returns>
        public string AddFileToServer(HttpPostedFileBase file, string PathCombine, string FileName, string position)
        {
            try
            {
                if (!System.IO.Directory.Exists(PathCombine))
                {
                    System.IO.Directory.CreateDirectory(PathCombine);
                }
                if (System.IO.File.Exists(PathCombine + FileName))
                {
                    //System.IO.File.Delete(PathCombine + FileName);
                    return "Exists";
                }
                switch (position)
                {
                    case "Icon":
                        file.InputStream.ResizeImage(75, 50, PathCombine + FileName);
                        break;
                    case "Main":
                        file.InputStream.ResizeImage(700, 466, PathCombine + FileName);
                        if (!System.IO.Directory.Exists(PathCombine + "thumbnails/"))
                        {
                            System.IO.Directory.CreateDirectory(PathCombine + "thumbnails/");
                        }

                        if (!System.IO.Directory.Exists(PathCombine + "BigImages"))
                        {
                            System.IO.Directory.CreateDirectory(PathCombine + "BigImages");
                        }
                        file.InputStream.ResizeImage(75, 50, PathCombine + "thumbnails/" + FileName);
                        file.SaveAs(PathCombine + "BigImages/" + FileName);
                        break;
                    case "Primary":
                        if (System.IO.File.Exists(PathCombine + "desktop/" + FileName))
                        {
                            //System.IO.File.Delete(PathCombine + FileName);
                            return "Exists";
                        }
                        if (!System.IO.Directory.Exists(PathCombine + "desktop/"))
                        {
                            System.IO.Directory.CreateDirectory(PathCombine + "desktop/");
                        }
                        if (!System.IO.Directory.Exists(PathCombine + "Phone/"))
                        {
                            System.IO.Directory.CreateDirectory(PathCombine + "Phone/");
                        }
                        if (!System.IO.Directory.Exists(PathCombine + "tablet/"))
                        {
                            System.IO.Directory.CreateDirectory(PathCombine + "tablet/");
                        }
                        if (!System.IO.Directory.Exists(PathCombine + "insidepage/"))
                        {
                            System.IO.Directory.CreateDirectory(PathCombine + "insidepage/");
                        }
                        if (!System.IO.Directory.Exists(PathCombine + "icon/"))
                        {
                            System.IO.Directory.CreateDirectory(PathCombine + "icon/");
                        }
                        file.InputStream.ResizeImage(700, 466, PathCombine + "insidepage/" + FileName);
                        file.InputStream.ResizeImage(400, 266, PathCombine + "desktop/" + FileName);
                        file.InputStream.ResizeImage(280, 186, PathCombine + "tablet/" + FileName);
                        file.InputStream.ResizeImage(150, 100, PathCombine + "phone/" + FileName);
                        file.InputStream.ResizeImage(75, 50, PathCombine + "icon/" + FileName);
                        break;
                    case "BannerSlider":
                        if (System.IO.File.Exists(PathCombine + "desktop/" + FileName))
                        {
                            //System.IO.File.Delete(PathCombine + FileName);
                            return "Exists";
                        }
                        if (!System.IO.Directory.Exists(PathCombine + "desktop/"))
                        {
                            System.IO.Directory.CreateDirectory(PathCombine + "desktop/");
                        }

                        if (!System.IO.Directory.Exists(PathCombine + "Tablet/"))
                        {
                            System.IO.Directory.CreateDirectory(PathCombine + "Tablet/");
                        }
                        if (!System.IO.Directory.Exists(PathCombine + "Phone/"))
                        {
                            System.IO.Directory.CreateDirectory(PathCombine + "Phone/");
                        }
                        file.InputStream.ResizeImage(1368, 400, PathCombine + "desktop/" + FileName);
                        file.InputStream.ResizeImage(768, 200, PathCombine + "Tablet/" + FileName);
                        file.InputStream.ResizeImage(280, 100, PathCombine + "Phone/" + FileName);
                        break;
                }
               
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
                return "Success";
            }
            catch
            {
                return "Error";
            }
        }
        public bool RenameBannerFile(string path, string OldFileName, string NewFileName,string Ext)
        {
            try
            {
                var OldFile = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path + "desktop/")) + OldFileName+Ext;
                var NewFile = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path + "desktop/")) + NewFileName + Ext;
                System.IO.File.Copy(OldFile, NewFile);
                System.IO.File.Delete(OldFile);
                 OldFile = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path + "tablet/")) + OldFileName + Ext;
                 NewFile = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path + "tablet/")) + NewFileName + Ext;
                System.IO.File.Copy(OldFile, NewFile);
                System.IO.File.Delete(OldFile);
                OldFile = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path + "phone/")) + OldFileName + Ext;
                NewFile = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path + "phone/")) + NewFileName + Ext;
                System.IO.File.Copy(OldFile, NewFile);
                System.IO.File.Delete(OldFile);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool RenamePrimaryFile(string path, string OldFileName, string NewFileName, string Ext)
        {
            try
            {
                var OldFile = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path + "desktop/")) + OldFileName + Ext;
                var NewFile = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path + "desktop/")) + NewFileName + Ext;
                System.IO.File.Copy(OldFile, NewFile);
                System.IO.File.Delete(OldFile);
                OldFile = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path + "insidepage/")) + OldFileName + Ext;
                NewFile = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path + "insidepage/")) + NewFileName + Ext;
                System.IO.File.Copy(OldFile, NewFile);
                System.IO.File.Delete(OldFile);
                OldFile = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path + "tablet/")) + OldFileName + Ext;
                NewFile = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path + "tablet/")) + NewFileName + Ext;
                System.IO.File.Copy(OldFile, NewFile);
                System.IO.File.Delete(OldFile);
                OldFile = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path + "phone/")) + OldFileName + Ext;
                NewFile = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path + "phone/")) + NewFileName + Ext;
                System.IO.File.Copy(OldFile, NewFile);
                System.IO.File.Delete(OldFile);
                OldFile = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path + "icon/")) + OldFileName + Ext;
                NewFile = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path + "icon/")) + NewFileName + Ext;
                System.IO.File.Copy(OldFile, NewFile);
                System.IO.File.Delete(OldFile);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool DeletePrimaryFile(string path, string FileName)
        {
            try
            {
                var fle = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path + "desktop/")) + FileName;

                if (System.IO.File.Exists(fle))
                {
                    System.IO.File.Delete(fle);
                }
                fle = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path + "insidepage/")) + FileName;
                if (System.IO.File.Exists(fle))
                {
                    System.IO.File.Delete(fle);

                }
                fle = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path + "tablet/")) + FileName;
                if (System.IO.File.Exists(fle))
                {
                    System.IO.File.Delete(fle);

                }
                fle = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path + "phone/")) + FileName;
                if (System.IO.File.Exists(fle))
                {
                    System.IO.File.Delete(fle);

                }
                fle = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path + "icon/")) + FileName;
                if (System.IO.File.Exists(fle))
                {
                    System.IO.File.Delete(fle);

                }
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool DeleteBannerFile(string path, string FileName)
        {
            try
            {
                var fle = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path+"desktop/")) + FileName;
           
                if (System.IO.File.Exists(fle))
                {
                    System.IO.File.Delete(fle);
                }
                fle = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path + "tablet/")) + FileName;
                if (System.IO.File.Exists(fle))
                {
                    System.IO.File.Delete(fle);

                }
                fle = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path + "phone/")) + FileName;
                if (System.IO.File.Exists(fle))
                {
                    System.IO.File.Delete(fle);

                }
                return true;
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// حذف قایل از سرور
        /// </summary>
        /// <param name="path">مسیر فایل</param>
        /// <param name="FileName">نام فایل به همراه پسوند</param>
        /// <returns>True Or False</returns>
        public bool DeleteFile(string path, string FileName)
        {
            try
            {
                var fle = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path)) + FileName;
                if (System.IO.File.Exists(fle))
                {
                    System.IO.File.Delete(fle);

                }
                 fle = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path+ "thumbnails/")) + FileName;
                if (System.IO.File.Exists(fle))
                {
                    System.IO.File.Delete(fle);

                }
                fle = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~" + path + "BigImages/")) + FileName;
                if (System.IO.File.Exists(fle))
                {
                    System.IO.File.Delete(fle);

                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// دریافت نام و کد محصول از طریق شناسه محصول
        /// </summary>
        /// <param name="Id">شناسه محصول</param>
        /// <returns>Name And Code</returns>
        public GetNameProductViewModel GetNameProduct(string Id)
        {
            var temp = (from p in db.Products
                        where p.Id == Id
                        select new
                        {
                            p.FirstTitle,
                            p.Code
                        }).SingleOrDefault();
            return new GetNameProductViewModel { Code = temp.Code, Title = temp.FirstTitle };
        }

        /// <summary>
        /// دریافت نام و کد دسته محصول از طریق شناسه دسته محصول
        /// </summary>
        /// <param name="Id">شناسه دسته محصول</param>
        /// <returns>Name And Code</returns>
        public GetTitleCategoryViewModel GetTitleCategory(string Id)
        {
            var temp = (from p in db.ProductCategories
                        where p.Id == Id
                        select new
                        {
                            p.Title,
                            p.Code
                        }).SingleOrDefault();
            return new GetTitleCategoryViewModel { Code = temp.Code, Title = temp.Title };
        }

        /// <summary>
        /// دریافت نام و کد ـنظیمات از طریق شناسه تنظیمات
        /// </summary>
        /// <param name="Id">شناسه تنظیمات</param>
        /// <returns>Name And Code</returns>
        public GetTitleCategoryViewModel GetTitleSetting(string Id)
        {
            var temp = (from p in db.Settings
                        where p.Id == Id
                        select new
                        {
                            p.Title,
                        }).SingleOrDefault();
            return new GetTitleCategoryViewModel { Title = temp.Title };
        }

        #endregion Methods

        /// <summary>
        /// غیر سازنده
        /// </summary>
        #region DeCTOR

        /// <summary>
        /// غیر سازنده
        /// </summary>
        ~Repositories()
        {
            db.Dispose();
        }

        #endregion DeCTOR

    }
}