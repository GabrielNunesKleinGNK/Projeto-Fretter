using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fretter;

namespace Fretter.Api.Models
{
    public class ContratoTransportadorViewModel : IViewModel<ContratoTransportador>
    {
        public int Id { get; set; }
        public int TransportadorId { get; set; }
        public string Descricao { get; set; }
        public int? QuantidadeTentativas { get; set; }
        public decimal? TaxaTentativaAdicional { get; set; }
        public decimal? TaxaRetornoRemetente { get; set; }
        public DateTime? VigenciaInicial { get; set; }
        public DateTime? VigenciaFinal { get; set; }
        public bool? Ativo { get; set; }
        public int? UsuarioCadastro { get; set; }
        public int? UsuarioAlteracao { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int TransportadorCnpjId { get; set; }
        public int TransportadorCnpjCobrancaId { get; set; }
        public int FaturaCicloId { get; set; }
        public bool PermiteTolerancia { get; set; }
        public bool FaturaAutomatica { get; set; }
        public decimal ToleranciaSuperior { get; set; }
        public decimal ToleranciaInferior { get; set; }
        public int? ToleranciaTipoId { get; set; }
        public int? MicroServicoId { get; set; }
        public bool? RecotaPesoTransportador { get; set; }
        public bool ConciliaEntregaFinalizada { get; set; }
        public ContratoTransportadorRegraViewModel Generalidades { get; set; }
        public FaturaCicloViewModel FaturaCiclo { get; set; }
        public ToleranciaTipoViewModel ToleranciaTipo { get; set; }
        public Fusion.TransportadorViewModel Transportador { get; set; }
        public Fusion.TransportadorCnpjViewModel TransportadorCnpj { get; set; }
        public Fusion.TransportadorCnpjViewModel TransportadorCnpjCobranca { get; set; }
        public virtual Fusion.AspNetUsersViewModel CadastroUsuario { get; set; }
        public virtual Fusion.AspNetUsersViewModel AlteracaoUsuario { get; set; }

        public ContratoTransportador Model()
        {
            return new ContratoTransportador(Id, TransportadorId, Descricao, QuantidadeTentativas, TaxaTentativaAdicional,
                TaxaRetornoRemetente, VigenciaInicial, VigenciaFinal, TransportadorCnpjId, TransportadorCnpjCobrancaId, FaturaCicloId,
                PermiteTolerancia, ToleranciaTipoId, ToleranciaSuperior, ToleranciaInferior, MicroServicoId, FaturaAutomatica, RecotaPesoTransportador, ConciliaEntregaFinalizada);
        }
    }
}
