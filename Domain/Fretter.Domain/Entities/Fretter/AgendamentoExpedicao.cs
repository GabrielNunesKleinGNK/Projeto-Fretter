using Fretter.Domain.Dto.Dashboard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Entities.Fretter
{
    public class AgendamentoExpedicao : EntityBase
    {
        public AgendamentoExpedicao(int id, int empresaId, int? canalId, int? transportadorId, int? transportadorCnpjId, bool expedicaoAutomatica, byte prazoComercial, DateTime dataCadastro)
        {
            Id = id;
            AtualizarEmpresaId(empresaId);
            CanalId = canalId;
            TransportadorId = transportadorId;
            TransportadorCnpjId = transportadorCnpjId;
            ExpedicaoAutomatica = expedicaoAutomatica;
            PrazoComercial = prazoComercial;
            DataCadastro = dataCadastro;
        }

        public int EmpresaId {get; set;}
        public int? CanalId { get; set; }
        public int? TransportadorId { get; set;}
        public int? TransportadorCnpjId { get; set;}
        public bool ExpedicaoAutomatica {get; set;}
        public byte PrazoComercial { get; set; }

        public virtual Canal Canal { get; set; }
        public virtual Fusion.Transportador Transportador { get; set; }
        public virtual Fusion.TransportadorCnpj TransportadorCnpj { get; set; }


        public void AtualizarEmpresaId(int empresaId) => EmpresaId = empresaId;
        public void AtualizarDataAlteracao(DateTime dataAlteracao) => DataAlteracao = dataAlteracao;
        public void AtualizarDataCadastro(DateTime dataCadastro) => DataCadastro = dataCadastro;

    }
}

