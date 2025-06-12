using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OMartDomain.Models.Products;
using OMartDomain.Models.Wrapper;
using OMartApplication.Services;
using System.Text.Json;
using OMartDomain.Models.Products.RequestAndResponce;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using Microsoft.AspNetCore.Components.Forms;
using ImageMagick;
using static System.Net.Mime.MediaTypeNames;

namespace OMAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class ProductController : ControllerBase
    {

        private readonly IProductService iProductService;

        public ProductController(IProductService _iProductService)
        {
            iProductService = _iProductService;
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchProducts(SearchRequest searchRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var searchResult = await iProductService.SearchProductByName(searchRequest);
                if (searchResult.Succeeded)
                {
                    return Ok(searchResult);
                }
                else
                {
                    return Conflict(searchResult);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Result.Fail($"An error occurred in product controller: {ex.Message}"));
            }
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var result = await iProductService.GetAllCategories();
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                else
                {
                    return Conflict(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Result.Fail($"An error occurred in product controller: {ex.Message}"));
            }
        }

        [HttpGet("GetDeliveryTimeSlotsByEntityID/{EnityID}")]
        public async Task<GetDeliveryTimeSlotsByEntityIDResponse> GetDeliveryTimeSlotsByEntityID(string EnityID)
        {
            return await iProductService.GetDeliveryTimeSlotsByEntityID(EnityID);
        }

        [HttpGet("getProductDeatils/{productId}")]
        public async Task<IActionResult> getProductDeatils(string productId)
        {
            try
            {
                var result = await iProductService.getProductDeatils(productId);

                if (!result.Succeeded)
                {
                    return Conflict(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Result.Fail($"An error occurred in product controller: {ex.Message}"));
            }
        }

        [HttpGet("GetProductsByCtaegory/{category}")]
        public async Task<IActionResult> GetProductOfCategory(string category)
        {
            try
            {
                var result = await iProductService.GetProductOfCategory(category);

                if (!result.Succeeded)
                {
                    return Conflict(result);
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, Result.Fail($"An error occurred in product controller: {ex.Message}"));
            }
        }

        [HttpPost("SearchFilters")]
        public async Task<IActionResult> SearchFilters(ProductListBySearchFilterRequest productListBySearchFilterRequest)
        {
            try
            {
                var result = await iProductService.GetProductByFilters(productListBySearchFilterRequest);

                if (!result.Succeeded)
                {
                    return Conflict(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Result.Fail($"An error occurred in product controller: {ex.Message}"));
            }
        }




        //[HttpPost("AddProductImages")]
        //public async Task<IActionResult> AddProductImages(AddProductImage request)
        //{
        //    await iProductService.AddProductImagesAsync(request);
        //    return Ok();
        //}

        [HttpPost("UploadProductImages")]
        public async Task<IActionResult> UploadFiles([FromForm] UploadProductImages request)
        {
            try
            {

                var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/publish/publicimg";
                if (request.MultipleFiles == null || !request.MultipleFiles.Any())
                    return BadRequest($"No files uploaded.{directory}");

                if (Directory.Exists(directory))
                {
                    const long maxSizeInBytes = 2 * 1024 * 1024; // 2 MB
                    //check extentions
                    foreach (var file in request.MultipleFiles)
                    {
                        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                        // Check for valid image format
                        if (extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".webp")
                        {
                            return BadRequest("Invalid Image Format.");
                        }


                    }
                    List<ProductImage> ImageNames = new List<ProductImage>();
                    var size = new MagickGeometry(1920, 1080)
                    {
                        IgnoreAspectRatio = false
                    };
                    bool first = true;
                    foreach (var file in request.MultipleFiles)
                    {
                        //string nFileName = DateTime.Now.ToUniversalTime + file.FileName;
                        string nFileName = DateTimeOffset.UtcNow.ToUnixTimeSeconds()+ Path.GetFileNameWithoutExtension(file.FileName) + ".webp";

                        //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", nFileName);
                        if (file.Length > 0)
                        {
                            var filePath = Path.Combine(directory, nFileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    // Copy the uploaded file to the MemoryStream
                                    await file.CopyToAsync(memoryStream);
                                    memoryStream.Position = 0; // Reset the stream position

                                    // Load the image from the MemoryStream
                                    using (var image = new MagickImage(memoryStream))
                                    {
                                        // Resize the image
                                        //image.Resize(width, height);
                                        image.Format = MagickFormat.WebP;
                                        //if (file.Length > maxSizeInBytes)
                                        //{
                                        image.Resize(size);
                                        //}

                                        // Write the resized image to the FileStream
                                        image.Write(stream);
                                    }
                                }
                                //var image = new MagickImage(file.ContentDisposition);
                                //image.Write(stream);

                                //await file.CopyToAsync(stream);
                            }
                        }
                        ProductImage productImage = new ProductImage { ImageName = nFileName };
                        if (first)
                        {
                            productImage.coverImage = 1;
                            first = false;
                        }
                       // ProductImage productImage = new ProductImage { ImageName = nFileName };
                        ImageNames.Add(productImage);
                    }

                    AddProductImage addProductRequest = new AddProductImage
                    {
                        ProductID = request.ProductID, // Example ProductID
                        ImageNames = ImageNames

                    };



                    var result = await iProductService.AddProductImagesAsync(addProductRequest);


                    return Ok(new { message = "Files uploaded successfully!" });
                }
                else { return BadRequest("Server Issue"); }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request.{ex.Message}");
            }
        }

        [HttpPost("GetProductImages")]
        public async Task<IActionResult> GetProductImages(GetProductImagesRequest request)
        {
            var images = await iProductService.GetImagesByProductIdAsync(request);
            return Ok(images);
        }

        //public async Task ResizeImageAsync(IFormFile imageFile, string outputPath, int width, int height)
        //{
        //    if (imageFile == null || imageFile.Length == 0)
        //    {
        //        throw new ArgumentException("No file uploaded.");
        //    }

        //    using (var memoryStream = new MemoryStream())
        //    {
        //        // Copy the uploaded file to the memory stream
        //        await imageFile.CopyToAsync(memoryStream);
        //        memoryStream.Position = 0; // Reset the stream position

        //        using (var image = new MagickImage(memoryStream))
        //        {
        //            // Resize to exact dimensions (may distort the image)
        //            image.Resize(width, height);
        //            image.Write(outputPath);
        //        }
        //    }
        //}

        //public FileStream 

        //public void ResizeWithMagick( )
        //{
        //    using var stream = GetStream();
        //    using var image = new MagickImage(stream);

        //    var size = new MagickGeometry(Width, Height)
        //    {
        //        IgnoreAspectRatio = false
        //    };
        //    image.Format = MagickFormat.Png;
        //    image.Resize(size);

        //    // Save the result
        //    image.Write($"images/output_magick.png");
        //    stream.Close();
        //}
        //private static Stream GetOutputStream(string name)
        //{
        //    return File.Open($"images/output_{name}.png", FileMode.OpenOrCreate);
        //}

        //private Stream GetStream()
        //{
        //    return File.OpenRead($"images/{inputFile}");
        //}

        //public  ResizeImage(string inputPath, string outputPath, int maxWidth, int maxHeight)
        //{
        //    using (var image = new MagickImage(inputPath))
        //    {
        //        // Resize while maintaining aspect ratio
        //        image.Resize(maxWidth, maxHeight);
        //        image.Write(outputPath);
        //    }
        //}
    }
}