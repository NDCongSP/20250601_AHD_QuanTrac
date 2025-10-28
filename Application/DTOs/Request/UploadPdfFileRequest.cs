using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Request
{
    public class UploadPdfFileRequest
    {
        public string PathFile { get; set; }
        public string FileName { get; set; }
        public string Base64 { get; set; }
    }
}
