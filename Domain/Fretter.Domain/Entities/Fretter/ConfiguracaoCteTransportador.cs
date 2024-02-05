using Fretter.Domain.Enum;
using System;

namespace Fretter.Domain.Entities
{
    public class ConfiguracaoCteTransportador : EntityBase
    {
		#region "Construtores"
        private ConfiguracaoCteTransportador() { }
        public ConfiguracaoCteTransportador(int Id, int transportadorCnpjId, EnumConfiguracaoCteTipo configuracaoCteTipoId, string alias)
		{
            if(Id > 0)
			    this.AtualizarId(Id);
            AtualizarTransportadorCnpjId(transportadorCnpjId);
            AtualizarTipo(configuracaoCteTipoId);
            AtualizarAlias(alias);
		}

        #endregion

        #region "Propriedades"
        public int TransportadorCnpjId { get; protected set; }
        public int ConfiguracaoCteTipoId { get; protected set; }
        public string Alias { get; protected set; }
        public int EmpresaId { get; protected set; }
        #endregion

        #region "Referencias"
        public virtual Fusion.TransportadorCnpj TransportadorCnpj { get; set; }
        public virtual ConfiguracaoCteTipo ConfiguracaoCteTipo { get; set; }
        #endregion

        #region "Métodos"
        public void AtualizarTransportadorCnpjId(int id) => this.TransportadorCnpjId = id;
        public void AtualizarAlias(string alias) => this.Alias = alias;
        public void AtualizarTipo(EnumConfiguracaoCteTipo configTipo) => this.ConfiguracaoCteTipoId = configTipo.GetHashCode();
        public void AtualizarEmpresaId(int empresaId) => this.EmpresaId = empresaId;
        #endregion
    }
}      
