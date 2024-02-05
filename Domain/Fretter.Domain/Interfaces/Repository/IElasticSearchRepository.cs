using Fretter.Domain.Config;
using Fretter.Domain.Enum;
using Nest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IElasticSearchRepository
    {
        void InitElasticSearch(ElasticSearchConfig config, EnumElasticConexaoTipo tipo);
        Task<bool> CreateDocument(string indexName, object obj, string documentId);
        Task<ISearchResponse<object>> Search(string indexName, Dictionary<string, string> parameters, object filter, int? size=null, 
            int? skip = null, string fieldAggregate = null,DateInterval? dateInterval = null);
        Task<T> GetDocument<T>(string indexName, string documentId);
        Task UpdateDocument(string indexName, object obj, string documentId);
        Task DeleteDocument<T>(string indexName, string documentId);
    }
}
