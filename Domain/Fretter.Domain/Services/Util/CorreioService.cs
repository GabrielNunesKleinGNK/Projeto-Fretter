using Fretter.Domain.Interfaces.Service.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WSCorreios;

namespace Fretter.Domain.Services.Util
{
    public class CorreioService : ICorreioService
    {
        AtendeClienteClient wsCorreio;

        public async Task<solicitaXmlPlpResponse> ConsultaXmlPLP(string codigoPlp, string usuario, string senha, bool ambienteProd)
        {
            try
            {
                wsCorreio = new AtendeClienteClient(ambienteProd ? AtendeClienteClient.EndpointConfiguration.AtendeClientePort : AtendeClienteClient.EndpointConfiguration.AtendeClienteHmlPort);
                var retorno = wsCorreio.solicitaXmlPlpAsync(Convert.ToInt64(codigoPlp), usuario, senha).Result;

                return retorno;
            }
            catch (System.Exception ex)
            {
                return new solicitaXmlPlpResponse(ex.InnerException.Message);
            }
        }
    }
}
