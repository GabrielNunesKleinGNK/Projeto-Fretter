

using System;

namespace Fretter.Domain.Entities
{
    public class ContratoTransportadorHistorico : EntityBase
    {
        #region "Construtores"
        public ContratoTransportadorHistorico(int Id, int contratoTransportadorId, int TransportadorId, string Descricao, int? QuantidadeTentativas,
                    decimal? TaxaTentativaAdicional, decimal? TaxaRetornoRemetente, DateTime? VigenciaInicial, DateTime? VigenciaFinal,
                    int TransportadorCnpjId, int TransportadorCnpjCobrancaId, int FaturaCicloId, bool permiteTolerancia,
                    int? toleranciaTipoId, decimal toleranciaSuperior, decimal toleranciaInferior, int? microServicoId)
        {
            this.Ativar();
            this.Id = Id;
            this.TransportadorId = TransportadorId;
            this.Descricao = Descricao;
            this.QuantidadeTentativas = QuantidadeTentativas;
            this.TaxaTentativaAdicional = TaxaTentativaAdicional;
            this.TaxaRetornoRemetente = TaxaRetornoRemetente;
            this.VigenciaInicial = VigenciaInicial;
            this.VigenciaFinal = VigenciaFinal;
            this.TransportadorCnpjId = TransportadorCnpjId;
            this.TransportadorCnpjCobrancaId = TransportadorCnpjCobrancaId;
            this.FaturaCicloId = FaturaCicloId;
            AtualizarPermiteTolerancia(permiteTolerancia);
            AtualizarToleranciaTipoId(toleranciaTipoId);
            AtualizarToleranciaSuperior(toleranciaSuperior);
            AtualizarToleranciaInferior(toleranciaInferior);
            AtualizarContratoTransportadorId(contratoTransportadorId);
            AtualizarMicroServicoId(microServicoId);
        }

        public ContratoTransportadorHistorico(ContratoTransportador contratoTransportador)
        {
            this.Ativar();
            this.Id = Id;
            AtualizarTransportadorId(contratoTransportador.TransportadorId);
            AtualizarDescricao(contratoTransportador.Descricao);
            AtualizarQuantidadeTentativas(contratoTransportador.QuantidadeTentativas);
            AtualizarTaxaRetornoRemetente(contratoTransportador.TaxaRetornoRemetente);
            AtualizarTaxaTentativaAdicional(contratoTransportador.TaxaTentativaAdicional);
            AtualizarVigenciaInicial(contratoTransportador.VigenciaInicial);
            AtualizarVigenciaFinal(contratoTransportador.VigenciaFinal);
            this.TransportadorCnpjId = contratoTransportador.TransportadorCnpjId;
            this.TransportadorCnpjCobrancaId = contratoTransportador.TransportadorCnpjCobrancaId;
            this.FaturaCicloId = contratoTransportador.FaturaCicloId;
            AtualizarPermiteTolerancia(contratoTransportador.PermiteTolerancia);
            AtualizarToleranciaTipoId(contratoTransportador.ToleranciaTipoId);
            AtualizarToleranciaSuperior(contratoTransportador.ToleranciaSuperior);
            AtualizarToleranciaInferior(contratoTransportador.ToleranciaInferior);
            AtualizarContratoTransportadorId(contratoTransportador.Id);
            AtualizarMicroServicoId(contratoTransportador.MicroServicoId);
            AtualizarEmpresaId(contratoTransportador.EmpresaId);
            AtualizarDataCriacao();
        }
        #endregion

        #region "Propriedades"
        public int ContratoTransportadorId { get; protected set; }
        public int TransportadorId { get; protected set; }
        public string Descricao { get; protected set; }
        public int? QuantidadeTentativas { get; protected set; }
        public decimal? TaxaTentativaAdicional { get; protected set; }
        public decimal? TaxaRetornoRemetente { get; protected set; }
        public DateTime? VigenciaInicial { get; protected set; }
        public DateTime? VigenciaFinal { get; protected set; }
        public int TransportadorCnpjId { get; protected set; }
        public int TransportadorCnpjCobrancaId { get; protected set; }
        public int EmpresaId { get; protected set; }
        public int FaturaCicloId { get; set; }
        public bool PermiteTolerancia { get; set; }
        public decimal ToleranciaSuperior { get; set; }
        public decimal ToleranciaInferior { get; set; }
        public int? ToleranciaTipoId { get; set; }
        public int? MicroServicoId { get; set; }
        #endregion

        #region "Referencias"
        public virtual Fusion.Transportador Transportador { get; set; }
        public virtual Fusion.TransportadorCnpj TransportadorCnpj { get; set; }
        public virtual Fusion.TransportadorCnpj TransportadorCnpjCobranca { get; set; }
        public virtual ContratoTransportador ContratoTransportador { get; set; }
        public virtual FaturaCiclo FaturaCiclo { get; set; }
        public virtual ToleranciaTipo ToleranciaTipo { get; set; }
        public virtual Fusion.AspNetUsers CadastroUsuario { get; set; }
        #endregion

        #region "Métodos"
        public void AtualizarMicroServicoId(int? microServicoId) => this.MicroServicoId = microServicoId;
        public void AtualizarContratoTransportadorId(int contratoTransportadorId) => this.ContratoTransportadorId = contratoTransportadorId;
        public void AtualizarEmpresaId(int empresaId) => this.EmpresaId = empresaId;
        public void AtualizarTransportadorId(int transportadorId) => this.TransportadorId = transportadorId;
        public void AtualizarDescricao(string descricao) => this.Descricao = descricao;
        public void AtualizarQuantidadeTentativas(int? quantidadeTentativas) => this.QuantidadeTentativas = quantidadeTentativas;
        public void AtualizarTaxaTentativaAdicional(decimal? taxaTentativaAdicional) => this.TaxaTentativaAdicional = taxaTentativaAdicional;
        public void AtualizarTaxaRetornoRemetente(decimal? taxaRetornoRemetente) => this.TaxaRetornoRemetente = taxaRetornoRemetente;
        public void AtualizarVigenciaInicial(DateTime? vigenciaInicial) => this.VigenciaInicial = vigenciaInicial;
        public void AtualizarVigenciaFinal(DateTime? vigenciaFinal) => this.VigenciaFinal = vigenciaFinal;

        public void AtualizarToleranciaTipoId(int? toleranciaTipoId) => this.ToleranciaTipoId = toleranciaTipoId;
        public void AtualizarPermiteTolerancia(bool permiteTolerancia) => this.PermiteTolerancia = permiteTolerancia;
        public void AtualizarToleranciaInferior(decimal toleranciaInferior) => this.ToleranciaInferior = toleranciaInferior;
        public void AtualizarToleranciaSuperior(decimal toleranciaSuperior) => this.ToleranciaSuperior = toleranciaSuperior;
        #endregion
    }
}
