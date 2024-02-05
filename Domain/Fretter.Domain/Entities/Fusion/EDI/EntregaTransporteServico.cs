using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class EntregaTransporteServico : EntityBase
    {
        #region "Construtores"
        public EntregaTransporteServico(int Id, int Empresa, int Transportador, string Nome, string CodigoContrato, string CodigoIntegracao, string URLBase)
        {
            this.Ativar();
            this.Id = Id;
            this.Empresa = Empresa;
            this.Transportador = Transportador;
            this.Nome = Nome;
            this.CodigoContrato = CodigoContrato;
            this.CodigoIntegracao = CodigoIntegracao;
            this.URLBase = URLBase;
            this.Ativo = Ativo;
        }
        #endregion

        #region "Propriedades"
        public int Empresa { get; protected set; }
        public int Transportador { get; protected set; }
        public string Nome { get; protected set; }
        public string Usuario { get; protected set; }
        public string Senha { get; protected set; }
        public string CodigoContrato { get; protected set; }
        public string CodigoIntegracao { get; protected set; }
        public string CodigoCartao { get; protected set; }
        public string URLBase { get; protected set; }
        #endregion

        #region "Referencias"
        public List<EntregaTransporteTipo> EntregaTransporteTipos { get; set; }
        #endregion

        #region "Métodos"
        public void AtualizarEmpresa(int Empresa) => this.Empresa = Empresa;
        public void AtualizarTransportador(int Transportador) => this.Transportador = Transportador;
        public void AtualizarNome(string Nome) => this.Nome = Nome;
        public void AtualizarCodigoContrato(string CodigoContrato) => this.CodigoContrato = CodigoContrato;
        public void AtualizarCodigoIntegracao(string CodigoIntegracao) => this.CodigoIntegracao = CodigoIntegracao;
        public void AtualizarURLBase(string URLBase) => this.URLBase = URLBase;
        #endregion
    }
}
