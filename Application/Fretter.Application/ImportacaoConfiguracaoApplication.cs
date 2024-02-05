using System.Linq;
using System.Threading.Tasks;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class ImportacaoConfiguracaoApplication<TContext> : ApplicationBase<TContext, ImportacaoConfiguracao>, IImportacaoConfiguracaoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public new readonly IImportacaoConfiguracaoService<TContext> _service;
        public ImportacaoConfiguracaoApplication(IUnitOfWork<TContext> context, IImportacaoConfiguracaoService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }

        public int ProcessarImportacaoConfiguracao()
        {
            int qtdArquivos = _service.ProcessarImportacaoConfiguracao().Result;
            _unitOfWork.Commit();
            return qtdArquivos;
        }
    }
}
