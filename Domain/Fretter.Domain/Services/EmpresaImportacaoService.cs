using Fretter.Domain.Dto.Fusion;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fretter.Domain.Services
{
    public class EmpresaImportacaoService<TContext> : ServiceBase<TContext, EmpresaImportacaoArquivo>, IEmpresaImportacaoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IUsuarioHelper _user;
        private readonly IEmpresaImportacaoRepository<TContext> _repository;
        public EmpresaImportacaoService(
                IEmpresaImportacaoRepository<TContext> repository,
                IUnitOfWork<TContext> unitOfWork,
                IUsuarioHelper user) : base(repository, unitOfWork, user)
        {
            _user = user;
            _repository = repository;
        }

        public IEnumerable<EmpresaImportacaoArquivo> GetEmpresaImportacaoArquivoPorEmpresa(EmpresaImportacaoFiltro filtro)
        {                        
            return _repository.GetEmpresaImportacaoArquivoPorEmpresa(_user.UsuarioLogado.EmpresaId, filtro);
        }
    }
}
