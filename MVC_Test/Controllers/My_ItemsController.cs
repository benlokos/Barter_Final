using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Services;
using Project.Models;
using MVC_Test.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace MVC_Test.Controllers
{
    public class My_ItemsController : Controller
    {
        private readonly ItemRepository Repo;
        private readonly IWebHostEnvironment webHostEnvironment;
        public My_ItemsController(ItemRepository Repo, IWebHostEnvironment webHostEnvironment)
        {
            this.Repo = Repo;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: Post
        public ActionResult Index()
        {
            ViewBag.List = Repo.GetAllItems();
            ViewBag.ToSubmit = new ItemSubmissionModel();
            return View(new BrowseModel(webHostEnvironment));
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View() ;
        }

        [HttpPost]
        public ActionResult Create(ItemSubmissionModel Model)
        {
            //Set unique ID for this item
            Model.Internal.ID = Model.Internal.Name + Model.Internal.Price;
            //Is going to change later on
            Console.WriteLine($"Id: {Model.Internal.ID}");
            Console.WriteLine($"Name: {Model.Internal.Name}");
            Console.WriteLine($"Description: {Model.Internal.Description}");
            Console.WriteLine($"Price: {Model.Internal.Price}");
            

            //Check that required ItemModel fields were filled with valid values
            if (!IsValidItem(Model.Internal) || Model.Photo == null) return View();
            Image.ImageUpload(Model.Internal, Model.Photo);


            if(null != Repo.Add(Model.Internal))
                Console.WriteLine("Submited Succeslfully!");
            return Redirect("~/My_Items/");
        }

        /**
         * Checks whether or not required fields for this item are filled
         * Required fields for this check are:
         * Name, Description, Price
         * @param item the item to check
         * @ return whether or not all necessary fields are filled with valid values
         */
        private bool IsValidItem(ItemModel item)
        {
            if(!String.IsNullOrEmpty(item.ID))
               if (!String.IsNullOrEmpty(item.Name))
                    if (!String.IsNullOrEmpty(item.Description))
                        if (0 < (item.Price))
                            return true;
            return false;
        }

    }
}