using Fretter.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Entities
{
    public class ImportacaoConfiguracao : EntityBase
    {
        public ImportacaoConfiguracao()
        {

        }
        public ImportacaoConfiguracao(int id, EnumImportacaoConfiguracaoTipo importacaoConfiguracaoTipoId, int? empresaId, int? transportadorId, EnumImportacaoArquivoTipo importacaoArquivoTipoId, string diretorio, string usuario, string senha, string outro, DateTime? ultimaExecucao, string ultimoRetorno, bool? sucesso, bool? compactado, string diretorioSucesso, string diretorioErro)
        {
            Id = id;
            ImportacaoConfiguracaoTipoId = importacaoConfiguracaoTipoId.GetHashCode();
            EmpresaId = empresaId;
            TransportadorId = transportadorId;
            ImportacaoArquivoTipoId = importacaoArquivoTipoId.GetHashCode();
            Diretorio = diretorio;
            Usuario = usuario;
            Senha = senha;
            Outro = outro;
            UltimaExecucao = ultimaExecucao;
            UltimoRetorno = ultimoRetorno;
            Sucesso = sucesso;
            Compactado = compactado;
            DiretorioSucesso = diretorioSucesso;
            DiretorioErro = diretorioErro;
        }

        public int ImportacaoConfiguracaoTipoId { get; set; }
        public int? EmpresaId { get; protected set; }
        public int? TransportadorId { get; protected set; }
        public int ImportacaoArquivoTipoId { get; protected set; }
        public string Diretorio { get; protected set; }
        public string DiretorioSucesso { get; protected set; }
        public string DiretorioErro { get; protected set; }
        public string Usuario { get; protected set; }
        public string Senha { get; protected set; }
        public string Outro { get; protected set; }
        public DateTime? UltimaExecucao { get; protected set; }
        public string UltimoRetorno { get; protected set; }
        public bool? Sucesso { get; protected set; }
        public bool? Compactado { get; protected set; }

        #region Fusion
        public virtual Empresa Empresa { get; set; }
        public virtual Fusion.Transportador Transportador { get; set; }
        #endregion

        public virtual ImportacaoArquivoTipo ArquivoTipo { get; set; }
        public virtual ImportacaoConfiguracaoTipo ConfiguracaoTipo { get; set; }

        #region "Métodos"
        public void AtualizarConfiguracaoTipo(EnumImportacaoConfiguracaoTipo enumConfiguracaoTipo) => this.ImportacaoConfiguracaoTipoId = enumConfiguracaoTipo.GetHashCode();
        public void AtualizarEmpresa(int? EmpresaId) => this.EmpresaId = EmpresaId;
        public void AtualizarTransportador(int? TransportadorId) => this.TransportadorId = TransportadorId;
        public void AtualizarArquivoTipo(EnumImportacaoArquivoTipo enumArquivoTipo) => this.ImportacaoArquivoTipoId = enumArquivoTipo.GetHashCode();
        public void AtualizarDiretorio(string Diretorio) => this.Diretorio = Diretorio;
        public void AtualizarUsuario(string Usuario) => this.Usuario = Usuario;
        public void AtualizarSenha(string Senha) => this.Senha = Senha;
        public void AtualizarOutro(string Outro) => this.Outro = Outro;
        public void AtualizarUltimoRetorno(string UltimoRetorno) => this.UltimoRetorno = UltimoRetorno;
        public void AtualizarSucesso(bool Sucesso) => this.Sucesso = Sucesso;
        public void AtualizarUltimaExecucao(DateTime? UltimaExecucao) => this.UltimaExecucao = UltimaExecucao;
        public void AtualizarDiretorioSucesso(string Diretorio) => this.DiretorioSucesso = Diretorio;
        public void AtualizarDiretorioErro(string Diretorio) => this.DiretorioErro = Diretorio;
        #endregion
    }
}
