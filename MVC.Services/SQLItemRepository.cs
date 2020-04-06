using Project.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Services
{
    public class SQLItemRepository : ItemRepository
    {
        private readonly ManualItemRepo ManRepo;
        private readonly AppDbContext Context;
        public SQLItemRepository(AppDbContext context)
        {
            Context = context;
            ManRepo = new ManualItemRepo("server=10.12.227.17,1433;database=Barter_MVC;Persist Security Info=True;User ID=Admin;Password=p@ssw0d");
        }

        public ItemModel Add(ItemModel newItem)
        {
            if (Context.Items.Find(newItem.ID) != null) return null;
            Context.Items.Add(newItem);
            Context.SaveChanges();
            return newItem;
        }

        public ItemModel Delete(string itemId)
        {
            ItemModel temp = Context.Items.Find(itemId);
            if (temp == null) Console.WriteLine("No Such Element");
            else
            {
                Context.Items.Remove(temp);
                Context.SaveChanges();
            }
            return temp;
        }

        public IEnumerable<ItemModel> GetAllItems()
        {
            return Context.Items;
        }

        public ItemModel GetItem(string id)
        {
            return Context.Items.Find(id);
        }

        public ItemModel Update(ItemModel updatedItem)
        {
            var item = Context.Items.Attach(updatedItem);
            item.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
            return updatedItem;
        }

        public IEnumerable<ItemModel> GetItemUnderPrice(int i)
        {
            return ManRepo.GetItemUnderPrice(i);
        }

        public IEnumerable<ItemModel> GetItemOverPrice(int i)
        {
            return ManRepo.GetItemOverPrice(i);
        }

        public IEnumerable<ItemModel> GetItemPriceRange(int lower, int higher)
        {
            return ManRepo.GetItemPriceRange(lower, higher);
        }

        public IEnumerable<ItemModel> GetItemOwner(int owner)
        {
            return ManRepo.GetItemOwner(owner);
        }


    }
}
