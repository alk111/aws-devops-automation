using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.Products.RequestAndResponce
{
    public class GetProductListBySellerResponse
    {
                public List<ProductDetails>? Products { get; set; }
    }
}