using OMartDomain.Models.Cart.RequestAndresponse;
using OMartDomain.Models.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartApplication.Repositories
{
    public interface IFileRepository
    {
        Task AddFile(List<FileDetailsAddRequest> fileDetailsAddRequests);

    }
}
