using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Project.Models;
namespace MVC_Test.Models
{
    public class ItemSubmissionModel
    {
        public ItemModel Internal { set; get; }
        
        public IFormFile Photo { set; get; }        
            
        public ItemSubmissionModel()
        {
            Internal = new ItemModel();
            
        } 
    }
}
