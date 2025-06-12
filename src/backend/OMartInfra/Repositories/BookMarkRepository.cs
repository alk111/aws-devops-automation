using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OMartApplication.Repositories;
using OMartApplication.Services.Wrapper;
using OMartDomain.Common;
using OMartDomain.Models.BookMark;
using OMartDomain.Models.BookMark.requestAndResponse;

namespace OMartInfra.Repositories
{
    public class BookMarkRepository : CentralRepository, IBookMarkRepository
    {
        private readonly string? _connectionString;

        public BookMarkRepository(IConfiguration configuration) : base(configuration, "OMartDevDbConnection")
        {
            _connectionString = configuration.GetConnectionString(name: "OMartDevDbConnection");

        }
        //InserBookMark Api
        public async Task<InsertBookMarkResponse> insertBookmark(InsertBookMarkRequest request)
        {

            try
            {
                var parameters = new
                {
                    p_UserID = request.UserID,
                    p_Type = request.Type,
                    p_ID = request.ID
                };

                int result = await ExecuteQueryAsync<int>(SPConstant.InsertBookmark, parameters);
                if (!(result > 0))
                {
                    throw new Exception("The stored procedure returned no result, indicating that the order might not have been added successfully.");
                }

                return new InsertBookMarkResponse { Message = "Done" };
            }
            catch (Exception ex)
            {
                throw new Exception($"BookMark is not added successfully...{ex.Message}");
            }
        }

        //updateBookMark Api
        public async Task<UpdateBookMarkResponse> updateBookmark(UpdateBookMarkRequest request)
        {
            try
            {
                var parameters = new
                {
                    p_BookMarkID = request.BookMarkID,
                    p_Type = request.Type,
                    p_ID = request.ID
                };

                int result = await ExecuteQueryAsync<int>(SPConstant.UpdateBookmark, parameters);
                return new UpdateBookMarkResponse { Message = "Done" };

            }
            catch (Exception ex)
            {
                throw new Exception($"Exception while Connecting Database.... {ex.Message}");
            }
        }

        //GetBookmarksbyUserID Api    
        public async Task<GetBookmarkResponse> GetBookmarksbyUserID(string UserID)
        {
            try
            {
                var parameters = new
                {
                    p_user_id = UserID
                };

                var result = await ExecuteQueryListAsync<BookMark>(SPConstant.GetBookmarksbyUserID, parameters);

                if (result == null)
                {

                    return new GetBookmarkResponse { Message = "No bookmarks found for the user.", bookmarks = null };
                }

                return new GetBookmarkResponse { Message = "Data Get Successfully...", bookmarks = result };
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception while connecting to the database: {ex.Message}", ex);
            }
        }

        //DeleteBookmark Api
        public async Task<UpdateBookMarkResponse> DeleteBookmark(int BookMarkID)
        {
            try
            {
                var parameters = new
                {
                    p_BookMarkID = BookMarkID
                };

                int result = await ExecuteQueryAsync<int>(SPConstant.DeleteBookmark, parameters);

                if (result > 0)
                {
                    return new UpdateBookMarkResponse { Message = "No bookmarks found for the user." };
                }

                return new UpdateBookMarkResponse { Message = "deleted SuccessFully..." };
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception while connecting to the database: {ex.Message}", ex);
            }
        }

    }
}