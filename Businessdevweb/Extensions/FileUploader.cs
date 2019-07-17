using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Businessdevweb.Extensions
{
    public static class FileUploader
    {

        private static void CreateIfMissing(string path)
        {
            var folderExists = Directory.Exists(Path.Combine(Path.Combine(HttpContext.Current.Server.MapPath("~"), "Content" + path)));
            if (!folderExists)
                Directory.CreateDirectory(Path.Combine(HttpContext.Current.Server.MapPath("~" ), "Content" + path));
        }

        /// <summary>
        ///     Delete File
        /// </summary>
        /// <param name="path">Directory+FileName</param>
        /// <returns></returns>
        public static void DeleteFile(string path)
        {
            var FullPath = Path.Combine(Path.Combine(HttpContext.Current.Server.MapPath("~"), "Content" + path));
            if (File.Exists(FullPath))
            {
                try
                {
                    File.Delete(FullPath);

                }
                catch (Exception e)
                {
                    throw new ArgumentException("فایل مورد نظر به دلیل خطای: " + e.Message + " حذف نشد!");
                }
            }

        }

        public static string SaveFile(HttpPostedFileBase file, string fullPath, string name, bool checkFileExists = false)
        {
            //int fileSize = file.ContentLength;
            string type = Path.GetExtension(file.FileName);
            CreateIfMissing(fullPath);
            // Verify that the user selected a file
            if (file.ContentLength > 0)
            {
                var regx = new Regex("([\\W_]+)+?", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var fileName = regx.Replace(name.Trim(), "-");
                var path = Path.Combine(Path.Combine(HttpContext.Current.Server.MapPath("~"), "Content" + fullPath, fileName + type));

                if (checkFileExists && File.Exists(path))
                {
                    throw new ArgumentException("فایل مورد نظر با این نام (" + name + ") در سیستم موجود است. ");
                }

                file.SaveAs(path);
                //using (var fileStream = new FileStream(path, FileMode.Create))
                //{
                //    file.CopyTo(fileStream);
                //}
                return fileName + type;
            }
            else
            {
                return null;
            }
        }

    }
}