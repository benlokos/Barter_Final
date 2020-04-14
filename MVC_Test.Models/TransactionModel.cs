using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Models
{
    public class TransactionModel
    {
        public string ID { set; get; }
        public TraderModel Buyer { set; get;}
        public TraderModel Seller { set; get; }
        public ItemModel Item { set; get; }
    }
}
