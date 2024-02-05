using Fretter.Domain.Dto.TabelaArquivo;
using Fretter.Domain.Entities;
using Fretter.Domain.Enum;
using System.Collections.Generic;
using System.Data;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IMenuFreteTabelaArquivoRepository<TContext> : IRepositoryBase<TContext, TabelaArquivo>
        where TContext : IUnitOfWork<TContext>
    {
        List<TabelaArquivoProcessamento> GetTabelArquivoProcessamento();
        List<RegiaoTipo> GetRegiaoTipo(int? idEmpresa, int? idTransportador);
        List<RegiaoCep> GetRegiaoCep(int idTabela);
        void AtualizarTabelaArquivo(int idTabelArquivo, EnumTabelaArquivoStatus enumTabelaArquivoStatus,
            string objRetorno = null, int? qtAdvertencia = null, int? qtErros = null, int? qtRegistros = null, int? nrPercentualAtualizacao = null);
        void InserirLista(int idTabela, int idEmpresa, int idTransportador, bool novo, DataTable lstFaixas,
            DataTable lstRegiao, DataTable lstValor, DataTable lstRegiaoCEP);
        void InserirListaCorreios(int idTabela, int idEmpresa, int idTransportador, bool novo, DataTable lstFaixas,
            DataTable lstRegiao, DataTable lstValor);
        void InserirParametroCep(int idTabela, int idEmpresa, DataTable lstRegiaoCEP);
        void InserirListaVtex(int idTabela, bool novo, DataTable registrosVtex);
        void AtualizarLista(int idTabela, int idEmpresa, int idTransportador, DataTable lstFaixas,
            DataTable lstRegiao, DataTable lstValor, DataTable lstRegiaoCEP);

        void InserirListaAgendamento(int idTabela, int idEmpresa, int idTransportador, 
            DataTable lstRegiaoCEPCapacidade, DataTable lstRegiaoCEP, DataTable lstRegiao);
    }
}

