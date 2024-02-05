using System.IO;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IBlobStorageService
    {
        void InitBlob(string blobConnectionString, string containerName);
        string UploadFileToBlobSync(string directoryName, string strFileName, byte[] fileData, string fileMimeType);
        
        string UploadFileToBlobWithContainerSync(string containerName, string directoryName, string strFileName, byte[] fileData, string fileMimeType);
        void DeleteBlobDataSync(string directory, string fileUrl);
        Task<string> UploadFileToBlobAsync(string directoryName, string strFileName, byte[] fileData, string fileMimeType);
        Task<string> UploadFileToBlobAsync(string directoryName, string folderName, string strFileName, byte[] fileData, string fileMimeType);
        Task<Stream> GetFile(string urlFile);
    }
}
