using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fretter.Domain.Entities
{
    public class MicroServico : EntityBase
    {
        #region "Construtores"
        public MicroServico(int Id, int EmpresaTransportadorId, string ServicoCodigo, Int16? OrigemAWBEmbarcador, string Descricao, bool DimensoesPesoObg, bool DadosDestinatarioObg,bool? PrazoAutomatico = null,string ServicoDescricao = null)
        {            
            this.Id = Id;
            this.EmpresaTransportadorId = EmpresaTransportadorId;
            this.ServicoCodigo = ServicoCodigo;
            this.OrigemAWBEmbarcador = OrigemAWBEmbarcador;
            this.Descricao = Descricao;
            this.DimensoesPesoObg = DimensoesPesoObg;
            this.DadosDestinatarioObg = DadosDestinatarioObg;
            this.PrazoAutomatico = PrazoAutomatico;
            this.ServicoDescricao = ServicoDescricao;
        }
        #endregion

        #region "Propriedades"
        public int EmpresaTransportadorId { get; protected set; }
        public string ServicoCodigo { get; protected set; }
        public Int16? OrigemAWBEmbarcador { get; protected set; }
        public Int16? FormatoEtiqueta { get; protected set; }
        public int? GeraEtiquetaFusion { get; protected set; }
        public string Descricao { get; protected set; }
        public bool DimensoesPesoObg { get; protected set; }
        [NotMapped]
        public int ConfigExpedicaoCompleta { get; protected set; }
        public string ModalidadeTransportador { get; protected set; }
        public bool DadosDestinatarioObg { get; protected set; }
        public string Servico2 { get; protected set; }
        public bool? PrazoAutomatico { get; protected set; }
        public string ServicoDescricao { get; protected set; }
        #endregion

        #region "Referencias"
        public EmpresaTransportadorConfig EmpresaTransportadorConfig { get; protected set; }
        #endregion

        #region "Métodos"
        public void AtualizarEmpresaTransportador(int EmpresaTransportador) => this.EmpresaTransportadorId = EmpresaTransportador;
        public void AtualizarServicoCodigo(string Servico) => this.ServicoCodigo = Servico;
        public void AtualizarOrigemAWBEmbarcador(Int16? OrigemAWBEmbarcador) => this.OrigemAWBEmbarcador = OrigemAWBEmbarcador;
        public void AtualizarFormatoEtiqueta(Int16? FormatoEtiqueta) => this.FormatoEtiqueta = FormatoEtiqueta;
        public void AtualizarGeraEtiquetaFusion(int? GeraEtiquetaFusion) => this.GeraEtiquetaFusion = GeraEtiquetaFusion;
        public void AtualizarDescricao(string Descricao) => this.Descricao = Descricao;
        public void AtualizarDimensoesPesoObg(bool DimensoesPesoObg) => this.DimensoesPesoObg = DimensoesPesoObg;
        public void AtualizarConfigExpedicaoCompleta(int ConfigExpedicaoCompleta) => this.ConfigExpedicaoCompleta = ConfigExpedicaoCompleta;
        public void AtualizarModalidadeTransportador(string ModalidadeTransportador) => this.ModalidadeTransportador = ModalidadeTransportador;
        public void AtualizarDadosDestinatarioObg(bool DadosDestinatarioObg) => this.DadosDestinatarioObg = DadosDestinatarioObg;
        public void AtualizarServico2(string Servico2) => this.Servico2 = Servico2;
        public void AtualizarPrazoAutomatico(bool? PrazoAutomatico) => this.PrazoAutomatico = PrazoAutomatico;
        public void AtualizarServicoDescricao(string Servico) => this.ServicoDescricao = Servico;
        #endregion
    }
}
