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
    public class AddressRepo
    {
        private static AddressRepo Instance;
       
        
        public static void SetInstance(string ConnectionString) { 
            if (Instance == null) Instance = new AddressRepo(ConnectionString);
        }


        public static AddressRepo GetInstance()
        {
            return Instance;
        }

        private readonly SqlConnection sqlConnection;
        private AddressRepo(string ConnectionString)
        {
            sqlConnection = new SqlConnection(ConnectionString);
        }
        private IEnumerable<AddressModel> AddressCondition(string condition)

        {
            if (!String.IsNullOrEmpty(condition) && !condition.Equals("*")) condition = " where " + condition;
            else condition = "";
            Console.WriteLine("Select * from dbo.Locations" + condition);
            List<AddressModel> MyList = new List<AddressModel>();
            AddressModel curr;
            sqlConnection.Open();
            var arg = "Select * from dbo.Locations" + condition;
            using (var command = new SqlCommand(arg, sqlConnection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {   
                        curr = new AddressModel();
                        curr.ID = reader["Addr_ID"] as string;
                        curr.FullName = reader["FullName"] as string; 
                        curr.Address = reader["Addr"] as string;
                        MyList.Add(curr);   
                    }   
                }   
            }
            sqlConnection.Close();
            return MyList;
        }


        public IEnumerable<AddressModel> GetAllAddresses()
        {
            return AddressCondition("");
        }

        public AddressModel GetById(string id)
        {
            AddressModel item = null;
            int i = 0;
            foreach (AddressModel it in AddressCondition("Addr_ID = \'" + id+"\'"))
            {
                i++;
                item = it;
            }
            Assert.IsTrue(item != null);
            Assert.IsTrue(i == 1);
            return item;
        }

        public AddressModel Add(AddressModel newItem)
        {
            if (newItem == null)
            {
                Console.WriteLine("Item Is null in AddressRepo.");
                return null;
            }
       
            sqlConnection.Open();
            string sql = "Insert into dbo.Locations values(@Addr_ID,@FullName,@Addr)";
            Console.WriteLine(sql);
            using (SqlCommand cmd = new SqlCommand(sql, sqlConnection))
            {
                cmd.Parameters.Add("@Addr_ID", SqlDbType.VarChar,20).Value = newItem.ID;
                cmd.Parameters.Add("@FullName", SqlDbType.NVarChar).Value = newItem.FullName;
                cmd.Parameters.Add("@Addr", SqlDbType.NVarChar).Value = newItem.Address;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            sqlConnection.Close();
            return newItem;
        }

        public AddressModel Delete(string AdressId)
        {
            AddressModel it = AddressCondition("Adress_ID = \'" + AdressId + "\'").First();
            sqlConnection.Open();
            string sql = "delete from dbo.Locations where Adress_ID = \'" + AdressId + "\'";
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