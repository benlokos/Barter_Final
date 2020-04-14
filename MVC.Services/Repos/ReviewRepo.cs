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
    public class ReviewRepo
    {
        private static ReviewRepo Instance;
       
        
        public static void SetInstance(string ConnectionString) { 
            if (Instance == null) Instance = new ReviewRepo(ConnectionString);
        }


        public static ReviewRepo GetInstance()
        {
            return Instance;
        }

        private readonly SqlConnection sqlConnection;
        private ReviewRepo(string ConnectionString)
        {
            sqlConnection = new SqlConnection(ConnectionString);
        }
      
        
        private IEnumerable<ReviewModel> ReviewCondition(string condition)

        {
            if (!String.IsNullOrEmpty(condition) && !condition.Equals("*")) condition = " where " + condition;
            else condition = "";
            Console.WriteLine("Select * from dbo.Reviews" + condition);
            List<ReviewModel> MyList = new List<ReviewModel>();
            ReviewModel curr;
            sqlConnection.Open();
            var arg = "Select * from dbo.Reviews" + condition;
            using (var command = new SqlCommand(arg, sqlConnection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {   
                        curr = new ReviewModel();
                        curr.ID = reader["Trans_ID"] as string;
                        string a = reader["Author"] as string;
                        string i = reader["Item_ID"] as string;
                        curr.Author = TraderRepo.GetInstance().GetById(a);
                        curr.Item = ItemRepo.GetInstance().GetItem(i);
                        curr.Review = reader["Review"] as string;
                        a = reader["stars"] + "";
                        curr.Stars = Double.Parse(a);
                        MyList.Add(curr);   
                    }   
                }   
            }
            sqlConnection.Close();
            return MyList;
        }


        public IEnumerable<ReviewModel> GetAllReviews()
        {
            return ReviewCondition("");
        }

        public ReviewModel GetById(string id)
        {
            ReviewModel item = null;
            int i = 0;
            foreach (ReviewModel it in ReviewCondition("Trans_ID = \'" + id+"\'"))
            {
                i++;
                item = it;
            }
            Assert.IsTrue(item != null);
            Assert.IsTrue(i == 1);
            return item;
        }

        public ReviewModel Add(ReviewModel newItem)
        {
            if (newItem == null)
            {
                Console.WriteLine("Item Is null in ReviewRepo.");
                return null;
            }
       
            sqlConnection.Open();
            string sql = "Insert into dbo.Reviews values(@Trans_ID,@Author,@Item_ID,@Review,@stars)";
            Console.WriteLine(sql);
            using (SqlCommand cmd = new SqlCommand(sql, sqlConnection))
            {
                cmd.Parameters.Add("@Addr_ID", SqlDbType.VarChar,20).Value = newItem.ID;
                cmd.Parameters.Add("@Author", SqlDbType.VarChar, 20).Value = newItem.Author.ID;
                cmd.Parameters.Add("@Item_ID", SqlDbType.VarChar, 20).Value = newItem.Item.ID;
                cmd.Parameters.Add("@Review", SqlDbType.NVarChar).Value = newItem.Review;
                cmd.Parameters.Add("@stars", SqlDbType.Int).Value = (int)newItem.Stars;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            sqlConnection.Close();
            return newItem;
        }

        public ReviewModel Delete(string AdressId)
        {
            ReviewModel it = ReviewCondition("Trans_ID = \'" + AdressId + "\'").First();
            sqlConnection.Open();
            string sql = "delete from dbo.Reviews where Trans_ID = \'" + AdressId + "\'";
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