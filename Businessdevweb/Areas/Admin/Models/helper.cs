using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace Businessdevweb.Areas.Admin.Models
{
    public static class Positions
    {
        static Positions()
        {
        }
        public static string Icon = "Icon";
        public static string Main = "Main";
        public static string Primary = "Primary";
        public static List<SelectListViewModel> Selectlist = new List<SelectListViewModel>()
        {
            new SelectListViewModel {Text="تصاویر اصلی",Value=Main }
        };
    }
    public static class Roles
    {
        public static string Admin = "Admin";
        public static string User = "User";
    }


    public static class Helpers
    {
        /// <summary>
        /// تغییر اندازه تصویر
        /// </summary>
        /// <param name="imageFile">آرایه بایتی از تصویر مورد نظر</param>
        /// <param name="targetSize">اندازه تصویر خروجی</param>
        /// <param name="format">فرمت تصویر خروجی</param>
        /// <returns></returns>
        public static byte[] ResizeImageFile(this byte[] imageFile, Int32 targetSize, ImageFormat format)
        {
            if (imageFile == null)
                throw new Exception("لطفا تصویر اصلی را مشخص نمایید");
            //باز کردن تصویر اصلی به عنوان یک جریان
            using (var oldImage = Image.FromStream(new MemoryStream(imageFile)))
            {
                //محاسبه اندازه تصویر خروجی با توجه به اندازه داده شده
                var newSize = CalculateDimensions(oldImage.Size, targetSize);
                //ایجاد تصویر جدید
                using (var newImage = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format24bppRgb))
                {
                    using (var canvas = Graphics.FromImage(newImage))
                    {
                        canvas.SmoothingMode = SmoothingMode.AntiAlias;
                        canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        canvas.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        //تغییر اندازه تصویر اصلی و قرار دادن آن در تصویر جدید
                        canvas.DrawImage(oldImage, new Rectangle(new Point(0, 0), newSize));
                        var m = new MemoryStream();
                        //ذخیره تصویر جدید با فرمت وارد شده
                        newImage.Save(m, format);
                        return m.GetBuffer();
                    }
                }
            }
        }

        /// <summary>
        /// محاسبه ابعاد تصویر خروجی
        /// </summary>
        /// <param name="oldSize">اندازه تصویر اصلی</param>
        /// <param name="targetSize">اندازه تصویر خروجی</param>
        /// <returns></returns>
        private static Size CalculateDimensions(Size oldSize, Int32 targetSize)
        {
            var newSize = new Size();
            if (oldSize.Height > oldSize.Width)
            {
                newSize.Width = Convert.ToInt32(oldSize.Width * (targetSize / (float)oldSize.Height));
                newSize.Height = targetSize;
            }
            else
            {
                newSize.Width = targetSize;
                newSize.Height = Convert.ToInt32(oldSize.Height * (targetSize / (float)oldSize.Width));
            }
            return newSize;
        }
    }


    public static class ImagesHelper
    {

        public enum ImageComperssion
        {
            Maximum = 50,
            Good = 60,
            Normal = 70,
            Fast = 80,
            Minimum = 90,
        }

        public static void ResizeImage(this Stream inputStream, int width, int height, string savePath, ImageComperssion ic = ImageComperssion.Normal)
        {
            System.Drawing.Image img = new Bitmap(inputStream);
            System.Drawing.Image result = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawImage(img, 0, 0, width, height);
            }
            result.CompressImage(savePath, ic);
        }

        public static void ResizeImage(this System.Drawing.Image img, int width, int height, string savePath, ImageComperssion ic = ImageComperssion.Normal)
        {
            System.Drawing.Image result = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawImage(img, 0, 0, width, height);
            }
            result.CompressImage(savePath, ic);
        }

        public static void ResizeImageByWidth(this Stream inputStream, int width, string savePath, ImageComperssion ic = ImageComperssion.Normal)
        {
            System.Drawing.Image img = new Bitmap(inputStream);
            int height = img.Height * width / img.Width;
            System.Drawing.Image result = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawImage(img, 0, 0, width, height);
            }
            result.CompressImage(savePath, ic);
        }

        public static void ResizeImageByWidth(this System.Drawing.Image img, int width, string savePath, ImageComperssion ic = ImageComperssion.Normal)
        {
            int height = img.Height * width / img.Width;
            System.Drawing.Image result = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawImage(img, 0, 0, width, height);
            }
            result.CompressImage(savePath, ic);
        }

        public static void ResizeImageByHeight(this Stream inputStream, int height, string savePath, ImageComperssion ic = ImageComperssion.Normal)
        {
            System.Drawing.Image img = new Bitmap(inputStream);
            int width = img.Width * height / img.Height;
            System.Drawing.Image result = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawImage(img, 0, 0, width, height);
            }
            result.CompressImage(savePath, ic);
        }

        public static void ResizeImageByHeight(this System.Drawing.Image img, int height, string savePath, ImageComperssion ic = ImageComperssion.Normal)
        {
            int width = img.Width * height / img.Height;
            System.Drawing.Image result = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawImage(img, 0, 0, width, height);
            }
            result.CompressImage(savePath, ic);
        }

        public static void CompressImage(this System.Drawing.Image img, string path, ImageComperssion ic)
        {
            System.Drawing.Imaging.EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Convert.ToInt32(ic));
            ImageFormat format = img.RawFormat;
            ImageCodecInfo codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == format.Guid);
            string mimeType = codec == null ? "image/jpeg" : codec.MimeType;
            ImageCodecInfo jpegCodec = null;
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < codecs.Length; i++)
            {
                if (codecs[i].MimeType == mimeType)
                {
                    jpegCodec = codecs[i];
                    break;
                }
            }
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            img.Save(path, jpegCodec, encoderParams);
        }

    }
}