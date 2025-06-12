using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.Products.RequestAndResponce
{
    public class ProductListBySearchFilterRequest
    {

        public string categories { get; set; } = "";

        public string searchText { get; set; } = "";
    }
}