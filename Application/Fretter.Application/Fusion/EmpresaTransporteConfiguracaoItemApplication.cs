using Fretter.Domain.Dto.EmpresaTransporteTipoItem;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fretter.Application
{
    public class EmpresaTransporteConfiguracaoItemApplication<TContext> : ApplicationBase<TContext, EmpresaTransporteConfiguracaoItem>, IEmpresaTransporteConfiguracaoItemApplication<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        public new readonly IEmpresaTransporteConfiguracaoItemService<TContext> _service;
        public EmpresaTransporteConfiguracaoItemApplication(IUnitOfWork<TContext> context, IEmpresaTransporteConfiguracaoItemService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }
    }
}

