using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Models
{
    public class ReviewModel
    {
        public string ID { set; get; }
        public TraderModel Author { set; get; }
        public ItemModel Item { set; get; }
        public string Review { set; get; }
        public double Stars { set; get; }
    }
}
