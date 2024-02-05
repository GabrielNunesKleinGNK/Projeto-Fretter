using Fretter.Domain.Dto.RecotacaoFrete;
using Fretter.Domain.Entities;
using Fretter.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Data;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IRecotacaoFreteRepository<TContext> : IRepositoryBase<TContext, RecotacaoFrete> 
        where TContext : IUnitOfWork<TContext>
    {
        void AtualizarRecotacaoFrete(int idRecotacaoFrete, EnumRecotacaoFreteStatus recotacaoFreteStatus, int qtAdvertencia = 0, int qtErros = 0, string objRetorno = "");
        void InserirRecotacaoFreteItem(DataTable itens);
        List<MenuFreteCotacao> BuscarDadosPedido(DataTable itens);
    }
}	
	
