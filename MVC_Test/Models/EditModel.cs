using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Models;
using Project.Services;


namespace MVC_Test.Models
{
    public class EditModel
    {
        private ItemRepository Repo;
        public string ID;
        public ItemModel Item { set; get; }
        public IFormFile Photo { set; get; }

        public EditModel(){}

        public void Populate(ItemRepository Repo, string ID)
        {
            this.ID = ID;
            this.Repo = Repo;
            Console.WriteLine($"Id in edit: {ID}");
        
            this.Item = Repo.GetItem(ID);
            if (Item == null) Console.WriteLine("Item is Null in EditModel");
        }
    

    }
}
