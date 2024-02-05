using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Entities.Fusion
{
    public class Transportador : EntityBase
    {
        public Transportador(int Id, string NomeFantasia)
        {
            this.Ativar();
            this.Id = Id;
            this.NomeFantasia = NomeFantasia;
        }
        public Transportador() { }
        public int Id { get; protected set; }
        public DateTime DataInclusao { get; protected set; }
        public string RazaoSocial { get; protected set; }
        public string NomeFantasia { get; protected set; }
        public DateTime? DataValidado { get; protected set; }
        public Guid TokenId { get; protected set; }
        public int? RastreamentoConfigTipoId { get; protected set; }
        public bool CalculoPrazo { get; protected set; }
        public int? OrigemImportacaoId { get; protected set; }
        public bool? Hibrido { get; protected set; }
        public bool Ativo { get; protected set; }
    }
}
