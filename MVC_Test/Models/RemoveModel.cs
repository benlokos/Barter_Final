using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Test.Models
{
    public class RemoveModel
    {
        public ItemModel Item { private set; get; }

        private readonly IWebHostEnvironment webHostEnvironment;
        
        public RemoveModel(IWebHostEnvironment webHost, ItemModel Item)
        {
            this.Item = Item;
            this.webHostEnvironment = webHost;
            if(Item == null)Console.WriteLine("Item is Null in RemoveModel");
        }

        public string SaveImage(ItemModel item)
        {
            return Image.SaveImage(item, webHostEnvironment);
        }

    }
}
