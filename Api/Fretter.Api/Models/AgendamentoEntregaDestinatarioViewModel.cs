using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;

namespace Fretter.Api.Models
{
    public class AgendamentoEntregaDestinatarioViewModel : IViewModel<AgendamentoEntregaDestinatario>
    {
        public int Id { get; set; }
        public int? IdEntrega { get; set; }
        public string Nome { get; set; }
        public string CpfCnpj { get; set; }
        public string InscricaoEstadual { get; set; }
        public string DocumentoHash { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string PontoReferencia { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public bool Whatsapp { get; set; }

        public AgendamentoEntregaDestinatario Model()
        {
            return new AgendamentoEntregaDestinatario(Id, IdEntrega, Nome, CpfCnpj, InscricaoEstadual, DocumentoHash, Cep, Logradouro, Numero, Complemento, PontoReferencia,
                Bairro, Cidade, UF, Email, Telefone, Celular, Whatsapp);
        }
    }
}
