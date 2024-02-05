using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Fretter.Api.Models
{
    public class EntregaDevolucaoViewModel : IViewModel<EntregaDevolucao>
    {
        public int Id { get; set; }
        public int EntregaId { get; set; }
        public int EntregaTransporteTipoId { get; set; }
        public string CodigoColeta { get; set; }
        public string CodigoRastreio { get; set; }
        public DateTime? Validade { get; set; }
        public string Observacao { get; set; }
        public string UltimaOcorrencia { get; set; }
        public DateTime? Inclusao { get; set; }
        public int EntregaDevolucaoStatus { get; set; }
        public bool? Processado { get; set; }
        public bool? Ativo { get; set; }
        public bool? Finalizado { get; set; }
        public string CodigoRetorno { get; set; }
        public string CodigoEntregaSaidaItem { get; set; }
        public EntregaDevolucaoStatusViewModel Status { get; set; }
        public List<EntregaDevolucaoOcorrenciaViewModel> Ocorrencias { get; set; }
        public EntregaViewModel Entrega { get; set; }
        public EntregaViewModel EntregaReversa { get; set; }
        public EntregaDevolucao Model()
        {
            return new EntregaDevolucao(Id, EntregaId, EntregaTransporteTipoId, CodigoColeta,
             CodigoRastreio, Validade, Observacao, Inclusao,
             EntregaDevolucaoStatus, Processado, Finalizado, CodigoRetorno, CodigoEntregaSaidaItem);
        }
    }
}
