using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fretter.Domain.Entities
{
    public class EmpresaToken : EntityBase
    {
        #region "Construtores"
        public EmpresaToken(int Id, int EmpresaId, byte EmpresaTokenTipoId, string Nome, Guid Token, bool Padrao, int AspNetUsers, DateTime Inclusao, byte? OrigemImportacao)
        {
            this.Ativar();
            this.Id = Id;
            this.EmpresaId = EmpresaId;
            this.EmpresaTokenTipoId = EmpresaTokenTipoId;
            this.Nome = Nome;
            this.Token = Token;
            this.Padrao = Padrao;
            this.AspNetUsers = AspNetUsers;
            this.Inclusao = Inclusao;
            this.OrigemImportacao = OrigemImportacao;
            this.CanalVendaInterfaces = new List<CanalVendaInterface>();
        }
        #endregion

        #region "Propriedades"
        public int EmpresaId { get; protected set; }
        public byte EmpresaTokenTipoId { get; protected set; }
        public string Nome { get; protected set; }
        public Guid Token { get; protected set; }
        public bool Padrao { get; protected set; }
        [NotMapped]
        public string PadraoUnico { get; protected set; }
        public int AspNetUsers { get; protected set; }
        public DateTime Inclusao { get; protected set; }
        public byte? OrigemImportacao { get; protected set; }
        #endregion

        #region "Referencias"		
        public ICollection<CanalVendaInterface> CanalVendaInterfaces { get; protected set; }
        #endregion

        #region "Métodos"
        public void AtualizarEmpresa(int Empresa) => this.EmpresaId = Empresa;
        public void AtualizarEmpresaTokenTipo(byte EmpresaTokenTipo) => this.EmpresaTokenTipoId = EmpresaTokenTipo;
        public void AtualizarNome(string Nome) => this.Nome = Nome;
        public void AtualizarToken(Guid Token) => this.Token = Token;
        public void AtualizarPadrao(bool Padrao) => this.Padrao = Padrao;
        public void AtualizarPadraoUnico(string PadraoUnico) => this.PadraoUnico = PadraoUnico;
        public void AtualizarAspNetUsers(int AspNetUsers) => this.AspNetUsers = AspNetUsers;
        public void AtualizarInclusao(DateTime Inclusao) => this.Inclusao = Inclusao;
        public void AtualizarOrigemImportacao(byte? OrigemImportacao) => this.OrigemImportacao = OrigemImportacao;
        #endregion
    }
}
