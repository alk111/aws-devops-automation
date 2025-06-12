using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartApplication.Services.Wrapper;
using OMartDomain.Models.BookMark.requestAndResponse;

namespace OMartApplication.Repositories
{
    public interface IBookMarkRepository
    {
        Task<InsertBookMarkResponse> insertBookmark(InsertBookMarkRequest request);

        Task<UpdateBookMarkResponse> updateBookmark(UpdateBookMarkRequest request );

        Task<GetBookmarkResponse> GetBookmarksbyUserID(string UserID);

        Task<UpdateBookMarkResponse> DeleteBookmark(int BookMarkID);
    }
}