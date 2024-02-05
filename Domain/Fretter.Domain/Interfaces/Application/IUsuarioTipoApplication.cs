using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IUsuarioTipoApplication<TContext> : IApplicationBase<TContext, UsuarioTipo>
		where TContext : IUnitOfWork<TContext>
	{
			
	}
}
	