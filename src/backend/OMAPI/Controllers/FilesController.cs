using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMartApplication.Services;
using OMartDomain.Models.File;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace OMAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileServices fileServices;

        public FilesController(IFileServices fileServices)
        {
            this.fileServices = fileServices;
        }



        /// <summary>
        ///// Single File Upload
        ///// </summary>
        ///// <param name="file"></param>
        ///// <returns></returns>
        //[HttpPost("AddProductImg")]
        //public async Task<ActionResult> AddFile([FromForm] FileDetailsAddRequest fileDetails)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //if (SingleFile != null && SingleFile.Length > 0)
        //        //{
        //        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", SingleFile.FileName);

        //        //    //Using Streaming
        //        //    using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        //        //    {
        //        //        await SingleFile.CopyToAsync(stream);
        //        //    }
        //        //    // Process the file here (e.g., save to the database, storage, etc.)
        //        //    return View("UploadSuccess");
        //        //}
        //    }
        //    if (fileDetails == null)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        await fileServices.AddFile(fileDetails);
        //        return Ok();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        //[HttpPost("upload")]
        //public async Task<IActionResult> MultiUploadAddFiles([FromForm] MultipleFilesModel model, string ID, FileType Type)
        //{
        //    if (model.Files == null || model.Files.Count == 0)
        //    {
        //        return BadRequest("No files selected.");
        //    }

        //    //var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

        //    // Create directory if it doesn't exist
        //    //if (!Directory.Exists(uploadPath))
        //    //{
        //    //    Directory.CreateDirectory(uploadPath);
        //    //}

        //    List<FileDetailsAddRequest> fileListDetails = [];

        //    foreach (var file in model.Files)
        //    {
        //        FileDetailsAddRequest fileD = new FileDetailsAddRequest();
        //        fileD.FileName = file.FileName;
        //        //if(file.ContentType )
        //        //fileD.FileType = 0;
        //        fileD.ProductID = ID;
        //        fileD.FileType = Type;

        //        var filePath = Path.Combine(_uploadPath, file.FileName);

        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await file.CopyToAsync(stream);
        //        }
        //        fileListDetails.Add(fileD);
        //    }

        //    await fileServices.AddFile(fileListDetails);

        //    return Ok(new { message = "Files uploaded successfully." });
        //}


        //private readonly string _uploadPath = "/app/publish/publicimg";

        //[HttpPost("upload2")]
        //public async Task<IActionResult> UploadFile(IFormFile file)
        //{
        //    var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/publicimg";
        //    Console.WriteLine(directory);

        //    if (file == null || file.Length == 0)
        //        return BadRequest("No file uploaded.");

        //    var filePath = Path.Combine(directory, file.FileName);

        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }

        //    return Ok(new { filePath });
        //}

        //[HttpPost("uploadProductImages")]
        //public async Task<IActionResult> UploadFiles([FromForm] IEnumerable<IFormFile> MultipleFiles)
        //{
        //    try
        //    {

        //        var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/publish/publicimg";
        //        if (MultipleFiles == null || !MultipleFiles.Any())
        //            return BadRequest($"No files uploaded.{directory}");

        //        if (Directory.Exists(directory))
        //        {
        //            //check extentions
        //            foreach (var file in MultipleFiles)
        //            {
        //                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        //                // Check for valid image format
        //                if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
        //                {
        //                    return BadRequest("Invalid Image Format.");
        //                }
        //                const long maxSizeInBytes = 2 * 1024 * 1024; // 2 MB

        //                if (file.Length > maxSizeInBytes)
        //                {
        //                    return BadRequest("File size exceeds 2 MB.");
        //                }
        //            }
        //            foreach (var file in MultipleFiles)
        //            {
        //                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        //                if (file.Length > 0)
        //                {
        //                    var filePath = Path.Combine(directory, file.FileName);
        //                    using (var stream = new FileStream(filePath, FileMode.Create))
        //                    {
        //                        await file.CopyToAsync(stream);
        //                    }
        //                }
        //            }



        //            return Ok(new { message = "Files uploaded successfully!" });
        //        }
        //        else { return BadRequest("Server Issue"); }

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred while processing your request.{ex.Message}");
        //    }
        //}

        // GET: api/fileupload/download/{filename}
        [HttpGet("download/{filename}")]
        public IActionResult DownloadFile(string filename)
        {
            try
            {
                var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/publish/publicimg";
                var filePath = Path.Combine(directory, filename);

                if (!System.IO.File.Exists(filePath))
                    return NotFound("File not found.");

                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "application/octet-stream", filename);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request.{ex.Message}");
            }
        }

    }
}
