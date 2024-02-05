using System;
namespace Fretter.Domain.Config
{
    public class BlobStorageConfig
    {
        public string ConnectionString { get; set; }
        public string ConciliacaoContainerName { get; set; }
        public string EmpresaContainerName { get; set; }
        public string MenuFreteContainerName { get; set; }
        public string RecotacaoFreteContainerName { get; set; }
        public string TabelasCorreiosName { get; set; }
        public BlobStorageConfig()
        {

        }
    }
}
