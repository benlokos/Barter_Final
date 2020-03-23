using System;
using System.Collections.Generic;
using Project.Models;
namespace Project.Services
{
    public interface ItemRepository
    {
        IEnumerable<ItemModel> GetAllItems();
        ItemModel GetItem(string id);
        ItemModel Update(ItemModel updatedItem);
        ItemModel Add(ItemModel newItem);
        ItemModel Delete(string itemId);
    }
}