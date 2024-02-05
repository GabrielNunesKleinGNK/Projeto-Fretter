using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class EmpresaTransporteConfiguracao : EntityBase
    {
        #region "Construtores"
        public EmpresaTransporteConfiguracao(int Id, int EmpresaTransporteTipoItemId, int EmpresaId, string CodigoContrato, string CodigoIntegracao, string CodigoCartao, string Usuario, string Senha,
                                             DateTime? VigenciaInicial, DateTime? VigenciaFinal, int DiasValidade, string RetornoValidacao, DateTime? DataValidacao, bool Valido, bool Producao)
        {
            this.Ativar();
            this.Id = Id;
            this.EmpresaTransporteTipoItemId = EmpresaTransporteTipoItemId;
            this.EmpresaId = EmpresaId;
            this.CodigoContrato = CodigoContrato;
            this.CodigoIntegracao = CodigoIntegracao;
            this.CodigoCartao = CodigoCartao;
            this.Usuario = Usuario;
            this.Senha = Senha;
            this.VigenciaInicial = VigenciaInicial;
            this.VigenciaFinal = VigenciaFinal;
            this.DiasValidade = DiasValidade;
            this.RetornoValidacao = RetornoValidacao;
            this.DataCadastro = DataCadastro;
            this.DataValidacao = DataValidacao;
            this.Valido = Valido;
            this.Producao = Producao;
            this.Ativo = Ativo;            
        }
        #endregion

        #region "Propriedades"
        public int EmpresaTransporteTipoItemId { get; protected set; }
        public int EmpresaId { get; protected set; }
        public string CodigoContrato { get; protected set; }
        public string CodigoIntegracao { get; protected set; }
        public string CodigoCartao { get; protected set; }
        public string Usuario { get; protected set; }
        public string Senha { get; protected set; }
        public DateTime? VigenciaInicial { get; protected set; }
        public DateTime? VigenciaFinal { get; protected set; }
        public int DiasValidade { get; protected set; }
        public string RetornoValidacao { get; protected set; }
        public DateTime? DataValidacao { get; protected set; }
        public bool Valido { get; protected set; }
        public bool Producao { get; protected set; }
        #endregion

        #region "Referencias"
        public virtual EmpresaTransporteTipoItem EmpresaTransporteTipoItem { get; set; }
        public virtual List<EmpresaTransporteConfiguracaoItem> EmpresaTransporteConfiguracaoItems { get; set; }
        #endregion

        #region "Métodos"
        public void AtualizarEmpresaTransporteTipoItemId(int EmpresaTransporteTipoItemId) => this.EmpresaTransporteTipoItemId = EmpresaTransporteTipoItemId;
        public void AtualizarEmpresaId(int EmpresaId) => this.EmpresaId = EmpresaId;
        public void AtualizarCodigoContrato(string CodigoContrato) => this.CodigoContrato = CodigoContrato;
        public void AtualizarCodigoIntegracao(string CodigoIntegracao) => this.CodigoIntegracao = CodigoIntegracao;
        public void AtualizarCodigoCodigoCartao(string CodigoCartao) => this.CodigoCartao = CodigoCartao;
        public void AtualizarUsuario(string Usuario) => this.Usuario = Usuario;
        public void AtualizarSenha(string Senha) => this.Senha = Senha;
        public void AtualizarVigenciaInicial(DateTime? VigenciaInicial) => this.VigenciaInicial = VigenciaInicial;
        public void AtualizarVigenciaFinal(DateTime? VigenciaFinal) => this.VigenciaFinal = VigenciaFinal;
        public void AtualizarDiasValidade(int DiasValidade) => this.DiasValidade = DiasValidade;
        public void AtualizarRetornoValidacao(string RetornoValidacao) => this.RetornoValidacao = RetornoValidacao;
        public void AtualizarValidacao(DateTime? DataValidacao) => this.DataValidacao = DataValidacao;
        public void AtualizarValido(bool Valido) => this.Valido = Valido;
        public void AtualizarProducao(bool Producao) => this.Producao = Producao;
        #endregion
    }
}
