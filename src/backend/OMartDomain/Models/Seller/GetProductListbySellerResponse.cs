using OMartDomain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartDomain.Models.Seller
{
    public class GetProductListbySellerResponse
    {
        public List<ProductDetails>? Products { get; set; }

    }
}
