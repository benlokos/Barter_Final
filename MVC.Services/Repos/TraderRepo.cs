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
    public class TraderRepo
    {
        private static TraderRepo Instance;

        public static void SetInstance(string ConnectionString)
        {
            if (Instance == null) Instance = new TraderRepo(ConnectionString);
        }

        public static TraderRepo GetInstance()
        { 
            return Instance;
        }

        private readonly SqlConnection sqlConnection;
        
        private TraderRepo(string ConnectionString)
        {
            sqlConnection = new SqlConnection(ConnectionString);
        }


        private IEnumerable<TraderModel> TraderCondition(string condition)

        {
            if (!String.IsNullOrEmpty(condition) && !condition.Equals("*")) condition = " where " + condition;
            else condition = "";
            Console.WriteLine("Select * from dbo.Traders" + condition);
            List<TraderModel> MyList = new List<TraderModel>();
            TraderModel curr;
            sqlConnection.Open();
            var arg = "Select * from dbo.Traders" + condition;
            using (var command = new SqlCommand(arg, sqlConnection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {   
                        curr = new TraderModel();
                        curr.ID = reader["Trader_ID"] as string;
                        curr.FirstName = reader["FirstName"] as string; 
                        curr.LastName = reader["LastName"] as string;
                        string address = reader["Addr"] as string;

                        curr.Address = AddressRepo.GetInstance().GetById(address); 

                        string rating = reader["Rating"] + "";
                        curr.rating = Double.Parse(rating);
                        curr.Photo = reader["Photo"] as byte[];
                        MyList.Add(curr);   
                    }   
                }   
            }
            sqlConnection.Close();
            return MyList;
        }

 
    

        public IEnumerable<TraderModel> GetAllTraders()
        {
            return TraderCondition("");
        }

        public TraderModel GetById(string id)
        {
            TraderModel item = null;
            int i = 0;
            foreach (TraderModel it in TraderCondition("Trader_ID = \'" + id+"\'"))
            {
                i++;
                item = it;
            }
            Assert.IsTrue(item != null);
            Assert.IsTrue(i == 1);
            return item;
        }



        public TraderModel Add(TraderModel newItem)
        {
            if (newItem == null)
            {
                Console.WriteLine("Item Is null in TraderRepo.");
                return null;
            }
       
            sqlConnection.Open();
            string sql = "Insert into dbo.Traders values(@Trader_ID,@FirtsName,@LastName, @Addr, @Rating ,@Photo)";
            Console.WriteLine(sql);
            using (SqlCommand cmd = new SqlCommand(sql, sqlConnection))
            {
                cmd.Parameters.Add("@Trader_ID", SqlDbType.VarChar,20).Value = newItem.ID;
                cmd.Parameters.Add("@FirtsName", SqlDbType.NVarChar).Value = newItem.FirstName;
                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = newItem.LastName;
                cmd.Parameters.Add("@Addr", SqlDbType.NVarChar).Value = newItem.Address.ID;
                cmd.Parameters.Add("@Rating", SqlDbType.NVarChar).Value = newItem.rating;
                cmd.Parameters.Add("@Photo", SqlDbType.VarBinary).Value = newItem.Photo;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            sqlConnection.Close();
            return newItem;
        }

        public TraderModel Delete(string traderId)
        {
            TraderModel it = TraderCondition("Trader_ID = \'" + traderId + "\'").First();
            sqlConnection.Open();
            string sql = "delete from dbo.Traders where Trader_ID = \'" + traderId + "\'";
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