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
        IEnumerable<ItemModel> GetItemUnderPrice(int i);
        IEnumerable<ItemModel> GetItemOverPrice(int i);
        IEnumerable<ItemModel> GetItemPriceRange(int lower, int higher);
        IEnumerable<ItemModel> GetItemOwner(int owner);
    }
}