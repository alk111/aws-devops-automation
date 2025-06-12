using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.Categorie.CategorieRequestAndResponse
{
    public class CategorieResponse
    {
        public string message{ get; set; }
        public List<Categoreis> categoreis { get; set; }
    }
}