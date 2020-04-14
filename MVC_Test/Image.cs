using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Test
{
    public class Image
    {
               
        public static void ImageUpload(ItemModel Item, IFormFile Photo)
        {
            if (Photo != null)
            {
                var memoryStream = new MemoryStream();
                try
                {
                    Photo.CopyTo(memoryStream);
                    Item.Photo = memoryStream.ToArray();
                }
                finally
                {
                    if (memoryStream != null) memoryStream.Dispose();
                }
            }

            Console.WriteLine("----------------------------");
            Console.WriteLine($"ItemPhoto is {Item.Photo.Length} long.");
            Console.WriteLine("----------------------------");
        }


        public static bool RemoveImage(ItemModel item, IWebHostEnvironment webHostEnvironment)
        {
            if (item == null) return false;
            string path = Path.Combine(webHostEnvironment.WebRootPath, "images");
            string photoName = item.ID + ".jpg";
            path = Path.Combine(path, photoName);

            if (!System.IO.File.Exists(path)) return false;

            File.Delete(path);

            return true;
        }

        public static string SaveImage(ItemModel item, IWebHostEnvironment webHostEnvironment)
        {
            if (item.Photo == null) return "~/images/no_image.jpg";

            string path = Path.Combine(webHostEnvironment.WebRootPath, "images");
            string photoName = item.ID + ".jpg";
            path = Path.Combine(path, photoName);
            if (System.IO.File.Exists(path)) return "~/images/" + photoName;
            var fileStream = new FileStream(path, FileMode.Create);
            var stream = new MemoryStream(item.Photo);
            IFormFile Photo = null;
            try
            {
                Photo = new FormFile(stream, 0, item.Photo.Length, photoName, "temp");
                Photo.CopyTo(fileStream);
                fileStream.Close();
            }
            finally
            {
                if (stream != null) stream.Dispose();
                if (fileStream != null) fileStream.Dispose();
            }
            return "~/images/" + photoName;
        }

        /**
      * Checks whether or not required fields for this item are filled
      * Required fields for this check are:
      * Name, Description, Price
      * @param item the item to check
      * @ return whether or not all necessary fields are filled with valid values
      */
        public static bool IsValid(ItemModel item)
        {
            if (!String.IsNullOrEmpty(item.ID))
                if (!String.IsNullOrEmpty(item.Name))
                    if (!String.IsNullOrEmpty(item.Description))
                        if (0 < (item.Price))
                            return true;
            return false;
        }


        
    }
}
