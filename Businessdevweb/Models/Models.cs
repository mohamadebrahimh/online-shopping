using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Businessdevweb.Models
{
    /// <summary>
    /// کلاس پایه شامل پراپرتی های یکسان برای دیگر کلاسها
    /// </summary>
    public class BaseEntity
    {
        #region CTOR
        /// <summary>
        /// سازنده پیش فرض کلاس موجودیت پایه
        /// </summary>
        public BaseEntity()
        {
            //مقدار دهی یه آی دی توسط Guid
            Id = Guid.NewGuid().ToString();
            //تعریف شی ای از تاریخ و زمان و مقدار دهی زمان حال آن توسط کلاس هلپر
            DateTime dtime = DateTime.Now;
            //مقدار دهی اینسرت تایم و آپدیت تایم
            InsertTime = dtime;
            UpdateTime = dtime;
        }
        #endregion CTOR

        #region Properties
        /// <summary>
        /// آی دی با نوع Guid به صورت پایه برای تمام کلاسهایی که به جدول تبدیل می شود تعریف شده است
        /// </summary>
        [Key]
        [Required]
        [Column(Order = 1)]
        public string Id { get; set; }
        /// <summary>
        /// زمان مربوط به وارد کردن داده را مشخص می کند
        /// </summary>
        [Display(Name = "تاریخ ایجاد")]
        public DateTime InsertTime { get; set; }
        /// <summary>
        /// زمان مربوط بروز رسانی را مشخص می کند
        /// </summary>
        [Display(Name ="تاریخ بروزرسانی")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// پاک سازی توسط کار بر یا نویسنده را مشخص میکند
        /// </summary>
        public bool isDeleted { get; set; }
        /// <summary>
        /// زمان حذف داده را مشخص می کند
        /// </summary>
        public DateTime? DeleteTime { get; set; }
        #endregion Properties
    }
    public class BaseEntityIsActive:BaseEntity
    {
        [Display(Name ="فعال شود؟")]
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// //////////////////////////////////Product
    /// </summary>
    public class Product : BaseEntityIsActive
    {
        #region Configuration
        /// <summary>
        /// Fluent Api
        /// </summary>
        internal class Configuration :
            System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Product>
        {
            public Configuration()
            {
                HasKey(m => m.Id);
                HasRequired(current => current.Category)
                    .WithMany(Category => Category.Products)
                    .HasForeignKey(current => current.CategoryId)
                    .WillCascadeOnDelete(false);
            }
        }
        #endregion /Configuration
        #region CTOR
        public Product()
        {


        }

        #endregion CTOR

        #region Properties
        [Display(Name = "کد محصول")]
        [MaxLength(45, ErrorMessage = "حد اکثر کاراکتر مجاز 45 کاراکتر می باشد.")]
        public string Code { get; set; }

        public string Name { get; set; }

        [Display(Name = "عنوان فارسی")]
        [Required(ErrorMessage = "وارد کردن عنوان فارسی الزامی است.")]
        [MaxLength(45, ErrorMessage = "حد اکثر کاراکتر مجاز 45 کاراکتر می باشد.")]
        public string FirstTitle { get; set; }

        [Display(Name = "عنوان انگلیسی")]
        [MaxLength(45, ErrorMessage = "حد اکثر کاراکتر مجاز 45 کاراکتر می باشد.")]
        [Required(ErrorMessage = "وارد کردن عنوان انگلیسی الزامی است.")]
        public string SecondTitle { get; set; }
        [UIHint("Html")]
        [Display(Name = "معرفی محصول")]
        public string Description { get; set; }


        [Display(Name = "برند محصول")]
        [MaxLength(70, ErrorMessage = "حد اکثر کاراکتر مجاز 70 کاراکتر می باشد.")]
        public string Brand { get; set; }

        [Display(Name = "تعداد بازدید")]
        public int Visit { get; set; }

        [Display(Name = "تگ توضیحات")]
        [MaxLength(160, ErrorMessage = "حد اکثر کاراکتر مجاز 160 کاراکتر می باشد.")]
        public string MetaDescription { get; set; }

        [Display(Name = "تگ کلمات کلیدی")]
        [MaxLength(150, ErrorMessage = "حد اکثر کاراکتر مجاز 150 کاراکتر می باشد.")]
        public string MetaKeyWord { get; set; }

        [Display(Name = "دسته محصول")]
        [Required(ErrorMessage = "وارد کردن دسته محصول الزامی است.")]
        [MaxLength(70, ErrorMessage = "حد اکثر کاراکتر مجاز 45 کاراکتر می باشد.")]
        public string CategoryId { get; set; }
        [Display(Name = "قیمت (ريال)")]
        public long? Price { get; set; }
        [Display(Name = "این محصول تخفیف هم دارد؟")]
        public bool IsDiscount { get; set; }
        [Display(Name = "درصد تخفیف از 1 تا 100")]
        public int DiscountPercent { get; set; }
        [Display(Name = "تعداد")]
        public int Qty { get; set; }
        public string ImageFile { get; set; }

        public long? PriceWithDiscount => (IsDiscount) ? Price - ((Price / 100) * DiscountPercent) :null;

        public virtual ProductCategory Category { get; set; }
        public virtual List<ProductImage> ProductImages { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<ProductFeatures> ProductFeatures { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }


        #endregion Properties



    }
    public class ProductFeatures : BaseEntity
    {
        #region Configuration
        /// <summary>
        /// Fluent Api
        /// </summary>
        internal class Configuration :
            System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ProductFeatures>
        {
            public Configuration()
            {
                HasKey(m => m.Id);
                HasRequired(current => current.Product)
                    .WithMany(Product => Product.ProductFeatures)
                    .HasForeignKey(current => current.ProductId)
                    .WillCascadeOnDelete(false);
            }
        }
        #endregion /Configuration

        #region CTOR
        public ProductFeatures()
        {

        }
        #endregion CTOR

        #region Properties

        public string ProductId { get; set; }

        public virtual Product Product { get; set; }
        [Display(Name = "گروه خصوصیت")]
        public string Group { get; set; }

        [Display(Name = "عنوان خصوصیت")]
        [Required(ErrorMessage = "وارد کردن عنوان خصوصیت الزامی است.")]
        public string Name { get; set; }

        [Display(Name = "مقدار خصوصیت")]
        [Required(ErrorMessage = "وارد کردن مقدار خصوصیت الزامی است.")]
        public string Value { get; set; }

        #endregion Properties



    }
    public class ProductImage : BaseEntity
    {
        //بدنه قسمت کانفیگوریشن
        //میتوان این قسمت را در یک کلاس جداگانه نوشت و به برنامه اد کرد
        #region Configuration
        /// <summary>
        /// Fluent Api
        /// </summary>
        internal class Configuration :
            System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ProductImage>
        {
            public Configuration()
            {
                HasKey(m => m.Id);
                ///ارتباط چند به یک با محصول
                HasRequired(current => current.Product)
                   .WithMany(product => product.ProductImages)
                   .HasForeignKey(current => current.ProductId)
                   .WillCascadeOnDelete(false);
            }
        }
        #endregion /Configuration

        #region CTOR
        public ProductImage()
        {

        }
        #endregion CTOR

        #region Properties

        /// <summary>
        /// Name Tasvir Ra Moshakhas Mikonad
        /// </summary>
        [Display(Name = "نام تصویر")]
        public string Name { get; set; }

        /// <summary>
        /// Onvan Tasvir
        /// </summary>
        [Display(Name = "عنوان تصویر")]
        public string Title { get; set; }


        #endregion Properties

        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
    public class ProductCategory : BaseEntity
    {
        #region Configuration
        /// <summary>
        /// Fluent Api
        /// </summary>
        internal class Configuration :
            System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ProductCategory>
        {
            public Configuration()
            {
                HasKey(m => m.Id);
                HasOptional(current => current.Parent)
                    .WithMany(Parent => Parent.Categories)
                    .HasForeignKey(current => current.ParentId)
                    .WillCascadeOnDelete(false);
            }
        }
        #endregion /Configuration

        #region CTOR
        public ProductCategory()
        {

        }
        #endregion CTOR

        #region Properties

        [Display(Name = "کد دسته")]
        public string Code { get; set; }

        [Display(Name = "عنوان دسته")]
        [Required(ErrorMessage = "وارد کردن عنوان الزامی است.")]
        [MaxLength(70, ErrorMessage = "حد اکثر کاراکتر مجاز 70 کاراکتر می باشد.")]
        public string Title { get; set; }

        [Display(Name = "نام دسته")]

        public string Name { get; set; }

        [UIHint("Html")]
        [Display(Name = "توضیحات دسته")]
        public string Description { get; set; }

        [Display(Name = "تگ توضیحات")]
        [MaxLength(160, ErrorMessage = "حد اکثر کاراکتر مجاز 160 کاراکتر می باشد.")]
        public string MetaDescription { get; set; }

        [Display(Name = "تگ کلمات کلیدی")]
        [MaxLength(150, ErrorMessage = "حد اکثر کاراکتر مجاز 150 کاراکتر می باشد.")]
        public string MetaKeyWord { get; set; }

        [Display(Name = "شماره ردیف دسته")]
        public int Position { get; set; }

        [Display(Name = "تعداد بازدید")]
        public int Visit { get; set; }

        [Display(Name = "دسته والد")]
        public string ParentId { get; set; }
        [Display(Name = "محصولات در این دسته قرار گیرد.")]
        public bool IsEndChild { get; set; }
        public string ImageFile { get; set; }

        public virtual ProductCategory Parent { get; set; }
        public virtual List<Product> Products { get; set; }
        public virtual List<ProductCategory> Categories { get; set; }
        #endregion Properties
    }
    public class Setting : BaseEntityIsActive
    {
        #region Configuration
        /// <summary>
        /// Fluent Api
        /// </summary>
        internal class Configuration :
            System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Setting>
        {
            public Configuration()
            {
                HasKey(m => m.Id);

            }
        }
        #endregion /Configuration

        #region CTOR
        public Setting()
        {

        }
        #endregion CTOR

        #region Properties


        [Display(Name = "عنوان سایت")]
        [Required(ErrorMessage = "وارد کردن عنوان سایت الزامی است.")]
        [MaxLength(70, ErrorMessage = "حد اکثر کاراکتر مجاز 70 کاراکتر می باشد.")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        [Display(Name = "کپی رایت")]
        public string Copyright { get; set; }
        [Display(Name = "شماره تماس")]
        public string Phone { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "لوگوی سایت")]
        public string LogoFile { get; set; }
        [Display(Name = "توضیحات تماس سایت")]
        public string DescriptionCall { get; set; }
        public string LogoTitle { get; set; }


        #endregion Properties
    }
    public class Slider : BaseEntityIsActive
    {
        #region Configuration
        /// <summary>
        /// Fluent Api
        /// </summary>
        internal class Configuration :
            System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Slider>
        {
            public Configuration()
            {
                HasKey(m => m.Id);

            }
        }
        #endregion /Configuration
        #region CTOR
        public Slider()
        {

        }
        #endregion CTOR
        #region Properties


        [Display(Name = "بنر اختصاصی برای صفحات است؟")]
        public bool IsPage { get; set; }


        [Display(Name = "بنر اختصاصی برای صفحه اصلی است؟")]
        public bool IsHome { get; set; }

        [Display(Name = "بنر اختصاصی برای محصولات است؟")]
        public bool IsProduct { get; set; }

        public virtual List<SliderImage> SliderImages { get; set; }
        [Display(Name = "عنوان")]
        public string Title { get;  set; }
        #endregion Properties
    }
    public class SliderImage : BaseEntity
    {
        //بدنه قسمت کانفیگوریشن
        //میتوان این قسمت را در یک کلاس جداگانه نوشت و به برنامه اد کرد
        #region Configuration
        /// <summary>
        /// Fluent Api
        /// </summary>
        internal class Configuration :
            System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<SliderImage>
        {
            public Configuration()
            {
                HasKey(m => m.Id);
                ///ارتباط چند به یک با بنرها
                HasRequired(current => current.Slider)
                   .WithMany(Slider => Slider.SliderImages)
                   .HasForeignKey(current => current.SliderId)
                   .WillCascadeOnDelete(false);
            }
        }
        #endregion /Configuration

        #region CTOR
        public SliderImage()
        {

        }
        #endregion CTOR

        #region Properties
        [Display(Name = "آدرس ارجاع")]
        public string URLPage { get; set; }
        /// <summary>
        /// Name Tasvir Ra Moshakhas Mikonad
        /// </summary>
        [Display(Name = "نام تصویر")]
        public string Name { get; set; }

        /// <summary>
        /// Onvan Tasvir
        /// </summary>
        [Display(Name = "عنوان تصویر")]
        public string Title { get; set; }
        public int Position { get; set; }

        #endregion Properties

        public string SliderId { get; set; }
        public virtual Slider Slider { get; set; }
    }
    public class Comment : BaseEntity
    {

        #region Configuration
        /// <summary>
        /// Fluent Api
        /// </summary>
        internal class Configuration :
            System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Comment>
        {
            public Configuration()
            {
                HasKey(m => m.Id);
                ///ارتباط چند به یک 
                HasOptional(current => current.Reply)
                   .WithMany(Reply => Reply.Comments)
                   .HasForeignKey(current => current.ReplyId)
                   .WillCascadeOnDelete(false);
                HasRequired(current => current.ApplicationUser)
                   .WithMany(ApplicationUser => ApplicationUser.Comments)
                   .HasForeignKey(current => current.UserId)
                   .WillCascadeOnDelete(true);
                HasRequired(current => current.Product)
                   .WithMany(ApplicationUser => ApplicationUser.Comments)
                   .HasForeignKey(current => current.ProductId)
                   .WillCascadeOnDelete(false);

            }
        }
        #endregion /Configuration

        #region CTOR
        public Comment()
        {

        }
        #endregion CTOR
        #region Properties

        /// <summary>
        /// پیام
        /// </summary>
        [Display(Name = "پیام")]
        [Required(ErrorMessage = "لطفا پیام (انتقاد، پیشنهاد، شکایت)  خود را وارد نمایید.")]
        [MaxLength(300, ErrorMessage = "تعداد کاراکتر های مجاز 300 حرف می باشد.")]
        public string Message { get; set; }
        public string ReplyId { get; set; }
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Comment Reply { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        #endregion Properties 
    }
    public class ContactUs : BaseEntity
    {

        #region Configuration
        /// <summary>
        /// Fluent Api
        /// </summary>
        internal class Configuration :
            System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ContactUs>
        {
            public Configuration()
            {
                HasKey(m => m.Id);

            }
        }
        #endregion /Configuration

        #region CTOR
        public ContactUs()
        {

        }
        #endregion CTOR
        #region Properties
        /// <summary>
        /// نام و نام خانوادگی
        /// </summary>
        [Display(Name = "نام کامل")]
        [Required(ErrorMessage = "لطفا نام خود را وارد کنید.")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های مجاز 100 حرف می باشد.")]
        public string FullName { get; set; }
        /// <summary>
        /// پست الکترونیکی
        /// </summary>
        [Display(Name = "پست الکترونیکی")]
        [EmailAddress(ErrorMessage ="نشانی نا معتبر است")]
        public string Email { get; set; }
        /// <summary>
        /// شماره همراه
        /// </summary>
        [Display(Name = "شماره همراه")]
        [Phone]
        public string Phone { get; set; }
        /// <summary>
        /// پیام
        /// </summary>
        [Display(Name = "پیام")]
        [Required(ErrorMessage = "لطفا پیام (انتقاد، پیشنهاد، شکایت)  خود را وارد نمایید.")]
        [MaxLength(300, ErrorMessage = "تعداد کاراکتر های مجاز 300 حرف می باشد.")]
        public string Message { get; set; }
        #endregion Properties 
    }
    public class AboutMe : BaseEntity
    {

        #region Configuration
        /// <summary>
        /// Fluent Api
        /// </summary>
        internal class Configuration :
            System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AboutMe>
        {
            public Configuration()
            {
                HasKey(m => m.Id);

            }
        }
        #endregion /Configuration

        #region CTOR
        public AboutMe()
        {

        }
        #endregion CTOR
        #region Properties
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [UIHint("Html")]
        [Display(Name = "محتویات")]
        public string Content { get; set; }

        #endregion Properties 
    }
    public class ContactUsPage : BaseEntity
    {

        #region Configuration
        /// <summary>
        /// Fluent Api
        /// </summary>
        internal class Configuration :
            System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ContactUsPage>
        {
            public Configuration()
            {
                HasKey(m => m.Id);

            }
        }
        #endregion /Configuration

        #region CTOR
        public ContactUsPage()
        {

        }
        #endregion CTOR
        #region Properties
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [UIHint("Html")]
        [Display(Name = "محتویات")]
        public string Content { get; set; }

        #endregion Properties 
    }
    public class Order : BaseEntity
    {

        #region Configuration
        /// <summary>
        /// Fluent Api
        /// </summary>
        internal class Configuration :
            System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Order>
        {
            public Configuration()
            {
                HasKey(m => m.Id);
                HasRequired(current => current.ApplicationUser)
                   .WithMany(ApplicationUser => ApplicationUser.Orders)
                   .HasForeignKey(current => current.UserId)
                   .WillCascadeOnDelete(false);

            }
        }
        #endregion /Configuration

        #region CTOR
        public Order()
        {

        }
        #endregion CTOR
        #region Properties
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public int Status { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }
        public string UserId { get; set; }
        public virtual List<Payment> Payments { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        #endregion Properties 
    }
    public class OrderItem : BaseEntity
    {

        #region Configuration
        /// <summary>
        /// Fluent Api
        /// </summary>
        internal class Configuration :
            System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<OrderItem>
        {
            public Configuration()
            {
                HasKey(m => m.Id);
                HasRequired(current => current.Product)
                   .WithMany(Product => Product.OrderItems)
                   .HasForeignKey(current => current.ProductId)
                   .WillCascadeOnDelete(false);
                HasRequired(current => current.Order)
                   .WithMany(Order => Order.OrderItems)
                   .HasForeignKey(current => current.OrderId)
                   .WillCascadeOnDelete(false);
            }
        }
        #endregion /Configuration

        #region CTOR
        public OrderItem()
        {

        }
        #endregion CTOR
        #region Properties
        public string ProductId { get; set; }
        [Display(Name = "قیمت (ريال)")]
        public long? Price { get; set; }
        [Display(Name = "این محصول تخفیف هم دارد؟")]
        public bool IsDiscount { get; set; }
        [Display(Name = "درصد تخفیف از 1 تا 100")]
        public int DiscountPercent { get; set; }
        [Display(Name = "تعداد")]
        public int Qty { get; set; }
        public long? TotalPrice => Qty * Price;
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

        #endregion Properties 
    }
    public class Payment : BaseEntity
    {

        #region Configuration
        /// <summary>
        /// Fluent Api
        /// </summary>
        internal class Configuration :
            System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Payment>
        {
            public Configuration()
            {
                HasKey(m => m.Id);
                HasOptional(current => current.ApplicationUser)
                   .WithMany(ApplicationUser => ApplicationUser.Payments)
                   .HasForeignKey(current => current.UserId)
                   .WillCascadeOnDelete(false);
                HasRequired(current => current.Order)
                   .WithMany(Order => Order.Payments)
                   .HasForeignKey(current => current.OrderId)
                   .WillCascadeOnDelete(false);
            }
        }
        #endregion /Configuration

        #region CTOR
        public Payment()
        {

        }
        #endregion CTOR
        #region Properties
        public string RefId { get; set; }
        [Display(Name = "مبلغ (ريال)")]
        public long? Price { get; set; }
        public int Status { get; set; }
        public string Bank { get; set; }
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        #endregion Properties 
    }
}
