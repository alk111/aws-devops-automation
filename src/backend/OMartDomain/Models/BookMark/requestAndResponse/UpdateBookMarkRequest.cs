using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.BookMark.requestAndResponse
{
    public class UpdateBookMarkRequest
    {
        public int BookMarkID{ get; set; }
        public string Type{ get; set; } 
        public string ID{ get; set; }
    }
}