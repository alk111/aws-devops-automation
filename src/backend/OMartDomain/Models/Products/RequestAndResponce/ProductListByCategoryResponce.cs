using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.Products.RequestAndResponce
{
    public class ProductListByCategoryResponce
    {
        public List<ProductDetails> products { get; set; }
    }
}