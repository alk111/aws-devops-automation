using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartDomain.Models.Products.RequestAndResponce
{
    public  class GetProductImagesRequest
    {
        public string ProductId { get; set; }
    }
    public class GetProductImagesResponse
    {
        public List<Image> ProductImages { get; set; }

        //public string ImageName { get; set; }
    }
    public class Image
    {
        public int PdImageID { get; set; }
        public string ImageName { get; set; }
        public string ImageURL { get; set; }

    }
}
