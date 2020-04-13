using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Models;
using Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Test.Models
{
    public class SearchModel
    {
        public string SearchStr { set; get; }
        public int LowestPrice { set; get; }
        public int HighestPrice { set; get; }
        private IWebHostEnvironment Env { set; get; }


        public IEnumerable<ItemModel> ViewList { private set; get; }

        public SearchModel()
        {
            SearchStr = "*";
            LowestPrice = 0;
            HighestPrice = Int32.MaxValue;
        }
        
        public void Populate(IEnumerable<ItemModel> ViewList, IWebHostEnvironment Env)
        {
            this.Env = Env;
            this.ViewList = ViewList;
        }

        public string SaveImage(ItemModel Item)
        {
            return Image.SaveImage(Item, Env);
        }

    }
}
