using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Config
{
    public class ImportacaoEntregaConfig
    {
        public string URLImportacaoEntrega { get; set; }
        public string URLImportacaoEntregaRota { get; set; }
        public IntegracaoCRF IntegracaoCRF { get; set; }
        public IntegracaoWP IntegracaoWP { get; set; }
		public IntegracaoLRY IntegracaoLRY { get; set; }
	}

    public class IntegracaoCRF
    {
        public int EmpresaId { get; set; }
    }

	public class IntegracaoLRY
	{
		public int EmpresaId { get; set; }
        public int IdLojista { get; set; }
        public string TransportadorNome { get; set; }
	}

	public class IntegracaoWP
    {
        public int EmpresaId { get; set; }
        public string EmpresaEntregaToken { get; set; }
        public List<string> TransportadorasCNPJ { get; set; }
        public Credenciais Credenciais { get; set; }
    }

    public class Credenciais
    {
        public string GrantType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string AuthToken { get; set; }
        public string AuthURL { get; set; }
        public string AuthURLRoute { get; set; }
    }
}
