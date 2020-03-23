using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Models;


namespace Project.Services
{
    public class MockItemRepo: ItemRepository
    {
        public List<ItemModel> List { private set; get; }

        public MockItemRepo()
        {
            List = new List<ItemModel>();
            /*            
            //Cat
            ItemModel temp = new ItemModel();
            temp.Name = "Cat";
            temp.Description = "Small and Furry.";
            temp.Price = 10;
            List.Add(temp);
            //Dog
            temp = new ItemModel();
            temp.Name = "Dog";
            temp.Description = "Cute but loud.";
            temp.Price = 10;
            List.Add(temp);
            //Dog
            temp = new ItemModel();
            temp.Name = "Bird";
            temp.Description = "Small and Chirpy.";
            temp.Price = 10;
            List.Add(temp);
            */
        }

        //Returns a list of all existing items
        public IEnumerable<ItemModel> GetAllItems() { return List; }

        //Gets a specific item using the id property
        //Returns null if id does not exist
        public ItemModel GetItem(string id)
        {
            foreach(ItemModel it in List)
                if (String.Equals(it.ID, id)) return it;
            return null;
        }

        //Finds and updates the properties of a specified item
        public ItemModel Update(ItemModel updatedItem)
        {
            ItemModel temp = null;
            foreach (ItemModel it in List)
                if (String.Equals(it.ID, updatedItem.ID))
                {
                    temp = it;
                    break;
                }
            if (temp == null) return null;
            temp.Name = updatedItem.Name;
            temp.Description = updatedItem.Description;
            temp.Price = updatedItem.Price;
            temp.Photo = updatedItem.Photo;

            return temp;
        }

        //Adds an Item to the list
        public ItemModel Add(ItemModel newItem)
        {
            List.Add(newItem);
            if (this.GetItem(newItem.ID) == null) return null;
            return newItem;
        }
        
        
        //Removes an item from the list, if it exists
       public ItemModel Delete(string itemId)
        {
            ItemModel temp = null;
            foreach (ItemModel it in List)
                if (String.Equals(it.ID, itemId)) 
                {
                    temp = it; 
                    break; 
                }
            if (temp != null) List.Remove(temp);
            return temp;
        }
    }



}
