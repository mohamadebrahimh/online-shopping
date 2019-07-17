using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Businessdevweb.Areas.Admin.Models
{
    public class AddFeaturesViewModel
    {
        public AddFeaturesViewModel()
        {

        }
        public string ProductId { get; set; }
        [Display(Name = "گروه خصوصیت")]
        public string Group { get; set; }

        [Display(Name = "عنوان خصوصیت")]
        [Required(ErrorMessage = "وارد کردن عنوان خصوصیت الزامی است.")]
        public string Name { get; set; }

        [Display(Name = "مقدار خصوصیت")]
        [Required(ErrorMessage = "وارد کردن مقدار خصوصیت الزامی است.")]
        public string Value { get; set; }
    }
    public class EditFeaturesViewModel
    {
        public EditFeaturesViewModel()
        {

        }
        public string Id { get; set; }
        public string ProductId { get; set; }
        [Display(Name = "گروه خصوصیت")]
        public string Group { get; set; }

        [Display(Name = "عنوان خصوصیت")]
        [Required(ErrorMessage = "وارد کردن عنوان خصوصیت الزامی است.")]
        public string Name { get; set; }

        [Display(Name = "مقدار خصوصیت")]
        [Required(ErrorMessage = "وارد کردن مقدار خصوصیت الزامی است.")]
        public string Value { get; set; }
    }
    public class CreateProductCategoryViewModel
    {
        #region CTOR
        public CreateProductCategoryViewModel()
        {

        }
        #endregion CTOR

        #region Properties
        [Display(Name = "کد دسته")]
        [Required(ErrorMessage = "وارد کردن کد دسته الزامی است.")]
        public string Code { get; set; }
        [Display(Name = "عنوان دسته")]
        [Required(ErrorMessage = "وارد کردن عنوان الزامی است.")]
        [MaxLength(70, ErrorMessage = "حد اکثر کاراکتر مجاز 70 کاراکتر می باشد.")]
        public string Title { get; set; }

        [UIHint("Html")]
        [Display(Name = "توضیحات دسته")]
        public string Description { get; set; }

        [Display(Name = "تگ توضیحات")]
        [Required(ErrorMessage = "وارد کردن تگ توضیحات الزامی است.")]
        [MaxLength(160, ErrorMessage = "حد اکثر کاراکتر مجاز 160 کاراکتر می باشد.")]
        public string MetaDescription { get; set; }

        [Display(Name = "تگ کلمات کلیدی")]
        [Required(ErrorMessage = "وارد کردن تگ کلمات کلیدی الزامی است.")]
        [MaxLength(150, ErrorMessage = "حد اکثر کاراکتر مجاز 150 کاراکتر می باشد.")]
        public string MetaKeyWord { get; set; }

        [Display(Name = "شماره ردیف دسته")]
        public int Index { get; set; }

        [Display(Name = "دسته والد")]
        public string ParentId { get; set; }
        [Display(Name = "محصولات در این دسته قرار گیرد.")]
        public bool IsEndChild { get; set; }
        #endregion Properties
    }
    public class CreateProductViewModel
    {
        #region CTOR
        public CreateProductViewModel()
        {
        }
        #endregion CTOR

        #region Properties
        [Display(Name = "کد محصول")]
        [Required(ErrorMessage = "وارد کردن کد محصول الزامی است.")]
        [MaxLength(45, ErrorMessage = "حد اکثر کاراکتر مجاز 45 کاراکتر می باشد.")]
        public string Code { get; set; }

        [Display(Name = "عنوان فارسی")]
        [Required(ErrorMessage = "وارد کردن عنوان فارسی الزامی است.")]
        [MaxLength(45, ErrorMessage = "حد اکثر کاراکتر مجاز 45 کاراکتر می باشد.")]
        public string FirstTitle { get; set; }

        [Display(Name = "عنوان انگلیسی")]
        [MaxLength(45, ErrorMessage = "حد اکثر کاراکتر مجاز 45 کاراکتر می باشد.")]
        public string SecondTitle { get; set; }

        [UIHint("Html")]
        [Display(Name = "معرفی محصول")]
        public string Description { get; set; }

        [Display(Name = "مدل محصول")]
        [MaxLength(70, ErrorMessage = "حد اکثر کاراکتر مجاز 70 کاراکتر می باشد.")]
        public string Model { get; set; }

        [Display(Name = "برند محصول")]
        [Required(ErrorMessage = "وارد کردن برند محصول الزامی است.")]
        [MaxLength(70, ErrorMessage = "حد اکثر کاراکتر مجاز 70 کاراکتر می باشد.")]
        public string Brand { get; set; }

        [Display(Name = "دسته محصول")]
        [Required(ErrorMessage = "وارد کردن دسته محصول الزامی است.")]
        [MaxLength(70, ErrorMessage = "حد اکثر کاراکتر مجاز 45 کاراکتر می باشد.")]
        public string CategoryId { get; set; }

        [Display(Name = "تگ توضیحات")]
        [Required(ErrorMessage = "وارد کردن تگ توضیحات الزامی است.")]
        [MaxLength(160, ErrorMessage = "حد اکثر کاراکتر مجاز 160 کاراکتر می باشد.")]
        public string MetaDescription { get; set; }

        [Display(Name = "تگ کلمات کلیدی")]
        [Required(ErrorMessage = "وارد کردن تگ کلمات کلیدی الزامی است.")]
        [MaxLength(150, ErrorMessage = "حد اکثر کاراکتر مجاز 150 کاراکتر می باشد.")]
        public string MetaKeyWord { get; set; }
        #endregion Properties
    }
    public class EditProductViewModel
    {
        #region CTOR
        public EditProductViewModel()
        {
        }
        #endregion CTOR

        #region Properties
        public string Id { get; set; }
        [Display(Name = "کد محصول")]
        [Required(ErrorMessage = "وارد کردن کد محصول الزامی است.")]
        [MaxLength(45, ErrorMessage = "حد اکثر کاراکتر مجاز 45 کاراکتر می باشد.")]
        public string Code { get; set; }

        [Display(Name = "عنوان فارسی")]
        [Required(ErrorMessage = "وارد کردن عنوان فارسی الزامی است.")]
        [MaxLength(45, ErrorMessage = "حد اکثر کاراکتر مجاز 45 کاراکتر می باشد.")]
        public string FirstTitle { get; set; }

        [Display(Name = "عنوان انگلیسی")]
        [MaxLength(45, ErrorMessage = "حد اکثر کاراکتر مجاز 45 کاراکتر می باشد.")]
        public string SecondTitle { get; set; }

        [UIHint("Html")]
        [Display(Name = "معرفی محصول")]
        public string Description { get; set; }

        [Display(Name = "مدل محصول")]
        [MaxLength(70, ErrorMessage = "حد اکثر کاراکتر مجاز 70 کاراکتر می باشد.")]
        public string Model { get; set; }

        [Display(Name = "برند محصول")]
        [Required(ErrorMessage = "وارد کردن برند محصول الزامی است.")]
        [MaxLength(70, ErrorMessage = "حد اکثر کاراکتر مجاز 70 کاراکتر می باشد.")]
        public string Brand { get; set; }

        [Display(Name = "دسته محصول")]
        [Required(ErrorMessage = "وارد کردن دسته محصول الزامی است.")]
        [MaxLength(70, ErrorMessage = "حد اکثر کاراکتر مجاز 45 کاراکتر می باشد.")]
        public string CategoryId { get; set; }

        [Display(Name = "تگ توضیحات")]
        [Required(ErrorMessage = "وارد کردن تگ توضیحات الزامی است.")]
        [MaxLength(160, ErrorMessage = "حد اکثر کاراکتر مجاز 160 کاراکتر می باشد.")]
        public string MetaDescription { get; set; }

        [Display(Name = "تگ کلمات کلیدی")]
        [Required(ErrorMessage = "وارد کردن تگ کلمات کلیدی الزامی است.")]
        [MaxLength(150, ErrorMessage = "حد اکثر کاراکتر مجاز 150 کاراکتر می باشد.")]
        public string MetaKeyWord { get; set; }
        #endregion Properties
    }
    public class GetNameProductViewModel
    {
        [Display(Name = "کد محصول")]
        public string Code { get; set; }

        [Display(Name = "نام محصول")]
        public string Title { get; set; }
    }
    public class GetTitleCategoryViewModel
    {
        [Display(Name = "کد دسته محصول")]
        public string Code { get; set; }

        [Display(Name = "نام دسته محصول")]
        public string Title { get; set; }
    }
    public class EditProductCategoryViewModel
    {
        #region CTOR
        public EditProductCategoryViewModel()
        {

        }
        #endregion CTOR

        #region Properties
        public string Id { get; set; }
        [Display(Name = "کد دسته")]
        [Required(ErrorMessage = "وارد کردن کد دسته الزامی است.")]
        public string Code { get; set; }
        [Display(Name = "عنوان دسته")]
        [Required(ErrorMessage = "وارد کردن عنوان الزامی است.")]
        [MaxLength(70, ErrorMessage = "حد اکثر کاراکتر مجاز 70 کاراکتر می باشد.")]
        public string Title { get; set; }

        [UIHint("Html")]
        [Display(Name = "توضیحات دسته")]
        public string Description { get; set; }

        [Display(Name = "تگ توضیحات")]
        [Required(ErrorMessage = "وارد کردن تگ توضیحات الزامی است.")]
        [MaxLength(160, ErrorMessage = "حد اکثر کاراکتر مجاز 160 کاراکتر می باشد.")]
        public string MetaDescription { get; set; }

        [Display(Name = "تگ کلمات کلیدی")]
        [Required(ErrorMessage = "وارد کردن تگ کلمات کلیدی الزامی است.")]
        [MaxLength(150, ErrorMessage = "حد اکثر کاراکتر مجاز 150 کاراکتر می باشد.")]
        public string MetaKeyWord { get; set; }

        [Display(Name = "شماره ردیف دسته")]
        public int Index { get; set; }

        [Display(Name = "محصولات در این دسته قرار گیرد.")]
        public bool IsEndChild { get; set; }

        [Display(Name = "دسته والد")]
        public string ParentId { get; set; }
        #endregion Properties
    }
    public class ListProductCategoryViewModel
    {
        #region CTOR
        public ListProductCategoryViewModel()
        {

        }
        #endregion CTOR

        #region Properties

        public string Id { get; set; }

        [Display(Name = "کد دسته")]
        [Required(ErrorMessage = "وارد کردن کد دسته الزامی است.")]
        public string Code { get; set; }

        [Display(Name = "عنوان دسته")]
        [Required(ErrorMessage = "وارد کردن عنوان الزامی است.")]
        [MaxLength(70, ErrorMessage = "حد اکثر کاراکتر مجاز 70 کاراکتر می باشد.")]
        public string Title { get; set; }

        [Display(Name = "شماره ردیف دسته")]
        public int Index { get; set; }

        [Display(Name = "تعداد بازدید")]
        public int Visit { get; set; }

        [Display(Name = "دسته والد")]
        public string ParentId { get; set; }

        #endregion Properties
    }
    public class ImageSelectImgIdViewModel
    {
        public string Id { get; set; }
        public Businessdevweb.Models.ProductImage Image { get; set; }
    }
    public class ImageUploadViewModel
    {
        public ImageUploadViewModel()
        {

        }
        /// <summary>
        /// Onvan Tasvir
        /// </summary>
        [Display(Name = "عنوان تصویر")]
        [Required(ErrorMessage = "وارد کردن عنوان تصویر الزامی است.")]
        public string Title { get; set; }

        /// <summary>
        /// Alt Tasvir Ra Moshakhas Mikonad
        /// </summary>
        [Display(Name = "متن جایگزین تصویر")]
        [Required(ErrorMessage = "وارد کردن متن جایگزین تصویر الزامی است.")]
        public string Alt { get; set; }
        public string Position { get; set; }
        [HiddenInput]
        public string TEntity { get; set; }

        [HiddenInput]
        public string Id { get; set; }
    }
    public class SelectListViewModel
    {
        public SelectListViewModel()
        {

        }
        public string Text { get; set; }
        public string Value { get; set; }
    }
    public class CreateColorViewModel
    {
        public CreateColorViewModel()
        {

        }
        [Display(Name = "کد رنگ")]
        [Required(ErrorMessage = "وارد کردن مقدار رنگ الزامی است.")]
        public string Value { get; set; }
        [Display(Name = "نام رنگ")]
        [Required(ErrorMessage = "وارد کردن نام رنگ الزامی است.")]
        public string Name { get; set; }
    }
    public class EditColorViewModel
    {
        public EditColorViewModel()
        {

        }
        public string Id { get; set; }
        [Display(Name = "کد رنگ")]
        [Required(ErrorMessage = "وارد کردن مقدار رنگ الزامی است.")]
        public string Value { get; set; }
        [Display(Name = "نام رنگ")]
        [Required(ErrorMessage = "وارد کردن نام رنگ الزامی است.")]
        public string Name { get; set; }
    }
    public class AddOrEditProductPrice
    {
        public AddOrEditProductPrice()
        {

        }
        public string ProductTitle { get; set; }
        public string ProductId { get; set; }
        public string Id { get; set; }
        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "وارد کردن قیمت الزامی است.")]
        public double Price { get; set; }

    }
    public class ProductPrices
    {
        public ProductPrices()
        {

        }
        /// <summary>
        /// زمان مربوط به وارد کردن داده را مشخص می کند
        /// </summary>
        [Display(Name = "تاریخ ایجاد")]
        public DateTime InsertTime { get; set; }
        /// <summary>
        /// زمان مربوط بروز رسانی را مشخص می کند
        /// </summary>
        [Display(Name = "تاریخ بروزرسانی")]
        public DateTime UpdateTime { get; set; }
        public string Id { get; set; }
        [Display(Name = "قیمت")]
        [MinLength(1)]
        [Required(ErrorMessage = "وارد کردن قیمت الزامی است.")]
        public double Price { get; set; }

    }
    public class SaveAsImage
    {
        public SaveAsImage()
        {

        }
        /// <summary>
        /// Onvan Tasvir
        /// </summary>
        [Display(Name = "عنوان تصویر")]
        [Required(ErrorMessage = "وارد کردن عنوان تصویر الزامی است.")]
        public string Title { get; set; }

        /// <summary>
        /// Alt Tasvir Ra Moshakhas Mikonad
        /// </summary>
        [Display(Name = "متن جایگزین تصویر")]
        [Required(ErrorMessage = "وارد کردن متن جایگزین تصویر الزامی است.")]
        public string Alt { get; set; }
        [HiddenInput]
        public string ReturnIdUrl { get; set; }
        [Display(Name = "فایل")]
        public HttpPostedFileBase File { get; set; }
        /// <summary>
        /// آی دی 
        /// </summary>
        [HiddenInput]
        public string Id { get; set; }

    }
    #region CTOR

    #endregion CTOR
    #region Properties
    #endregion Properties
}