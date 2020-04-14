using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MVC_Test.Models;
using Project.Models;
using Project.Services;

namespace MVC_Test.Controllers
{
    public class SearchController : Controller
    {
        private readonly ItemRepository Repo;
        private readonly IWebHostEnvironment Env;

        public SearchController(ItemRepository Repo, IWebHostEnvironment Env)
        {
            this.Repo = Repo;
            this.Env = Env;
        }


        public IActionResult Index()
        {
            SearchModel model = new SearchModel();
            model.Populate(new List<ItemModel>(), Env);
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(SearchModel model)
        {
            if (model == null) Console.WriteLine("HttpPost model is Null");
            Console.WriteLine($"SearchString is {model.SearchStr}");
            List<ItemModel> list = new List<ItemModel>();
            
            foreach(ItemModel it in Repo.GetItemPriceRange(model.LowestPrice, model.HighestPrice)){
                if (String.IsNullOrEmpty(model.SearchStr) || it.Name.IndexOf(model.SearchStr) >= 0)
                {
                    list.Add(it);
                }
            }

            model.Populate(list, Env);
            return View(model);
        }


    }
}