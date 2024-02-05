using Fretter.Domain.Dto.Fusion;
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
    public class EmpresaTransporteTipoItemApplication<TContext> : ApplicationBase<TContext, EmpresaTransporteTipoItem>, IEmpresaTransporteTipoItemApplication<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        public new readonly IEmpresaTransporteTipoItemService<TContext> _service;
        public EmpresaTransporteTipoItemApplication(IUnitOfWork<TContext> context, IEmpresaTransporteTipoItemService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }

        public IEnumerable<EmpresaTransporteTipoItem> GetEmpresaTransporteItemPorTipo(int transporteTipoId)
        {
            return _service.GetEmpresaTransporteItemPorTipo(transporteTipoId);
        }
    }
}

