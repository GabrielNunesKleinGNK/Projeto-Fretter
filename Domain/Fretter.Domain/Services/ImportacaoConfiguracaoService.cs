using Fretter.Api.Helpers.Atributes;
using Fretter.Domain.Config;
using Fretter.Domain.Entities;
using Fretter.Domain.Enum;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Helper;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Microsoft.Extensions.Options;
using S22.Imap;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Fretter.Domain.Services
{
    public class ImportacaoConfiguracaoService<TContext> : ServiceBase<TContext, ImportacaoConfiguracao>, IImportacaoConfiguracaoService<TContext>
    where TContext : IUnitOfWork<TContext>
    {
        private readonly IImportacaoConfiguracaoRepository<TContext> _repository;
        private readonly IImportacaoArquivoService<TContext> _arquivoService;
        private readonly BlobStorageConfig _blobConfig;
        public readonly IBlobStorageService _blobStorageService;
        private string errorMessage = string.Empty;
        private readonly ILogHelper _logHelper;

        public ImportacaoConfiguracaoService(IImportacaoConfiguracaoRepository<TContext> repository,
            IImportacaoArquivoService<TContext> arquivoService,
            IUnitOfWork<TContext> unitOfWork,
            IBlobStorageService blobStorageService,
            IOptions<BlobStorageConfig> blobConfig,
            ILogHelper logHelper,
            IUsuarioHelper user) : base(repository, unitOfWork, user)
        {
            _blobConfig = blobConfig.Value;
            _blobStorageService = blobStorageService;
            _blobStorageService.InitBlob(_blobConfig.ConnectionString, _blobConfig.ConciliacaoContainerName);
            _repository = repository;
            _arquivoService = arquivoService;
            _logHelper = logHelper;
        }

        public async Task<int> ProcessarImportacaoConfiguracao()
        {
            int qtdArquivos = 0;
            var configuracoes = _repository.GetAll(t => t.Ativo);

            foreach (var configuracao in configuracoes)
            {
                switch ((Enum.EnumImportacaoConfiguracaoTipo)configuracao.ImportacaoConfiguracaoTipoId)
                {
                    case Enum.EnumImportacaoConfiguracaoTipo.FTP:
                        qtdArquivos = await ProcessaImportacaoFTP(configuracao);
                        break;
                    case Enum.EnumImportacaoConfiguracaoTipo.Api:
                        qtdArquivos = await ProcessaImportacaoApi(configuracao);
                        break;
                    case Enum.EnumImportacaoConfiguracaoTipo.Email:
                        qtdArquivos = await ProcessaImportacaoEmail(configuracao);
                        break;
                    default:
                        break;
                }

                string mensagemRetorno = string.IsNullOrEmpty(errorMessage) ? $"SUCESSO: Processamento de {qtdArquivos} Arquivos" : (errorMessage.Length > 510 ? errorMessage.Substring(0, 510) : errorMessage);
                configuracao.AtualizarUltimoRetorno(mensagemRetorno);
                configuracao.AtualizarUltimaExecucao(DateTime.Now);
                configuracao.AtualizarSucesso(string.IsNullOrEmpty(errorMessage));
                _repository.Update(configuracao);
            }

            return qtdArquivos;
        }

        public override IEnumerable<ImportacaoConfiguracao> GetAll(Expression<Func<ImportacaoConfiguracao, bool>> predicate = null)
        {
            predicate = x => (x.EmpresaId == _user.UsuarioLogado.EmpresaId);
            return base.GetAll(predicate);
        }
        public override ImportacaoConfiguracao Save(ImportacaoConfiguracao entidade)
        {
            entidade.AtualizarEmpresa(_user.UsuarioLogado.EmpresaId);
            return base.Save(entidade);
        }

        public override ImportacaoConfiguracao Update(ImportacaoConfiguracao entidade)
        {
            entidade.AtualizarEmpresa(_user.UsuarioLogado.EmpresaId);
            return base.Update(entidade);
        }

        #region Métodos Privados
        private async Task<int> ProcessaImportacaoEmail(ImportacaoConfiguracao configuracao)
        {
            int qtdArquivos = 0;
            string mailBox = "INBOX";

            using (ImapClient client = ObterConexaoImap(configuracao, mailBox))
            {
                string mailBoxProcessado = $"INBOX.CteProcessado";
                string mailBoxErro = $"INBOX.CteErro";

                CriarPastasEmail(client, new List<string>() { mailBoxProcessado, mailBoxErro });
                var units = client.Search(SearchCondition.Undeleted());

                foreach (var emailUnit in units)
                {
                    var message = client.GetMessage(emailUnit);
                    foreach (var attachment in message.Attachments)
                    {
                        var nomeArquivo = attachment.Name;
                        var extensao = Path.GetExtension(nomeArquivo);
                        using (var stream = new MemoryStream())
                        {
                            try
                            {
                                attachment.ContentStream.CopyTo(stream);
                                var bytes = stream.ToArray();
                                await _arquivoService.ImportarArquivo(bytes, nomeArquivo, (int)configuracao.EmpresaId, configuracao.Id);
                                client.CopyMessage(emailUnit, mailBoxProcessado);
                            }
                            catch (Exception ex)
                            {
                                client.CopyMessage(emailUnit, mailBoxErro);
                            }
                            finally
                            {
                                client.DeleteMessage(emailUnit);
                            }
                        }
                    }
                }
            }
            return qtdArquivos;
        }
        private async Task<int> ProcessaImportacaoApi(ImportacaoConfiguracao configuracao)
        {
            throw new NotImplementedException();
        }
        private async Task<int> ProcessaImportacaoFTP(ImportacaoConfiguracao configuracao)
        {
            int qtdDiretorio = 0;

            if (configuracao != null)
            {
                var diretorioFtp = configuracao.Diretorio.EndsWith("/") ? configuracao.Diretorio : $"{configuracao.Diretorio}/";
                var credenciaisFtp = new NetworkCredential(configuracao.Usuario.Normalize(), configuracao.Senha.Normalize());
                var arquivosFtp = new string[] { };

                try
                {
                    if (configuracao.Compactado == true)
                    {
                        var arquivosZips = UnzipFiles(diretorioFtp, credenciaisFtp, configuracao.DiretorioSucesso, configuracao.DiretorioErro);

                        foreach (var arquivo in arquivosZips)
                        {
                            byte[] bytesArquivo = System.IO.File.ReadAllBytes(arquivo);

                            if (bytesArquivo != null)
                            {
                                await _arquivoService.ImportarArquivo(bytesArquivo, Path.GetFileName(arquivo), (int)configuracao.EmpresaId, configuracao.Id);
								_unitOfWork.Commit();

								if (File.Exists(arquivo))
                                    File.Delete(arquivo);
                            }
                        }

                        qtdDiretorio = arquivosZips.Count();
                    }
                    else
                    {
                        switch ((Enum.EnumImportacaoArquivoTipo)configuracao.ImportacaoArquivoTipoId)
                        {
                            case Enum.EnumImportacaoArquivoTipo.CTe:
                                arquivosFtp = GetFiles(diretorioFtp, credenciaisFtp, ".xml");
                                break;
                            case Enum.EnumImportacaoArquivoTipo.CONEMB:
                                arquivosFtp = GetFiles(diretorioFtp, credenciaisFtp, ".txt");
                                break;
                            default:
                                break;
                        }
                        foreach (var arquivo in arquivosFtp)
                        {
                            var bytesArquivo = GetBytesFromFTPFile(diretorioFtp, arquivo, credenciaisFtp);
                            if (bytesArquivo != null)
                            {
                                if (!string.IsNullOrEmpty(configuracao.DiretorioSucesso))
                                {
                                    string newFileName = $"{Guid.NewGuid().ToString().Replace("-", "")}_{arquivo}";
                                    bool fileMoved = MoveFile(sourcePath: diretorioFtp, fileName: arquivo, credentials: credenciaisFtp, targetFolder: configuracao.DiretorioSucesso, newFileName: newFileName);

                                    if (!fileMoved)
                                        Upload(fileName: arquivo, fileData: bytesArquivo, targetPath: Path.Combine(diretorioFtp, configuracao.DiretorioSucesso), credentials: credenciaisFtp);

                                    if (File.Exists(Path.Combine(diretorioFtp, arquivo)))
                                        Delete(diretorioFtp, arquivo, credenciaisFtp);
                                }
                                await _arquivoService.ImportarArquivo(bytesArquivo, arquivo, (int)configuracao.EmpresaId, configuracao.Id);
                                _unitOfWork.Commit();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logHelper.LogError("ImportacaoConfiguracaoTask", "ProcessaImportacaoFTP", null, DateTime.Now, exception: ex);
                    Console.WriteLine($"Task - ImportacaoConfiguracaoTask - Error: {configuracao.Diretorio}. Error: Message->{ex.Message}, InnerMessage->{ex.InnerException}");
                    errorMessage = $"Importacao Configuracao Error: {configuracao.Diretorio}. Error: Message->{ex.Message}, InnerMessage->{ex.InnerException}";
                }
            }

            return qtdDiretorio;
        }
        private string[] UnzipFiles(string diretorioFtp, NetworkCredential credenciaisFtp, string diretorioSucesso, string diretorioErro)
        {
            var arquivosXml = new string[] { };
            var arquivosFtpZip = new string[] { };
            var arquivosFtpRar = new string[] { };

            Console.WriteLine($"Task - ImportacaoConfiguracaoTask - INICIO Busca Arquivos .Zip {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
            arquivosFtpZip = GetFiles(diretorioFtp, credenciaisFtp, ".zip");
            arquivosFtpRar = GetFiles(diretorioFtp, credenciaisFtp, ".rar");

            int arquivosFtpZipLength = arquivosFtpZip.Length;
            Array.Resize<string>(ref arquivosFtpZip, arquivosFtpZipLength + arquivosFtpRar.Length);
            Array.Copy(arquivosFtpRar, 0, arquivosFtpZip, arquivosFtpZipLength, arquivosFtpRar.Length);

            Console.WriteLine($"Task - ImportacaoConfiguracaoTask - TERMINO Busca Arquivos .Zip {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} Qtd:{arquivosFtpZip.Length}");
            var diretorioTemp = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? @"./Temp\Importacao\" : @"./Temp/Importacao/";

            ClearTempDirectory(diretorioTemp);

            foreach (var arquivoZip in arquivosFtpZip)
            {
                string fullPathZip = Path.Combine(diretorioTemp, arquivoZip);
                var bytesArquivo = GetBytesFromFTPFile(diretorioFtp, arquivoZip, credenciaisFtp);

                try
                {
                    if (bytesArquivo != null)
                    {
                        SaveFile(bytesArquivo, fullPathZip);

                        var fileExtension = Path.GetExtension(fullPathZip);

                        switch (fileExtension)
                        {
                            case ".zip":
                                ZipFile.ExtractToDirectory(fullPathZip, diretorioTemp);
                                break;
                            case ".rar":
                            case ".7z":
                                using (var archive = RarArchive.Open(fullPathZip))
                                {
                                    foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                                    {
                                        entry.WriteToDirectory(diretorioTemp, new ExtractionOptions() { });
                                    }
                                }
                                break;
                            default:
                                break;
                        }

                        if (File.Exists(fullPathZip))
                        {

                            if (!string.IsNullOrEmpty(diretorioSucesso))
                            {
                                string newFileName = $"{Guid.NewGuid().ToString().Replace("-", "")}_{arquivoZip}";
                                bool fileMoved = MoveFile(sourcePath: diretorioFtp, fileName: arquivoZip, credentials: credenciaisFtp, targetFolder: diretorioSucesso, newFileName: newFileName);

                                if (!fileMoved)
                                {
                                    Upload(fileName: arquivoZip, fileData: bytesArquivo, Path.Combine(diretorioFtp, diretorioSucesso), credenciaisFtp);

                                    if (File.Exists(Path.Combine(diretorioFtp, arquivoZip)))
                                        File.Delete(Path.Combine(diretorioFtp, arquivoZip));
                                }
                            }

                            //if (!string.IsNullOrEmpty(diretorioSucesso))
                            //    Upload(fileName: arquivoZip, fileData: bytesArquivo, Path.Combine(diretorioFtp, diretorioSucesso), credenciaisFtp);                            

                            File.Delete(fullPathZip);
                        }
                    }
                }
                catch (Exception ex)
                {

                    _logHelper.LogError("ImportacaoConfiguracaoTask", "UnzipFiles", null, DateTime.Now, exception: ex);
                    Console.WriteLine($"Task - ImportacaoConfiguracaoTask - Extract File Error: {fullPathZip}. Error: Message->{ex.Message}, InnerMessage->{ex.InnerException}");
                    errorMessage += $"Extract File Error: {fullPathZip}. Error: Message->{ex.Message}, InnerMessage->{ex.InnerException}";

                    if (File.Exists(fullPathZip))
                    {
                        if (!string.IsNullOrEmpty(diretorioErro))
                        {
                            string newFileName = $"{Guid.NewGuid().ToString().Replace("-", "")}_{arquivoZip}";
                            bool fileMoved = MoveFile(sourcePath: diretorioFtp, fileName: arquivoZip, credentials: credenciaisFtp, targetFolder: diretorioErro, newFileName: newFileName);

                            if (!fileMoved)
                            {
                                Upload(fileName: arquivoZip, fileData: bytesArquivo, Path.Combine(diretorioFtp, diretorioErro), credenciaisFtp);

                                if (File.Exists(Path.Combine(diretorioFtp, arquivoZip)))
                                    File.Delete(Path.Combine(diretorioFtp, arquivoZip));
                            }
                        }

                        //if (!string.IsNullOrEmpty(diretorioErro))
                        //    Upload(fileName: arquivoZip, fileData: bytesArquivo, Path.Combine(diretorioFtp, diretorioErro), credenciaisFtp);

                        File.Delete(fullPathZip);
                    }
                }
            }

            arquivosXml = GetFilesByExtension(diretorioTemp, ".xml");
            return arquivosXml;
        }
        private void ClearTempDirectory(string tempDirectory)
        {
            string[] filePaths = Directory.GetFiles(tempDirectory);
            foreach (string filePath in filePaths)
            {
                var name = new FileInfo(filePath).Name;
                name = name.ToLower();
                if (name != "blank.temp")
                {
                    File.Delete(filePath);
                }
            }
        }
        private string[] GetFilesByExtension(string tempDirectory, string fileExtension)
        {
            List<string> files = new List<string>();
            string[] fullFiles = Directory.GetFiles(tempDirectory);

            foreach (string fileTemp in fullFiles)
            {
                var name = new FileInfo(fileTemp).Name;

                if (fileTemp.Contains(fileExtension))
                    files.Add(fileTemp);
            }

            return files.ToArray();
        }
        private void SaveFile(Byte[] fileBytes, string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(fileBytes, 0, fileBytes.Length);
            fileStream.Close();
        }
        private byte[] GetBytesFromFTPFile(string sourcePath, string fileName, NetworkCredential sourceCredentials)
        {
            byte[] bytesArquivo = null;

            try
            {
                var request = (FtpWebRequest)WebRequest.Create(Path.Combine(sourcePath, fileName));
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = sourceCredentials;

                using (var response = (FtpWebResponse)request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        bytesArquivo = ToByteArray(stream);
                    }
                }
            }
            catch (Exception e)
            {
                _logHelper.LogWarn("GetBytesFromFTPFile", $"Erro ao buscar aquivo CTe no ftp: {sourcePath}", exception: e);
            }

            return bytesArquivo;
        }
        private bool Delete(string path, string fileName, NetworkCredential credentials)
        {
            bool isDeleted = false;
            try
            {
                var request = (FtpWebRequest)WebRequest.Create(Path.Combine(path, fileName));
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = credentials;

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == FtpStatusCode.FileActionOK)
                        isDeleted = true;
                    else isDeleted = false;
                }
            }
            catch (Exception ex)
            {
                _logHelper.LogError("ImportacaoConfiguracaoTask", "Delete", null, DateTime.Now, exception: ex);
                isDeleted = false;
                errorMessage += $"Delete Error: {path}. Error: Message->{ex.Message}, InnerMessage->{ex.InnerException}";
            }

            return isDeleted;
        }
        private bool CopyFile(string sourcePath, string fileName, NetworkCredential sourceCredentials, string targetPath, NetworkCredential targetCredentials)
        {
            bool isCopied = false;
            try
            {
                var request = (FtpWebRequest)WebRequest.Create(Path.Combine(sourcePath, fileName));
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = sourceCredentials;

                using (var response = (FtpWebResponse)request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        isCopied = Upload(fileName, ToByteArray(stream), targetPath, targetCredentials);
                    }
                }
            }
            catch (Exception ex)
            {
                isCopied = false;
                errorMessage += $"Move File Error: {sourcePath}. Error: Message->{ex.Message}, InnerMessage->{ex.InnerException}";
            }

            return isCopied;
        }
        private bool MoveFile(string sourcePath, string fileName, NetworkCredential credentials, string targetFolder, string newFileName)
        {
            bool isMovedFile = false;
            try
            {
                var request = (FtpWebRequest)WebRequest.Create(Path.Combine(sourcePath, fileName));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.UsePassive = false;
                request.Credentials = credentials;
                var newFilename = $"{targetFolder}/{newFileName}";
                request.Method = WebRequestMethods.Ftp.Rename;
                request.RenameTo = newFilename;
                request.GetResponse().Close();
                isMovedFile = true;
            }
            catch (Exception ex)
            {
                _logHelper.LogError("ImportacaoConfiguracaoTask", "MoveFile", null, DateTime.Now, exception: ex);
                isMovedFile = false;
            }

            return isMovedFile;
        }
        private bool Upload(string fileName, byte[] fileData, string targetPath, NetworkCredential credentials)
        {
            bool isUploaded = false;
            try
            {
                var request = (FtpWebRequest)WebRequest.Create(Path.Combine(targetPath, fileName));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = credentials;
                request.UseBinary = true;
                request.ContentLength = fileData.Length;

                int buffLength = 2048;
                int offset = 0;

                using (var stream = request.GetRequestStream())
                {
                    int total_bytes = (int)fileData.Length;

                    while (total_bytes > 0)
                    {
                        int len = Math.Min(buffLength, total_bytes);
                        stream.Write(fileData, offset, len);
                        total_bytes -= len;
                        offset += len;
                    }
                }

                isUploaded = true;
            }
            catch (Exception ex)
            {
                _logHelper.LogError("ImportacaoConfiguracaoTask", "Upload", null, DateTime.Now, exception: ex);
                isUploaded = false;
                errorMessage += $"Upload Error: {targetPath}. Error: Message->{ex.Message}, InnerMessage->{ex.InnerException}";
            }

            return isUploaded;
        }
        private Byte[] ToByteArray(Stream stream)
        {
            byte[] byteArray;
            byte[] chunk = new byte[4096];
            int bytesRead;

            using (var ms = new MemoryStream())
            {
                while ((bytesRead = stream.Read(chunk, 0, chunk.Length)) > 0)
                {
                    ms.Write(chunk, 0, bytesRead);
                }
                byteArray = ms.ToArray();

                //if (byteArray.Length > 0)
                //    countRows = Encoding.UTF8.GetString(byteArray).Split("\r\n").Count();
            }

            return byteArray;
        }
        private string[] GetFiles(string path, NetworkCredential Credentials, string extension)
        {
            var request = (FtpWebRequest)WebRequest.Create(path);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = Credentials;
            //request.Timeout = 15000;
            //request.ReadWriteTimeout = 15000;
            //request.UseBinary = true;
            //request.KeepAlive = true;
            //request.UsePassive = false;
            List<string> files = new List<string>();

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream, true))
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            if (line.Contains(extension, StringComparison.InvariantCultureIgnoreCase))
                                files.Add(line);
                        }
                    }
                }
            }
            return files.ToArray();
        }
        private ImapClient ObterConexaoImap(ImportacaoConfiguracao configuracao, string mailBox)
        {
            var client = new ImapClient(configuracao.Diretorio, 993, true);
            client.Login(configuracao.Usuario, configuracao.Senha, AuthMethod.Auto);

            bool existeMailBox = client.ListMailboxes().Any(x => x.Equals(mailBox));
            if (!existeMailBox)
                client.CreateMailbox(mailBox);

            client.DefaultMailbox = mailBox;
            return client;
        }
        private void CriarPastasEmail(ImapClient client, List<string> pastas)
        {
            foreach (string pasta in pastas)
            {
                string newMailBox = $"{pasta}";
                bool existePasta = client.ListMailboxes().Any(x => x.Equals(newMailBox));
                if (!existePasta)
                    client.CreateMailbox(newMailBox);
            }
        }
        #endregion
    }
}
