using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WSCorreios;

namespace Fretter.Domain.Interfaces.Service.Util
{
    public interface ICorreioService
    {
        Task<solicitaXmlPlpResponse> ConsultaXmlPLP(string codigoPlp, string usuario, string senha, bool ambienteProd);
    }
}
