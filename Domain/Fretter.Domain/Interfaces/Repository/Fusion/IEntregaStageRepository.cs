using  Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Dto.Carrefour;

namespace  Fretter.Domain.Interfaces.Repository
{
    public interface IEntregaStageRepository<TContext> : IRepositoryBase<TContext, EntregaStage> 
        where TContext : IUnitOfWork<TContext>
    {
        IEnumerable<StageEtiquetaReciclagemFilaDTO> ObterEtiquetasParaReciclagem(DateTime dataAtual, DateTime dataInicialParaBusca);
    }
}	
	
