using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Fretter.Domain.Interfaces.Service.Api
{
    public interface IAzureApiService
    {
        string UploadFile(string fileBase64, string fileName);
        byte[] DownloadFile(string fileName);
    }
}
