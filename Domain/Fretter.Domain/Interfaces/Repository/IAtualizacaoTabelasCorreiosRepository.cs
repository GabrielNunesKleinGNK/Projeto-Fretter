using  Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Fretter.Domain.Interfaces.Repository;
using System.Data;

namespace  Fretter.Domain.Interfaces.Repository
{
    public interface IAtualizacaoTabelasCorreiosRepository<TContext> : IRepositoryBase<TContext, TabelasCorreiosArquivo> 
        where TContext : IUnitOfWork<TContext>
    {
        bool ImportarDadosTabelasTemps(DataTable listCapitais, DataTable listLocais, DataTable listDivisas, DataTable listEstaduais, DataTable listInteriores, DataTable listMatrizes);
        bool AtualizarTabelasFinais();
    }
}	
	
