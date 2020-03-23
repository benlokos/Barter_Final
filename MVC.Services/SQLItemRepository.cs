using Project.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Services
{
    public class SQLItemRepository : ItemRepository
    {
        private readonly AppDbContext Context;
        public SQLItemRepository(AppDbContext context)
        {
            Context = context;
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

    }
}
