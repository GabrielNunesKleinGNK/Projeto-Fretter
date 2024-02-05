using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;

namespace Fretter.Api.Models.Fusion
{
    public class CanalViewModel : IViewModel<Canal>
    {
        public int Id { get; set; }
        public DateTime Inclusao { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public string CanalNome { get; set; }
        public int SegmentoId { get; set; }
        public Int16? OrigemImportacaoId { get; set; }
        public string InscricaoEstadual { get; set; }
        public int EmpresaId { get; set; }

        public Canal Model()
        {
            return new Canal(Id, Inclusao, RazaoSocial, NomeFantasia, Cnpj, CanalNome, SegmentoId, OrigemImportacaoId, EmpresaId);
        }
    }
}
