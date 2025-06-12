using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartDomain.Models.App
{
    public class ApiResponse
    {
        public List<string> Messages { get; set; }
        public bool Success { get; set; }
    }

    public class ApiResponse<T> : ApiResponse where T : class
    {
        public T Data { get; set; } = null;
    }
}
