using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fretter.Domain.Entities
{
    public class CanalVenda : EntityBase
    {
        #region "Construtores"
        public CanalVenda(int Id, string CanalVendaNome, bool Default, int EmpresaId)
        {
            this.Id = Id;
            this.CanalVendaNome = CanalVendaNome;
            this.Default = Default;
            this.EmpresaId = EmpresaId;
            this.CanalVendaEntradas = new List<CanalVendaEntrada>();
            this.CanalVendaConfigs = new List<CanalVendaConfig>();
        }
        public CanalVenda(int Id, string CanalVendaNome, bool Default, int EmpresaId, string CanalVendaUnico, int DefaultUnico, DateTime? UltAtualizacaoProduto, string CanalVendaCodigo, byte? TipoIntegracao, bool? EmbalagemUnicaMF, byte? OrigemImportacao)
        {
            this.Id = Id;
            this.CanalVendaNome = CanalVendaNome;
            this.CanalVendaCodigo = CanalVendaCodigo;
            this.Default = Default;
            this.EmpresaId = EmpresaId;
            this.CanalVendaUnico = CanalVendaUnico;
            this.DefaultUnico = DefaultUnico;
            this.UltAtualizacaoProduto = UltAtualizacaoProduto;
            this.TipoIntegracao = TipoIntegracao;
            this.EmbalagemUnicaMF = EmbalagemUnicaMF;
            this.OrigemImportacao = OrigemImportacao;
            this.CanalVendaEntradas = new List<CanalVendaEntrada>();
            this.CanalVendaConfigs = new List<CanalVendaConfig>();
        }
        #endregion

        #region "Propriedades"        
        public bool Default { get; protected set; }
        public int EmpresaId { get; protected set; }
        [NotMapped]
        public string CanalVendaUnico { get; protected set; }
        [NotMapped]
        public int DefaultUnico { get; protected set; }
        public string CanalVendaNome { get; protected set; }
        public string CanalVendaCodigo { get; protected set; }
        public DateTime? UltAtualizacaoProduto { get; protected set; }
        public byte? TipoIntegracao { get; protected set; }
        public bool? EmbalagemUnicaMF { get; protected set; }
        public byte? OrigemImportacao { get; protected set; }
        #endregion

        #region "Referencias"
        public ICollection<CanalVendaEntrada> CanalVendaEntradas { get; set; }
        public ICollection<CanalVendaConfig> CanalVendaConfigs { get; set; }
        #endregion

        #region "Métodos"
        public void AtualizarCanalVendaNome(string CanalVendaNome) => this.CanalVendaNome = CanalVendaNome;
        public void AtualizarCanalVendaCodigo(string CanalVendaCodigo) => this.CanalVendaCodigo = CanalVendaCodigo;
        public void AtualizarDefault(bool Default) => this.Default = Default;
        public void AtualizarEmpresa(int Empresa) => this.EmpresaId = Empresa;
        public void AtualizarCanalVendaUnico(string CanalVendaUnico) => this.CanalVendaUnico = CanalVendaUnico;
        public void AtualizarDefaultUnico(int DefaultUnico) => this.DefaultUnico = DefaultUnico;
        public void AtualizarUltAtualizacaoProduto(DateTime? UltAtualizacaoProduto) => this.UltAtualizacaoProduto = UltAtualizacaoProduto;
        public void AtualizarTipoIntegracao(byte? TipoIntegracao) => this.TipoIntegracao = TipoIntegracao;
        public void AtualizarEmbalagemUnicaMF(bool? EmbalagemUnicaMF) => this.EmbalagemUnicaMF = EmbalagemUnicaMF;
        public void AtualizarOrigemImportacao(byte? OrigemImportacao) => this.OrigemImportacao = OrigemImportacao;
        #endregion
    }
}
