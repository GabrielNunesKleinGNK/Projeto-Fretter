using Fretter.Domain.Entities;

namespace Fretter.Domain.Interfaces
{
    public interface IUsuarioHelper
    {
        UsuarioIdentity UsuarioLogado { get; }
    }
}
