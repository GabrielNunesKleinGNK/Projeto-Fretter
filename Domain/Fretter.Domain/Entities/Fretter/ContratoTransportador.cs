
using Fretter.Domain.Entities.Fretter;
using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
	public class ContratoTransportador : EntityBase
	{
		#region "Construtores"
		public ContratoTransportador(int Id, int TransportadorId, string Descricao, int? QuantidadeTentativas,
					decimal? TaxaTentativaAdicional, decimal? TaxaRetornoRemetente, DateTime? VigenciaInicial, DateTime? VigenciaFinal,
					int TransportadorCnpjId, int TransportadorCnpjCobrancaId, int FaturaCicloId, bool permiteTolerancia,
					int? toleranciaTipoId, decimal toleranciaSuperior, decimal toleranciaInferior, int? microServicoId, bool faturaAutomatica, bool? recotaPesoTransportador, bool conciliaEntregaFinalizada)
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
			AtualizarMicroServicoId(microServicoId);
			AtualizarFaturaAutomatica(faturaAutomatica);
			AtualizarRecotaPesoTransportador(recotaPesoTransportador);
			AtualizarConciliaEntregaFinalizada(conciliaEntregaFinalizada);
		}
		#endregion

		#region "Propriedades"
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
		public bool FaturaAutomatica { get; protected set; }
		public decimal ToleranciaSuperior { get; set; }
		public decimal ToleranciaInferior { get; set; }
		public int? ToleranciaTipoId { get; set; }
		public int? MicroServicoId { get; set; }
		public bool? RecotaPesoTransportador { get; protected set; }
		public bool ConciliaEntregaFinalizada { get; set; }
		#endregion

		#region "Referencias"
		public virtual Fusion.Transportador Transportador { get; set; }
		public virtual Fusion.TransportadorCnpj TransportadorCnpj { get; set; }
		public virtual Fusion.TransportadorCnpj TransportadorCnpjCobranca { get; set; }
		public virtual FaturaCiclo FaturaCiclo { get; set; }
		public virtual ToleranciaTipo ToleranciaTipo { get; set; }
		public virtual ICollection<ContratoTransportadorHistorico> Historicos { get; set; }
		public virtual Fusion.AspNetUsers CadastroUsuario { get; set; }
		public virtual Fusion.AspNetUsers AlteracaoUsuario { get; set; }
		#endregion

		#region "Métodos"
		public void AtualizarMicroServicoId(int? microServicoId) => this.MicroServicoId = microServicoId;
		public void AtualizarEmpresaId(int empresaId) => this.EmpresaId = empresaId;
		public void AtualizarTransportadorId(int TransportadorId) => this.TransportadorId = TransportadorId;
		public void AtualizarDescricao(string Descricao) => this.Descricao = Descricao;
		public void AtualizarQuantidadeTentativas(int? QuantidadeTentativas) => this.QuantidadeTentativas = QuantidadeTentativas;
		public void AtualizarTaxaTentativaAdicional(decimal? TaxaTentativaAdicional) => this.TaxaTentativaAdicional = TaxaTentativaAdicional;
		public void AtualizarTaxaRetornoRemetente(decimal? TaxaRetornoRemetente) => this.TaxaRetornoRemetente = TaxaRetornoRemetente;
		public void AtualizarVigenciaInicial(DateTime? VigenciaInicial) => this.VigenciaInicial = VigenciaInicial;
		public void AtualizarVigenciaFinal(DateTime? VigenciaFinal) => this.VigenciaFinal = VigenciaFinal;
		public void AtualizarToleranciaTipoId(int? toleranciaTipoId) => this.ToleranciaTipoId = toleranciaTipoId;
		public void AtualizarPermiteTolerancia(bool permiteTolerancia) => this.PermiteTolerancia = permiteTolerancia;
		public void AtualizarFaturaAutomatica(bool faturaAutomatica) => this.FaturaAutomatica = faturaAutomatica;
		public void AtualizarToleranciaInferior(decimal toleranciaInferior) => this.ToleranciaInferior = toleranciaInferior;
		public void AtualizarToleranciaSuperior(decimal toleranciaSuperior) => this.ToleranciaSuperior = toleranciaSuperior;
		public void AtualizarRecotaPesoTransportador(bool? recotaPesoTransportador) => this.RecotaPesoTransportador = recotaPesoTransportador;
		public void AtualizarConciliaEntregaFinalizada(bool conciliaEntregaFinalizada) => this.ConciliaEntregaFinalizada = conciliaEntregaFinalizada;

		#endregion
	}
}
