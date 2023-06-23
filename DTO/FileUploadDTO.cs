using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class FileUploadDTO
    {
        public string? filename { get; set; }
        public string? msg   { get; set; }
        public bool success { get; set; }
    }
}
