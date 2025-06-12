using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OMartDomain.Models.File
{
    public class FileDetails
    {
        [Key]
        public int FileID { get; set; }
        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public string Link { get; set; }
        public string ProductID { get; set; }


    }
    public enum FileType
    {
        Product = 0,
        Shop = 1,
        Review = 2
    }

    public class FileDetailsAddRequest
    {
        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public string Link { get; set; }
        public string ProductID { get; set; }


    }
    public class MultipleFilesModel
    {
        public IFormFileCollection Files { get; set; }
    }
}
