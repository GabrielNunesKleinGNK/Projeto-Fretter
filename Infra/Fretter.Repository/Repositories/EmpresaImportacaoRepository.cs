using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Fretter.Domain.Enum;
using Fretter.Domain.Dto.Fatura;
using System.Threading.Tasks;
using Fretter.Domain.Dto.Fusion;

namespace Fretter.Repository.Repositories
{
    public class EmpresaImportacaoRepository<TContext> : RepositoryBase<TContext, EmpresaImportacaoArquivo>, IEmpresaImportacaoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EmpresaImportacaoArquivo> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EmpresaImportacaoArquivo>();
        public EmpresaImportacaoRepository(IUnitOfWork<TContext> context) : base(context)
        {

        }

        public List<EmpresaImportacaoArquivo> GetEmpresaImportacaoArquivoPorEmpresa(int empresaId, EmpresaImportacaoFiltro filtro)
        {
            return _dbSet
                   .Where(x => x.EmpresaId == empresaId && x.Detalhes.Any(t => (t.Cnpj == filtro.Cnpj || filtro.Cnpj == default) && (t.Nome.Contains(filtro.NomeFantasia) || filtro.NomeFantasia == default)))
                   .ToList();
        }

        public EmpresaImportacaoArquivo GetEmpresaImportacaoDetalhePorArquivo(int arquivoId)
        {
            return _dbSet
                   .Where(x => x.Id == arquivoId)
                   .Include(t => t.Detalhes)
                   .FirstOrDefault();
        }
    }
}
