using OMartApplication.Repositories;
using OMartDomain.Models.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartInfra.Repositories
{
    public class FileRepository : IFileRepository
    {
        

        public Task AddFile(List<FileDetailsAddRequest> fileDetailsAddRequests)
        {
            throw new NotImplementedException();
        }
    }
}
