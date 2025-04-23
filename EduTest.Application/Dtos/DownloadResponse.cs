using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.Dtos
{
    public class DownloadResponse
    {
        public byte[] FileData { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}
