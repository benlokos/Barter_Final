using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Project.Models;
using Project.Services;
using NUnit.Framework;
using System.Data;

namespace Project.Services.Repos
{
    public class ItemRepo
    {
        private static ItemRepo Instance;
        
        public static void SetInstance(string ConnectionString)
        {
            if (Instance == null) Instance = new ItemRepo(ConnectionString);
        }

        public static ItemRepo GetInstance()
        {
            return Instance;
        }
     
        private readonly SqlConnection sqlConnection;

        public ItemRepo(string ConnectionString)
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
                        string owner = reader["OwnerId"] as string;
                        curr.OwnerId = TraderRepo.GetInstance().GetById(owner);
                        string price = reader["Price"] + "";
                        curr.Price = Int32.Parse(price);
                        curr.Photo = reader["Photo"] as byte[];
                        curr.Tags = TagAssocRepo.GetInstance().ItemTags(curr.ID);
                        MyList.Add(curr);   
                    }   
                }   
            }
            sqlConnection.Close();
            return MyList;
        }


       public ItemModel GetItem(string id)
        {
            ItemModel item = null;
            int i = 0;
            foreach (ItemModel it in ItemCondition("ID = \'" + id + "\'"))
            {
                i++;
                item = it;
            }
            Assert.IsTrue(item != null);
            Assert.IsTrue(i == 1);
            return item;
        }

        public ItemModel Add(ItemModel newItem)
        {
            if (newItem == null)
            {
                Console.WriteLine("Item Is null in ItemRepo.");
                return null;
            }
       
            sqlConnection.Open();
            string sql = "Insert into dbo.Items values(@ID,@Name,@Description, @OwnerId, @Price ,@Photo)";
            Console.WriteLine(sql);
            using (SqlCommand cmd = new SqlCommand(sql, sqlConnection))
            {
                cmd.Parameters.Add("@ID", SqlDbType.VarChar,20).Value = newItem.ID;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = newItem.Name;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = newItem.Description;
                cmd.Parameters.Add("@OwnerId", SqlDbType.NVarChar).Value = newItem.OwnerId.ID;
                cmd.Parameters.Add("@Price", SqlDbType.Int).Value = newItem.Price;
                cmd.Parameters.Add("@Photo", SqlDbType.VarBinary).Value = newItem.Photo;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            sqlConnection.Close();
            return newItem;
        }

        public ItemModel Delete(string itemId)
        {
            ItemModel it = ItemCondition("ID = \'" + itemId + "\'").First();
            sqlConnection.Open();
            string sql = "delete from dbo.Items where ID = \'" + itemId + "\'";
            Console.WriteLine(sql);
            using (SqlCommand cmd = new SqlCommand(sql, sqlConnection))
            {
                cmd.ExecuteNonQuery();
            }

            return it;
        }    
    }   
}