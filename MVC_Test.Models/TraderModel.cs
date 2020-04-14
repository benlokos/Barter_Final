using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Models
{
    public class TraderModel
    {
        public string ID { set; get; }
        public string FirstName { set; get; }
        public string LastName{ set; get; }
        public AddressModel Address { set; get; }
        public double rating;
        public byte[] Photo { set; get; }
        
        public TraderModel() { }

        public TraderModel(string ID, string FirstName, string LastName)
        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            Address = null;
            rating = 2.5;
        }
    }
}
