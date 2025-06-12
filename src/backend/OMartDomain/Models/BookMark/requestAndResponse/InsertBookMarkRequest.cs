using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.BookMark.requestAndResponse
{
    public class InsertBookMarkRequest
    {
        
       public string UserID{ get; set; } 
        public string Type{ get; set; } 
        public string ID{ get; set; }
    }
}