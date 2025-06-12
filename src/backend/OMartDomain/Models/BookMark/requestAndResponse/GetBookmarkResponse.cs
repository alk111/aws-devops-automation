using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.BookMark.requestAndResponse
{
    public class GetBookmarkResponse
    {
        public string Message{ get; set; }
        public List<BookMark> bookmarks{ get; set; }
    }
}