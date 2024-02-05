using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fretter.Domain.Entities
{
    public class Canal : EntityBase
    {
        #region "Construtores"
        public Canal()
        {}
        public Canal(int Id, DateTime? Inclusao, string RazaoSocial, string NomeFantasia, string Cnpj, string CanalNome, int? SegmentoId, Int16? OrigemImportacaoId, int? EmpresaId)
        {
            this.Ativar();
            this.Id = Id;
            this.Inclusao = Inclusao;
            this.RazaoSocial = RazaoSocial;
            this.NomeFantasia = NomeFantasia;
            this.Ativo = Ativo;
            this.Cnpj = Cnpj;
            this.CanalNome = CanalNome;
            this.SegmentoId = SegmentoId;
            this.OrigemImportacaoId = OrigemImportacaoId;
            this.EmpresaId = EmpresaId;
        }
        public Canal(int Id, DateTime Inclusao, string RazaoSocial, string NomeFantasia, string Cnpj, string CanalNome, Int16? OrigemImportacaoId, EmpresaSegmento Segmento, Empresa Empresa)
        {
            this.Ativar();
            this.Id = Id;
            this.Inclusao = Inclusao;
            this.RazaoSocial = RazaoSocial;
            this.NomeFantasia = NomeFantasia;
            this.Ativo = Ativo;
            this.Cnpj = Cnpj;
            this.CanalNome = CanalNome;
            this.OrigemImportacaoId = OrigemImportacaoId;
            this.Segmento = Segmento;
            this.Empresa = Empresa;
        }
        #endregion

        #region "Propriedades"
        public DateTime? Inclusao { get; protected set; }
        public string RazaoSocial { get; protected set; }
        public string NomeFantasia { get; protected set; }
        public string Cnpj { get; protected set; }
        public string CanalNome { get; protected set; }
        public int? SegmentoId { get; protected set; }
        [NotMapped]
        public Int64? CnpjUnico { get; protected set; }
        public Int16? OrigemImportacaoId { get; protected set; }
        public string InscricaoEstadual { get; protected set; }
        public int? EmpresaId { get; protected set; }
        #endregion

        #region "Referencias"
        public EmpresaSegmento Segmento { get; protected set; }
        public Empresa Empresa { get; protected set; }
        public CanalConfig CanalConfig { get; private set; }
        #endregion

        #region "Métodos"
        public void AtualizarInclusao(DateTime Inclusao) => this.Inclusao = Inclusao;
        public void AtualizarRazaoSocial(string RazaoSocial) => this.RazaoSocial = RazaoSocial;
        public void AtualizarNomeFantasia(string NomeFantasia) => this.NomeFantasia = NomeFantasia;
        public void AtualizarCnpj(string Cnpj) => this.Cnpj = Cnpj;
        public void AtualizarCanal(string Canal) => this.CanalNome = Canal;
        public void AtualizarSegmento(int Segmento) => this.SegmentoId = Segmento;
        public void AtualizarCnpjUnico(Int64? CnpjUnico) => this.CnpjUnico = CnpjUnico;
        public void AtualizarOrigemImportacao(Int16? OrigemImportacao) => this.OrigemImportacaoId = OrigemImportacao;
        public void AtualizarInscricaoEstadual(string InscricaoEstadual) => this.InscricaoEstadual = InscricaoEstadual;
        public void AtualizarEmpresa(int Empresa) => this.EmpresaId = Empresa;
        public void AtualizarCanalConfig(CanalConfig config) => this.CanalConfig = config;
        #endregion
    }
}
