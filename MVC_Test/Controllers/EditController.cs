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
    public class EditController : Controller
    {
        private readonly ItemRepository Repo;

        private readonly IWebHostEnvironment webHostEnvironment;

        [BindProperty(SupportsGet = true)]
        public string ID { set; get; }

        private ItemModel OriginalItem;

        public EditController(ItemRepository Repo, IWebHostEnvironment webHostEnvironment)
        {
            this.Repo = Repo;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            Console.WriteLine($"In Index... ID: {ID}");
            OriginalItem = Repo.GetItem(ID);
            if (OriginalItem == null) Console.WriteLine("Item to edit is Null");
            EditModel model = new EditModel();
            model.Populate(Repo, ID);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditModel edit)
        {
            edit.Item.ID = ID;
            Image.RemoveImage(edit.Item, webHostEnvironment);
            Image.ImageUpload(edit.Item, edit.Photo);
            if (!edit.Item.IsValidItem()) Console.WriteLine("Not a valid Item");
   
            

            ItemModel it = Repo.Update(edit.Item);
            ViewBag.Message = "Item Edited Successfully";
            if (!it.IsValidItem()) ViewBag.Message = "Item could not be edited.";
            if (it == null) Console.WriteLine("Unsuccessfull");
            else Console.WriteLine("Successfull");
            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
    }
}