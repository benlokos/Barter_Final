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
    public class TagRepo
    {
        private static TagRepo Instance;
       
        
        public static void SetInstance(string ConnectionString) { 
            if (Instance == null) Instance = new TagRepo(ConnectionString);
        }


        public static TagRepo GetInstance()
        {
            return Instance;
        }

        private readonly SqlConnection sqlConnection;
        private TagRepo(string ConnectionString)
        {
            sqlConnection = new SqlConnection(ConnectionString);
        }
        private IEnumerable<TagModel> AddressCondition(string condition)

        {
            if (!String.IsNullOrEmpty(condition) && !condition.Equals("*")) condition = " where " + condition;
            else condition = "";
            Console.WriteLine("Select * from dbo.Tags" + condition);
            List<TagModel> MyList = new List<TagModel>();
            TagModel curr;
            sqlConnection.Open();
            var arg = "Select * from dbo.Tags" + condition;
            using (var command = new SqlCommand(arg, sqlConnection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {   
                        curr = new TagModel();
                        curr.ID = reader["Tag_ID"] as string;
                        curr.tag = reader["TagName"] as string; 
                        MyList.Add(curr);   
                    }   
                }   
            }
            sqlConnection.Close();
            return MyList;
        }


        public IEnumerable<TagModel> GetAllAddresses()
        {
            return AddressCondition("");
        }

        public TagModel GetById(string id)
        {
            TagModel item = null;
            int i = 0;
            foreach (TagModel it in AddressCondition("Tag_ID = \'" + id+"\'"))
            {
                i++;
                item = it;
            }
            Assert.IsTrue(item != null);
            Assert.IsTrue(i == 1);
            return item;
        }

        public TagModel Add(TagModel newItem)
        {
            if (newItem == null)
            {
                Console.WriteLine("Item Is null in TagRepo.");
                return null;
            }
       
            sqlConnection.Open();
            string sql = "Insert into dbo.Tags values(@Tag_ID,@TagName)";
            Console.WriteLine(sql);
            using (SqlCommand cmd = new SqlCommand(sql, sqlConnection))
            {
                cmd.Parameters.Add("@Tag_ID", SqlDbType.VarChar,20).Value = newItem.ID;
                cmd.Parameters.Add("@TagName", SqlDbType.NVarChar).Value = newItem.tag;

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            sqlConnection.Close();
            return newItem;
        }

        public TagModel Delete(string TagId)
        {
            TagModel it = AddressCondition("Tag_ID = \'" + TagId + "\'").First();
            sqlConnection.Open();
            string sql = "delete from dbo.Tags where Tag_ID = \'" + TagId + "\'";
            Console.WriteLine(sql);
            using (SqlCommand cmd = new SqlCommand(sql, sqlConnection))
            {
                cmd.ExecuteNonQuery();
            }
            sqlConnection.Close();

            return it;
        }    
    }   
}