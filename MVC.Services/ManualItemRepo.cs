using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Project.Models;
using Project.Services;
using NUnit.Framework;

namespace Project.Services
{
    public class ManualItemRepo: ItemRepository
    {
        private readonly SqlConnection sqlConnection;

        public ManualItemRepo(string ConnectionString)
        {
            sqlConnection = new SqlConnection(ConnectionString);
        }
        private IEnumerable<ItemModel> ItemCondition(string condition)
        {
            if (!String.IsNullOrEmpty(condition) && !condition.Equals("*")) condition = " where " + condition;
            else condition = "";
            Console.WriteLine("Select * from dbo.Items" + condition);
            List<ItemModel> MyList = new List<ItemModel>();
            ItemModel curr;
            sqlConnection.Open();
            var arg = "Select * from dbo.Items" + condition;
            using (var command = new SqlCommand(arg, sqlConnection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {   
                        curr = new ItemModel();
                        curr.ID = reader["ID"] as string;
                        curr.Name = reader["Name"] as string; 
                        curr.Description = reader["Description"] as string;
                        curr.OwnerId = reader["OwnerId"] as string;
                        string price = reader["Price"] + "";
                        curr.Price = Int32.Parse(price);
                        curr.Photo = reader["Photo"] as byte[];
                        MyList.Add(curr);   
                    }   
                }   
            }
            return MyList;
        }

        public IEnumerable<ItemModel> GetItemUnderPrice(int i)
        {
            string condition = "Price < " + i;
            return ItemCondition(condition);
        }

        public IEnumerable<ItemModel> GetItemOverPrice(int i)
        {
            string condition = "Price > " + i;
            return ItemCondition(condition);
        }

        public IEnumerable<ItemModel> GetItemPriceRange(int lower, int higher)
        {
            string condition = "Price < " + higher + " & Price > " + lower;
            return ItemCondition(condition);
        }

        public IEnumerable<ItemModel> GetItemOwner(int owner)
        {
            string condition = "OwnerId = " + owner;
            return ItemCondition(condition);
        }


        IEnumerable<ItemModel> ItemRepository.GetAllItems()
        {
            return ItemCondition("");
        }

        ItemModel ItemRepository.GetItem(string id)
        {
            ItemModel item = null;
            int i = 0;
            foreach (ItemModel it in ItemCondition("ID = " + id))
            {
                i++;
                item = it;
            }
            Assert.IsTrue(item != null);
            Assert.IsTrue(i == 1);
            return item;
        }

        ItemModel ItemRepository.Update(ItemModel updatedItem)
        {
            throw new NotImplementedException();
        }

        ItemModel ItemRepository.Add(ItemModel newItem)
        {
            throw new NotImplementedException();
        }

        ItemModel ItemRepository.Delete(string itemId)
        {
            throw new NotImplementedException();
        }
    }



}
