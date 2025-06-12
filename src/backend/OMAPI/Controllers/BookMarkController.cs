using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMartApplication.Services;
using OMartDomain.Models.BookMark.requestAndResponse;

namespace OMAPI.Controllers
{
       
        [ApiController]
        [Route("api/[controller]")]
        public class BookMarkController : ControllerBase
        {
        private readonly IBookMarkService bookMarkService;
        public BookMarkController(IBookMarkService bookMarkService)
        {
            this.bookMarkService = bookMarkService;
        }

//inserBookMark Api
       
        [HttpPost("insertBookmark")]
        public async Task<InsertBookMarkResponse> insertBookmark(InsertBookMarkRequest request)
        {
                return await bookMarkService.insertBookmark(request);
        }

////UpdateBookMark Api 

//        [HttpPut("updateBookMark")]
//        public async Task<UpdateBookMarkResponse> updateBookMark(UpdateBookMarkRequest request)
//        {
//            return await bookMarkService.updateBookMark(request);
//        }

//GetBookmarksbyUserID Api
       [HttpGet("GetBookmarksByUserId/{userId}")]
        public async Task<IActionResult> GetBookmarksByUserIdAsync(string userId)
        {
           
            try
            {
               
                var result = await bookMarkService.GetBookmarksbyUserID(userId);

                if (result == null)
                {
                    return NotFound($"No bookmarks found for user with ID {userId}.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred while fetching bookmarks for user {userId}: {ex.Message}");
            }
        }


//DeleteBookmark Api
        [HttpDelete("deleteBookmark/{BookMarkID}")]
        public async Task<UpdateBookMarkResponse> GetBookmarksByUserIdAsync(int BookMarkID)
         {
                try
                {
                    var result = await bookMarkService.DeleteBookmark(BookMarkID);
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error occurred while fetching bookmarks for user {BookMarkID}: {ex.Message}", ex);
                }
         }
 

    }
}