using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace MVC_Test.Models
{
    public class BrowseModel
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        public BrowseModel(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public string SaveImage(ItemModel item)
        {
            return Image.SaveImage(item, webHostEnvironment);
        }
    }
}
