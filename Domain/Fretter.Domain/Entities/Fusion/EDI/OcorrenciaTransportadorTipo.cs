using Fretter.Domain.Entities.Fusion;

namespace Fretter.Domain.Entities
{
    public class OcorrenciaTransportadorTipo : EntityBase
    {
        #region "Construtores"
        public OcorrenciaTransportadorTipo(int Id, string sigla, int transportadorId, int ocorrenciaTipoId)
        {
            this.Ativar();
            this.Id = Id;
            this.Sigla = sigla;
            this.TransportadorId = transportadorId;
            this.OcorrenciaTipoId = ocorrenciaTipoId;
            this.Ativo = Ativo;
        }
        #endregion

        #region "Propriedades"
        public string Sigla { get; protected set; }
        public int TransportadorId { get; protected set; }
        public int OcorrenciaTipoId { get; protected set; }

        #endregion

        #region "Referencias"
        public Transportador Transportador { get; protected set; }
        public OcorrenciaTipo OcorrenciaTipo { get; protected set; }
        #endregion

        #region "Métodos"
        public void AtualizarSigla(string sigla) => this.Sigla = sigla;
        public void AtualizarTransportador(int transportador) => this.TransportadorId = transportador;
        public void AtualizarOcorrenciaTipo(int ocorrenciaTipo) => this.OcorrenciaTipoId = ocorrenciaTipo;
        #endregion
    }
}
