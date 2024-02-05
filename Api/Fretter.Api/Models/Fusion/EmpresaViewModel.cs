using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models.Fusion
{
    public class EmpresaViewModel : IViewModel<Empresa>
    {
        public int Id { get; set; }
        public DateTime Inclusao { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public Guid TokenId { get; set; }
        public Guid TokenConsultaId { get; set; }
        public bool Marketplace { get; set; }
        public Guid TokenWebHookId { get; set; }
        public bool DesconsideraTransportadorNoRatreio { get; set; }
        public bool? TrocaTransportadorNoRoteiro { get; set; }
        public int? OrigemImportacaoId { get; set; }
        public bool CalculaPrazoEntregaMicroServico { get; set; }
        public bool? NaoUsaEndpointExterno { get; set; }
        public Empresa Model()
        {
            return new Empresa(Id, Inclusao, RazaoSocial, NomeFantasia, Cnpj, TokenId, TokenConsultaId, Marketplace, TokenWebHookId, DesconsideraTransportadorNoRatreio, TrocaTransportadorNoRoteiro, OrigemImportacaoId, CalculaPrazoEntregaMicroServico, NaoUsaEndpointExterno);
        }
    }
}
