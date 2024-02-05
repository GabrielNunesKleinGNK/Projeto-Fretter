using Fretter.Domain.Entities.Fretter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IAgendamentoRegraRepository<TContext> : IRepositoryBase<TContext, AgendamentoRegra>
        where TContext : IUnitOfWork<TContext>
    {
        int GravaRegra(AgendamentoRegra agendamentoRegra, RegraGrupoItem regraGrupoItem, DataTable regraItens);
        int InativarRegra(int id, int usuarioAlteracao);
    }
}
