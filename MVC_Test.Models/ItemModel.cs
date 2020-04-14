using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class ItemModel
    {
        public string ID { set;get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public TraderModel OwnerId { set; get; }
        public int Price { set; get; }
        public byte[] Photo{ set; get; }
        public List<TagModel> Tags { set; get; }

        public bool IsValidItem()
        {
            if (!String.IsNullOrEmpty(this.ID))
                if (!String.IsNullOrEmpty(this.Name))
                    if (!String.IsNullOrEmpty(this.Description))
                        if (0 < (this.Price))
                            return true;
            return false;
        }
    }
}
