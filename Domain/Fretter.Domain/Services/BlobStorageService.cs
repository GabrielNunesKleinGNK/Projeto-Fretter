using Fretter.Domain.Interfaces.Service;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Fretter.Domain.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private CloudBlobClient _cloudBlobClient;
        private CloudBlobContainer _cloudBlobContainer;
        public BlobStorageService()
        {
        }
        public void InitBlob(string blobConnectionString, string containerName)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobConnectionString);
            _cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            //Blob é case sensitive https://docs.microsoft.com/en-us/rest/api/storageservices/naming-and-referencing-containers--blobs--and-metadata
            //Container tem que ser sempre em letras minúsculas, de 3 a 63 caracteres
            _cloudBlobContainer = _cloudBlobClient.GetContainerReference(containerName.ToLower().RemoveSpecialCharacters());
        }

        public async void DeleteBlobDataSync(string directory, string fileUrl)
        {
            Uri uriObj = new Uri(fileUrl);
            string BlobName = Path.GetFileName(uriObj.LocalPath);

            CloudBlobDirectory blobDirectory = _cloudBlobContainer.GetDirectoryReference(directory);
            // get block blob refarence    
            CloudBlockBlob blockBlob = blobDirectory.GetBlockBlobReference(BlobName);
            // delete blob from container        
            await blockBlob.DeleteAsync();
        }

        public string UploadFileToBlobSync(string directoryName, string strFileName, byte[] fileData, string fileMimeType)
        {
            try
            {
                string fullFileName = $"{directoryName.Trim().RemoveSpecialCharacters()}/{strFileName.Trim().RemoveSpecialCharacters()}";

                if (_cloudBlobContainer.CreateIfNotExists())
                    _cloudBlobContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                if (fullFileName != null && fileData != null)
                {
                    CloudBlockBlob cloudBlockBlob = _cloudBlobContainer.GetBlockBlobReference(fullFileName);
                    cloudBlockBlob.Properties.ContentType = fileMimeType;
                    cloudBlockBlob.UploadFromByteArray(fileData, 0, fileData.Length);
                    return cloudBlockBlob.Uri.AbsoluteUri;
                }
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public string UploadFileToBlobWithContainerSync(string containerName, string directoryName, string strFileName, byte[] fileData, string fileMimeType)
        {
            try
            {
                string fullFileName = $"{directoryName.Trim().RemoveSpecialCharacters()}/{strFileName.Trim().RemoveSpecialCharacters()}";

                _cloudBlobContainer = _cloudBlobClient.GetContainerReference(containerName.ToLower().RemoveSpecialCharacters());

                if (_cloudBlobContainer.CreateIfNotExists())
                    _cloudBlobContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                if (fullFileName != null && fileData != null)
                {
                    CloudBlockBlob cloudBlockBlob = _cloudBlobContainer.GetBlockBlobReference(fullFileName);
                    cloudBlockBlob.Properties.ContentType = fileMimeType;
                    cloudBlockBlob.UploadFromByteArray(fileData, 0, fileData.Length);
                    return cloudBlockBlob.Uri.AbsoluteUri;
                }
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<string> UploadFileToBlobAsync(string directoryName, string strFileName, byte[] fileData, string fileMimeType)
        {
            try
            {
                string fullFileName = $"{directoryName.Trim().RemoveSpecialCharacters()}/{strFileName.Trim().RemoveSpecialCharacters()}";

                if (await _cloudBlobContainer.CreateIfNotExistsAsync())
                    await _cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                if (fullFileName != null && fileData != null)
                {
                    CloudBlockBlob cloudBlockBlob = _cloudBlobContainer.GetBlockBlobReference(fullFileName);
                    cloudBlockBlob.Properties.ContentType = fileMimeType;
                    await cloudBlockBlob.UploadFromByteArrayAsync(fileData, 0, fileData.Length);
                    return cloudBlockBlob.Uri.AbsoluteUri;
                }
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<string> UploadFileToBlobAsync(string directoryName, string folderName ,string strFileName, byte[] fileData, string fileMimeType)
        {
            try
            {
                string fullFileName = $"{directoryName.Trim()}/{folderName}/{strFileName.Trim().RemoveSpecialCharacters()}";

                if (await _cloudBlobContainer.CreateIfNotExistsAsync())
                    await _cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                if (fullFileName != null && fileData != null)
                {
                    CloudBlockBlob cloudBlockBlob = _cloudBlobContainer.GetBlockBlobReference(fullFileName);
                    cloudBlockBlob.Properties.ContentType = fileMimeType;
                    await cloudBlockBlob.UploadFromByteArrayAsync(fileData, 0, fileData.Length);
                    return cloudBlockBlob.Uri.AbsoluteUri;
                }
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<Stream> GetFile(string urlFile)
        {
            var file = _cloudBlobClient.GetBlobReferenceFromServer(new StorageUri(new Uri(urlFile)));
            return await file.OpenReadAsync();
        }

    }
}
