using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class EmpresaImportacaoArquivo : EntityBase
    {
        #region "Construtores"
        public EmpresaImportacaoArquivo(int Id)
        {
            this.Ativar();
            this.Id = Id;
        }
        public EmpresaImportacaoArquivo(int Id, string Nome, string Descricao, int? EmpresaId, string ArquivoURL)
        {
            this.Ativar();
            this.Id = Id;
            this.Nome = Nome;
            this.Descricao = Descricao;
            this.EmpresaId = EmpresaId;
            this.ArquivoURL = ArquivoURL;
            this.Detalhes = new List<EmpresaImportacaoDetalhe>();
        }
        #endregion

        #region "Propriedades"        
        public string Nome { get; protected set; }
        public string Descricao { get; protected set; }
        public int? EmpresaId { get; protected set; }
        public string ArquivoURL { get; protected set; }
        public int QuantidadeEmpresa { get; protected set; }
        public bool? Processado { get; protected set; }
        public bool? Sucesso { get; protected set; }
        #endregion

        #region "Referencias"
        public ICollection<EmpresaImportacaoDetalhe> Detalhes { get; protected set; }

        #endregion

        #region "Métodos"
        public void AtualizarQuantidadeEmpresa(int QuantidadeEmpresa) => this.QuantidadeEmpresa = QuantidadeEmpresa;
        public void AtualizarProcessado(bool? processado) => this.Processado = processado;
        public void AtualizarSucesso(bool? sucesso) => this.Sucesso = sucesso;
        public void AtualizarDescricao(string descricao) => this.Descricao = descricao;
        #endregion
    }
}

