﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Services;
using Project.Models;
using Microsoft.AspNetCore.Hosting;
using MVC_Test.Models;

namespace MVC_Test.Controllers
{
    public class RemoveController : Controller
    {
        private readonly ItemRepository Repo;

        private readonly IWebHostEnvironment webHostEnvironment;

        [BindProperty(SupportsGet = true)]
        public string ID { set; get; }

        public RemoveController(ItemRepository Repo, IWebHostEnvironment webHostEnvironment)
        {
            this.Repo = Repo;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            Console.WriteLine($"In Index... ID: {ID}");
            ItemModel Item = Repo.GetItem(ID);
            if (Item == null) Console.WriteLine("Item to delete is Null");
            return View(new RemoveModel(webHostEnvironment, Item));
        }

        public IActionResult Delete(string ID)
        {
            if (String.IsNullOrEmpty(ID)) Console.WriteLine("String ID is Null");
            Console.WriteLine($"ID in Delete:  {ID}");
            ItemModel Item = Repo.Delete(ID);
            if (null != Item) ViewBag.Message = "Item Deleted Successfully";
            else ViewBag.Argument = "Item could not be deleted.";
            Image.RemoveImage(Item, webHostEnvironment);
            return View();
        }
    }
}