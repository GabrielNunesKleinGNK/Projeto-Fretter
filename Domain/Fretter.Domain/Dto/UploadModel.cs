
using Fretter.Domain.Enum;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Fretter.Api.Models
{
    public class UploadModel
    {
        public List<IFormFile> files { get; set; }
        public string url { get; set; }
        public int id { get; set; }
        public string fileName { get; set; }
    }
}
