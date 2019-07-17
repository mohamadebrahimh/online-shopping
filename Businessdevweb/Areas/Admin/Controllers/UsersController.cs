using Businessdevweb.Extensions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Businessdevweb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public UsersController()
        {

       
        }

        public UsersController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
         
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            return View(UserManager.Users.Where(m=>m.Id!=userId).ToList());
        }
        [HttpPost]
        public ActionResult ChangePicture(HttpPostedFileBase file,string id)
        {
            if (file == null)
            {
                return new JsonResult { Data = new { type = "error", result = "تصویری را انتخاب نکرده اید" } };
            }
            try
            {
                
                var user = UserManager.Users.FirstOrDefault(m => m.Id == id);
                FileUploader.DeleteFile("/uploadFiles/Users/" + user.ImageFile);
                user.ImageFile = FileUploader.SaveFile(file, "/uploadFiles/Users/", user.UserName);
                if (user.ImageFile == null)
                {
                    return new JsonResult { Data = new { type = "error", result = "به دلیل خطایی نا مشخص فایل تغییر نکرد" } };
                }
                UserManager.Update(user);
                return new JsonResult { Data = new { type = "success", result = "تصویر با موفقیت تغییر کرد." } };
            }
            catch (Exception e)
            {

                return new JsonResult { Data = new { type = "error", result = e.Message } };
            }

        }


        [HttpPost]
        public JsonResult Delete(string id)
        {

            try
            {
                // TODO: Add delete logic here
                var user = UserManager.Users.FirstOrDefault(m=>m.Id==id);
                if (user.ImageFile!=null)
                {
                    FileUploader.DeleteFile("/uploadFiles/Users/" + user.ImageFile);

                }
                UserManager.Delete(user);
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
                UserManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}