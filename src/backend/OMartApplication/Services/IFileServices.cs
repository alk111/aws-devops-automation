using OMartDomain.Models.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartApplication.Services
{
    public interface IFileServices
    {
        Task AddFile(List<FileDetailsAddRequest> fileDetailsAddRequest);
    } 
}
