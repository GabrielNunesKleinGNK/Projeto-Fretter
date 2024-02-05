using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Fretter.Domain.Interfaces.Repository;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IEmpresaRepository<TContext> : IRepositoryBase<TContext, Empresa>
        where TContext : IUnitOfWork<TContext>
    {
        bool ProcessaPermissaoEmpresa(string email, string cnpj, int tipoPermissao);
        Empresa ObterEmpresaPeloCanalPorCnpj(string cnpj);        
    }
}

