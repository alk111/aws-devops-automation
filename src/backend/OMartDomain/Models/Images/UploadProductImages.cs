using Microsoft.AspNetCore.Http;
using OMartDomain.Models.Products.RequestAndResponce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartDomain.Models.Images
{
    public class UploadProductImages : AddProductImage
    {
        IEnumerable<IFormFile> MultipleFiles { get; set; }
    }
    //public class AddProductImage
    //{
    //    public string ProductID { get; set; }

    //    public List<ProductImage> ImageNames { get; set; }
    //}
    //public class ProductImage
    //{

    //    public string ImageName { get; set; }
    //}
}
