using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.BookMark
{
    public class BookMark
    {
        public int BookMarkID { get; set; }
        //public string  UserID{ get; set; }
        //public string  Type{ get; set; }
        //public string ID{ get; set; }
        public string product_id { get; set; }
        public string productName { get; set; }
        public string details { get; set; }
        public string establishment_name { get; set; }


    }
}