using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.SampleProductDetails.SampleProductRequestAndResponse
{
    public class GetSampleProductDetailsResponse
    {
         public string message{ get; set; }
        public List<SampleProductDetails> samples { get; set; }
    }
}