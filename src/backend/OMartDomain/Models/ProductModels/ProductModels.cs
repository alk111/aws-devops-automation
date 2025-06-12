using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.ProductModels
{
    public class ProductModels
    {
        public string ProductID { get; set; }
        public int ModelNum{ get; set; }
        public Decimal ModelQuantity{ get; set; }
        public Decimal Price{ get; set; }
        public Decimal SalePrice{ get; set; }
        public int TaxID{ get; set; }
        public string Modelname{ get; set; }
    }
}