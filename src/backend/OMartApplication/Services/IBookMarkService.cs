using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartApplication.Services.Wrapper;
using OMartDomain.Models.BookMark.requestAndResponse;

namespace OMartApplication.Services
{
    public interface IBookMarkService
    {
         Task<InsertBookMarkResponse> insertBookmark(InsertBookMarkRequest request);

         Task<UpdateBookMarkResponse> updateBookMark(UpdateBookMarkRequest request);

         Task<GetBookmarkResponse> GetBookmarksbyUserID(string UserID);
         
         Task<UpdateBookMarkResponse> DeleteBookmark(int BookMarkID);
    }
}