using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Businessdevweb.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required(ErrorMessage="وارد کردن کلمه عبور جدید الزامی است.")]
        [StringLength(100, ErrorMessage = "حد اکثر  100 کاراکتر و حداقل {2} کاراکتر مجاز می باشد.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور جدید")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "وارد کردن تایید کلمه عبور جدید الزامی است.")]
        [DataType(DataType.Password)]
        [Display(Name = "تایید کلمه عبور جدید")]
        [Compare("NewPassword", ErrorMessage = "کلمه عبور جدید همخوانی ندارند و مورد تایید نمی باشد.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "وارد کردن کلمه عبور فعلی الزامی است.")]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور فعلی")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "وارد کردن کلمه عبور جدید الزامی است.")]
        [StringLength( 100, ErrorMessage = "حد اکثر  100 کاراکتر و حداقل {2} کاراکتر مجاز می باشد.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور جدید")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "وارد کردن تایید کلمه عبور جدید الزامی است.")]
        [DataType(DataType.Password)]
        [Display(Name = "تایید کلمه عبور جدید")]
        [Compare("NewPassword", ErrorMessage = "کلمه عبور جدید همخوانی ندارند و مورد تایید نمی باشد.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required(ErrorMessage = "وارد کردن شماره تلفن الزامی است.")]
        [Phone]
        [Display(Name = "شماره تلفن")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required(ErrorMessage = "وارد کردن کد الزامی است.")]
        [Display(Name = "کد")]
        public string Code { get; set; }

        [Required(ErrorMessage = "وارد کردن شماره تلفن الزامی است.")]
        [Phone]
        [Display(Name = "شماره تلف")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}