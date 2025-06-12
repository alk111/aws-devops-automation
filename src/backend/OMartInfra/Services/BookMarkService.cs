using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartApplication.Repositories;
using OMartApplication.Services;
using OMartApplication.Services.Wrapper;
using OMartDomain.Models.BookMark.requestAndResponse;

namespace OMartInfra.Services
{
    public class BookMarkService : IBookMarkService
    {
        private readonly IBookMarkRepository bookMarkRepository; 
        public BookMarkService(IBookMarkRepository bookMarkRepository)
        {
            this.bookMarkRepository = bookMarkRepository;
        }

//InsertBookMark Api
        public async Task<InsertBookMarkResponse> insertBookmark(InsertBookMarkRequest Request)
        {
            var insertBookmark= await bookMarkRepository.insertBookmark(Request);
            return insertBookmark;
        }

//UpdateBookMark Api
       public async Task<UpdateBookMarkResponse> updateBookMark(UpdateBookMarkRequest request)
        {
            try
            {
                var updateBookMark = await bookMarkRepository.updateBookmark(request);
                return updateBookMark;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                throw new Exception($"An error occurred while updating the bookmark: {ex.Message}", ex);
            }
        }

//GetBookmarksbyUserID
         public async Task<GetBookmarkResponse> GetBookmarksbyUserID(string UserID)
           {
             var GetBookmarksbyUserID= await bookMarkRepository.GetBookmarksbyUserID(UserID); 
             return GetBookmarksbyUserID;   
           }

//DeleteBookmark Api
           public async Task<UpdateBookMarkResponse> DeleteBookmark(int BookMarkID)
           {
               var DeleteBookmark= await bookMarkRepository.DeleteBookmark(BookMarkID); 
               return DeleteBookmark;   
           }             

    }
}