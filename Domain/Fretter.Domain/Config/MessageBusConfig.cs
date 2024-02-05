using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Config
{
    public class MessageBusConfig
    {
        public MessageBusConfig()
        {
        }

        public string ConnectionString { get; set; }
        public int SecondsToTimeout { get; set; }
        public int PreFetchCount { get; set; }
        public int PreFetchCountPedidoPendenteBatimento { get; set; }
        public int ConsumeCount { get; set; }
        public string EntregaUpdateQueue { get; set; }
        public string EntregaStageCallbackQueue { get; set; }
        public string EntregaQueue { get; set; }        
        public string EntregaIncidenteTopic { get; set; }
        public string EtiquetaStageShipNTopic { get; set; }
        public string EntregaConciliacaoTopic { get; set; }
        public string EntregaConciliacaoQueue { get; set; }
        public string EntregaIncidenteQueue { get; set; }
        public string EntregaDevolucaoCallbackQueue { get; set; }
        public string PedidoPendenteBatimentoQueue { get; set; }
        public string EntregaStageReciclagemEtiquetaTopic { get; set; }
        public string TrackingPadraoTopic { get; set; }
        public string TrackingEspecificoTopic { get; set; }
        public string WebhookSyncTopic { get; set; }
        public string EntregaMiraklOndetahTopic { get; set; }
        public string TrackingIntegracaoInsucessoQueue { get; set; }

    }

    public class MessageBusShipNConfig
    {
        public MessageBusShipNConfig()
        {
        }

        public string EntregaStageDanfeTopic { get; set; }
        public string EntregaStageTopic { get; set; }
    }
}
