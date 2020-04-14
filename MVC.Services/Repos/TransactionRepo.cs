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
    public class TransactionRepo
    {
        private static TransactionRepo Instance;

        public static void SetInstance(string ConnectionString)
        {
            if (Instance == null) Instance = new TransactionRepo(ConnectionString);
        }

        public static TransactionRepo GetInstance()
        { 
            return Instance;
        }

        private readonly SqlConnection sqlConnection;
        
        private TransactionRepo(string ConnectionString)
        {
            sqlConnection = new SqlConnection(ConnectionString);
        }


        private IEnumerable<TransactionModel> TransactionCondition(string condition)

        {
            if (!String.IsNullOrEmpty(condition) && !condition.Equals("*")) condition = " where " + condition;
            else condition = "";
            Console.WriteLine("Select * from dbo.Transactions" + condition);
            List<TransactionModel> MyList = new List<TransactionModel>();
            TransactionModel curr;
            sqlConnection.Open();
            var arg = "Select * from dbo.Transactions" + condition;
            using (var command = new SqlCommand(arg, sqlConnection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {   
                        curr = new TransactionModel();
                        curr.ID = reader["Trans_ID"] as string;
                        string b = reader["Buyer_ID"] as string;
                        string s = reader["Seller_ID"] as string;
                        string i = reader["Item_ID"] as string;

                        curr.Buyer = TraderRepo.GetInstance().GetById(b);
                        curr.Seller = TraderRepo.GetInstance().GetById(s);
                        curr.Item = ItemRepo.GetInstance().GetItem(i); 
                        MyList.Add(curr);   
                    }   
                }   
            }
            sqlConnection.Close();
            return MyList;
        }

 
    

        public IEnumerable<TransactionModel> GetAllTransactions()
        {
            return TransactionCondition("");
        }

        public TransactionModel GetById(string id)
        {
            TransactionModel item = null;
            int i = 0;
            foreach (TransactionModel it in TransactionCondition("Trans_ID = \'" + id+"\'"))
            {
                i++;
                item = it;
            }
            Assert.IsTrue(item != null);
            Assert.IsTrue(i == 1);
            return item;
        }



        public TransactionModel Add(TransactionModel transaction)
        {
            if (transaction == null)
            {
                Console.WriteLine("Item Is null in TransactionRepo.");
                return null;
            }
       
            sqlConnection.Open();
            string sql = "Insert into dbo.Transactions values(@Trans_ID,@Buyer_ID,@Seller_ID, @Item_ID)";
            Console.WriteLine(sql);
            using (SqlCommand cmd = new SqlCommand(sql, sqlConnection))
            {
                cmd.Parameters.Add("@Transaction_ID", SqlDbType.VarChar,20).Value = transaction.ID;
                cmd.Parameters.Add("@Buyer_ID", SqlDbType.NVarChar).Value = transaction.Buyer.ID;
                cmd.Parameters.Add("@Seller_ID", SqlDbType.NVarChar).Value = transaction.Seller.ID;
                cmd.Parameters.Add("@Item_ID", SqlDbType.NVarChar).Value = transaction.Item.ID;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            sqlConnection.Close();
            return transaction;
        }

        public TransactionModel Delete(string TransactionId)
        {
            TransactionModel it = TransactionCondition("Trans_ID = \'" + TransactionId + "\'").First();
            sqlConnection.Open();
            string sql = "delete from dbo.Transactions where Trans_ID = \'" + TransactionId + "\'";
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