using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Services;
using MVC_Test.Models;

namespace MVC_Test.Controllers
{
    public class BrowseController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public ItemRepository Repo{ private set; get; }

        public IEnumerable<ItemModel> List { private set; get;  }
       
        public BrowseController(ItemRepository Repo, IWebHostEnvironment webHostEnvironment)
        {
            this.Repo = Repo;
            this.webHostEnvironment = webHostEnvironment;
    }

        // GET: Browse
        public ActionResult Index()
        {
            ViewBag.List = Repo.GetAllItems();
            return View(new BrowseModel(webHostEnvironment));
        }
    }
}