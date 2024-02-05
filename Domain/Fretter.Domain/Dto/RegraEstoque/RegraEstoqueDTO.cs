using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.RegraEstoque
{
    public class RegraEstoqueDTO
    {
		public int id { get; set; }
        public int empresaId { get; set; }
        public int canalIdOrigem { get; set; }
        public int canalIdDestino { get; set; }
        public int? grupoId { get; set; }
        public string skus { get; set; }
        public int? UsuarioCadastro { get; set; }
        public int? UsuarioAlteracao { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public bool? Ativo { get; set; }

    }
}
