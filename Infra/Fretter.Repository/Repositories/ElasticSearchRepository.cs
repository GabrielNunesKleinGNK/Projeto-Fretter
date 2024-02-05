using Elasticsearch.Net;
using Fretter.Domain.Config;
using Fretter.Domain.Dto.LogElasticSearch;
using Fretter.Domain.Enum;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Repository.Util;
using Nest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Repository.Repositories
{
    public class ElasticSearchRepository : IElasticSearchRepository
    {
        ElasticClient _elasticClient;
        public void InitElasticSearch(ElasticSearchConfig config, EnumElasticConexaoTipo tipo)
        {
            StaticConnectionPool connectionPool;
            ConnectionSettings connectionSettings;

            
            
            if (tipo == EnumElasticConexaoTipo.PorUri)
            {
                var nodes = new Uri[] { new Uri(config.ConnectionUri) };
                connectionPool = new StaticConnectionPool(nodes);

                connectionSettings = new ConnectionSettings(connectionPool);
                connectionSettings.BasicAuthentication(config.Usuario, config.Senha);
            }
            else
            {
                var credentials = new BasicAuthenticationCredentials(config.Usuario, config.Senha);
                var pool = new CloudConnectionPool(config.ConnectionPool, credentials);

                connectionSettings = new ConnectionSettings(pool);
            }
            
            _elasticClient = new ElasticClient(connectionSettings);
        }

        public async Task<bool> CreateDocument(string indexName, object obj, string documentId)
        {
            var response = await _elasticClient.IndexAsync(obj, i => i
            .Index(indexName.ToLower() + "_alias")
            .Id(documentId)
            .Refresh(Refresh.True));

            return response.IsValid;
        }


        public async Task<ISearchResponse<object>> Search(string indexName, 
            Dictionary<string, string> parameters, object filter, int? size =null, int? skip =null, string fieldAggregate = null, 
            DateInterval? dateInterval = null)
        {
            var response = await _elasticClient.SearchAsync<object>(s =>s.GetSearchDescriptor(indexName, parameters, filter, size,
                skip,fieldAggregate,dateInterval));
            
            return response;
        }


        public async Task<T> GetDocument<T>(string indexName, string documentId)
        {
            var response = await _elasticClient.SearchAsync<object>(s => s
            .Index(indexName + "_alias")
            .Query(q => q.Term(t => t.Field("_id").Value(documentId))));

            return (T)response;
        }

        public async Task UpdateDocument(string indexName, object obj, string documentId)
        {
            var response = await _elasticClient.IndexAsync(obj, i => i
            .Index(indexName)
            .Id(documentId)
            .Refresh(Refresh.True));
        }

        public async Task DeleteDocument<T>(string indexName, string documentId)
        {
            var response = await _elasticClient.DeleteAsync<object>(documentId, d => d
            .Index(indexName));
        }
    }
}
