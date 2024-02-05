using  Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Dto.Fatura;
using System.Threading.Tasks;
using Fretter.Domain.Dto.Fretter.Conciliacao;
using System.Data;

namespace  Fretter.Domain.Interfaces.Repository
{
    public interface IFaturaRepository<TContext> : IRepositoryBase<TContext, Fatura> 
        where TContext : IUnitOfWork<TContext>
    {
        Task<List<Fatura>> GetFaturasDaEmpresa(int empresaId, FaturaFiltro filtro);
        Task<List<Fatura>> GetFaturasPorId(List<int> faturas);
        List<DemonstrativoRetorno> GetDemonstrativoPorFatura(DataTable listFaturaId);
        List<Item> ProcessarFaturaManual(DataTable entregaConciliacao, int userId);
        List<Item> ProcessarFaturaImportacao(DataTable entregaConciliacao, int usuarioId, int faturaArquivoId);
        List<FaturaArquivoCriticaDTO> ValidarDataEmissaoNF(DataTable lstDataEmissaoNF);
    }
}	
	
