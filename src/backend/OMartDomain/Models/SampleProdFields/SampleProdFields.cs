using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.SampleProdFields
{
    public class SampleProdFields
    {
        public int  SampleProdID{ get; set; }
        public int  SampleProdOptionalFieldID{ get; set; }
        public string fieldName{ get; set; }
        public string fieldValue{ get; set; }
    }
}