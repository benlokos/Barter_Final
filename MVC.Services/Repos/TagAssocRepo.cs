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
    public class TagAssocRepo
    {
        private static TagAssocRepo Instance;
       
        
        public static void SetInstance(string ConnectionString) { 
            if (Instance == null) Instance = new TagAssocRepo(ConnectionString);
        }


        public static TagAssocRepo GetInstance()
        {
            return Instance;
        }

        private readonly SqlConnection sqlConnection;
        private TagAssocRepo(string ConnectionString)
        {
            sqlConnection = new SqlConnection(ConnectionString);
        }

        private List<ItemModel> ItemsWithTag(string condition)
        {
            if (!String.IsNullOrEmpty(condition) && !condition.Equals("*")) condition = " where Tag_ID = \'" + condition + "\'";
            else condition = "";
            Console.WriteLine("Select * from dbo.TagAssoc" + condition);
            List<ItemModel> MyList = new List<ItemModel>();
            ItemModel curr;
            sqlConnection.Open();
            var arg = "Select * from dbo.TagAssoc" + condition;
            using (var command = new SqlCommand(arg, sqlConnection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {   
                        string itemid = reader["Tag_ID"] as string;
                        curr = ItemRepo.GetInstance().GetItem(itemid);
                        MyList.Add(curr);   
                    }   
                }   
            }
            sqlConnection.Close();
            return MyList;
        }


        public List<TagModel> ItemTags(string ItemId)
        {
            if (!String.IsNullOrEmpty(ItemId) && !ItemId.Equals("*")) ItemId = " where Item_ID = \'" + ItemId + "\'";
            else ItemId = "";
            Console.WriteLine("Select * from dbo.TagAssoc" + ItemId);
            List<TagModel> MyList = new List<TagModel>();
            TagModel curr;
            sqlConnection.Open();
            var arg = "Select * from dbo.TagAssoc" + ItemId;
            using (var command = new SqlCommand(arg, sqlConnection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string itemid = reader["Tag_ID"] as string;
                        curr = TagRepo.GetInstance().GetById(itemid);
                        MyList.Add(curr);
                    }
                }
            }
            sqlConnection.Close();
            return MyList;
        }

        public ItemModel AddAll(ItemModel Item)
        {
            if (Item == null)
            {
                Console.WriteLine("Item Is null in TagAssocRepo.");
                return null;
            }
       
            sqlConnection.Open();
            foreach (TagModel current in Item.Tags) {
                string sql = "Insert into dbo.TagAssoc values(@Tag_ID,@Item_ID,@Addr)";
                Console.WriteLine(sql);
                using (SqlCommand cmd = new SqlCommand(sql, sqlConnection))
                {
                    cmd.Parameters.Add("@Tag_ID", SqlDbType.VarChar, 20).Value = current.ID;
                    cmd.Parameters.Add("@Item_ID", SqlDbType.VarChar, 20).Value = Item.ID;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
            sqlConnection.Close();
            return Item;
        }

        public ItemModel DeleteAll(ItemModel Item)
        {
            if (Item == null)
            {
                Console.WriteLine("Item Is null in TagAssocRepo.");
                return null;
            }

            sqlConnection.Open();
           
                string sql = "Delete from dbo.TagAssoc where Item_ID = \'"+Item.ID+"\'";
                Console.WriteLine(sql);
                using (SqlCommand cmd = new SqlCommand(sql, sqlConnection))
                {
                    cmd.ExecuteNonQuery();
                }
           
            sqlConnection.Close();
            return Item;
        }


        public ItemModel AddTag(ItemModel Item, TagModel tag)
        {
            if (Item == null)
            {
                Console.WriteLine("Item Is null in TagAssocRepo.");
                return null;
            }
            if (tag== null)
            {
                Console.WriteLine("Tag Is null in TagAssocRepo.");
                return null;
            }

            sqlConnection.Open();
            
                string sql = "Insert into dbo.TagAssoc values(@Tag_ID,@Item_ID,@Addr)";
                Console.WriteLine(sql);
                using (SqlCommand cmd = new SqlCommand(sql, sqlConnection))
                {
                    cmd.Parameters.Add("@Tag_ID", SqlDbType.VarChar, 20).Value = tag.ID;
                    cmd.Parameters.Add("@Item_ID", SqlDbType.VarChar, 20).Value = Item.ID;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
           
            sqlConnection.Close();
            return Item;
        }

        public ItemModel DeleteTag(ItemModel Item, TagModel tag)
        {
            if (Item == null)
            {
                Console.WriteLine("Item Is null in TagAssocRepo.");
                return null;
            }
            if (tag == null)
            {
                Console.WriteLine("Tag Is null in TagAssocRepo.");
                return null;
            }

            sqlConnection.Open();

            string sql = "Delete from dbo.TagAssoc where Item_ID = \'" + Item.ID + "\' ";
            sql += "and Tag_ID = \'"+tag.ID+"\'";
            Console.WriteLine(sql);
            using (SqlCommand cmd = new SqlCommand(sql, sqlConnection))
            {
                cmd.ExecuteNonQuery();
            }

            sqlConnection.Close();
            return Item;
        }
    }   
}