using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartDomain.Models.Products.RequestAndResponce
{
    public class AddProductImage
    {
        public string ProductID { get; set; }
        public List<ProductImage> ImageNames { get; set; }
    }
    public class ProductImage
    {
        
        public string ImageName { get; set; }
        public int coverImage { get; set; }
    }
    public class UploadProductImages  
    {
        public IEnumerable<IFormFile> MultipleFiles { get; set; }
        public string ProductID { get; set; }
    }



}
