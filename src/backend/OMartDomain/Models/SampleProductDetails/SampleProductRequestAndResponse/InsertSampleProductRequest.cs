using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.SampleProductDetails.SampleProductRequestAndResponse
{
    public class InsertSampleProductRequest
    {
         public string BrandName{ get; set; }
        public string ProductName{ get; set; }
        public string ProductDescription{ get; set; }
    }
}