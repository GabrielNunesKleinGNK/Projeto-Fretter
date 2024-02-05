using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Fretter.Domain.Interfaces.Service.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Fretter.Domain.Services.Api
{
    public class AzureApiService : IAzureApiService
    {
        const string connectionString = "DefaultEndpointsProtocol=https;AccountName=bvdocumentosstorage;AccountKey=UVvGpd892HQ97ehBKIIC/2fLDkYWJsw5Dbt3hSeAKfTOWXd2ierFQWXxBbNT8Bu+ouBcXZZJ1UUopmVn65clrA==;EndpointSuffix=core.windows.net";

        public string UploadFile(string fileBase64, string fileName)
        {
            BlobContainerClient container = new BlobContainerClient(connectionString, "clientes-documentos");

            container.CreateIfNotExists(PublicAccessType.Blob);

            var extension = Path.GetExtension(fileName);

            var blockBlob = container.GetBlobClient(fileName);

            byte[] bytes = Helpers.FileHelper.ConvertFromBase64String(fileBase64);

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                blockBlob.Upload(ms);
            }

            return blockBlob.Uri.AbsoluteUri;
        }

        public byte[] DownloadFile(string fileName)
        {
            BlobContainerClient container = new BlobContainerClient(connectionString, "clientes-documentos");

            container.CreateIfNotExists(PublicAccessType.Blob);

            var blockBlob = container.GetBlobClient(fileName);

            Stream ms = new MemoryStream();

            blockBlob.DownloadTo(ms);

            return Helpers.FileHelper.StreamToBytes(ms);
        }
    }
}
