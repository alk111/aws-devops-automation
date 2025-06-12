using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.ProductModels.RequestAndResponse
{
    public class GetProductModelsByProductIDResponse
    {
        public string message { get; set; }
        public List<ProductModels> productModels{ get; set; }
    }
}