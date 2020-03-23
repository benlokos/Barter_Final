using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class ItemModel
    {
        public string Name { set; get; }
        public string ID { set; get; }
        public string Description { set; get; }
        public string OwnerId { set; get; }
        public int Price { set; get; }
        public byte[] Photo{ set; get; }
    }
}
