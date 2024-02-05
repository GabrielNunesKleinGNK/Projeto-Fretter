using Fretter.Domain.Dto.PedidoPendenteTransportador.Rodonaves;
using Fretter.Domain.Dto.PedidoPendenteTransportador.SSW;
using Fretter.Domain.Dto.PedidoPendenteTransportador.UxDelivery;
using System;
using System.Linq;
using System.Xml;

namespace Fretter.Domain.Helpers.PedidoPendenteTransportador
{
    public static class PedidoPendenteTransportadorHelper
    {
        public static Entities.Fretter.PedidoPendenteTransportador PopulatePedidoPendenteTransportador(this Entities.Fretter.PedidoPendenteTransportador pedidoPendenteTransportador, 
            ResponseTrackingRodonaves response)
        {

            if (response.Events?.FirstOrDefault() != null)
            {
                var ultimoEvento = response.Events.OrderByDescending(e => e.Date).First();
                AtualizarStatus(pedidoPendenteTransportador, ultimoEvento.Description, ultimoEvento.Date, ultimoEvento.Date);
            }

            return pedidoPendenteTransportador;
        }

        public static Entities.Fretter.PedidoPendenteTransportador PopulatePedidoPendenteTransportador(this Entities.Fretter.PedidoPendenteTransportador pedidoPendenteTransportador, 
            ResponseTrackingSSW response)
        {
            if(response?.Documento?.Tracking?.FirstOrDefault()!= null)
            {
                var ultimoEvento = response.Documento.Tracking.OrderByDescending(e => e.Data_Hora).First();
                AtualizarStatus(pedidoPendenteTransportador, ultimoEvento.Ocorrencia, ultimoEvento.Data_Hora, ultimoEvento.Data_Hora_Efetiva);
                pedidoPendenteTransportador.AtualizarNomeTransportadora(ultimoEvento.Dominio);
            }
            else
                pedidoPendenteTransportador.AtualizarStatusTransportadora(response?.Message);
            
            return pedidoPendenteTransportador;
        }

        public static Entities.Fretter.PedidoPendenteTransportador PopulatePedidoPendenteTransportador(this Entities.Fretter.PedidoPendenteTransportador pedidoPendenteTransportador, 
            ResponseTrackingUxDelivery response)
        {
            if(response?.ListaResultados?.FirstOrDefault() !=null)
            {
                var ultimoEvento = response.ListaResultados.OrderByDescending(e => e.DtOcorrencia).First();
                AtualizarStatus(pedidoPendenteTransportador, ultimoEvento.DescricaoOcorrencia, ultimoEvento.DtOcorrencia, ultimoEvento.DtOcorrencia);
            }
            else
                pedidoPendenteTransportador.AtualizarStatusTransportadora(response?.ListaMensagens?.FirstOrDefault());

            
            return pedidoPendenteTransportador;
        }

        public static Entities.Fretter.PedidoPendenteTransportador PopulatePedidoPendenteTransportador(this Entities.Fretter.PedidoPendenteTransportador pedidoPendenteTransportador,
            XmlDocument xmlRetorno)
        {
            var nodes = xmlRetorno.SelectNodes("//objeto");
            if (nodes != null)
            {
                foreach (XmlNode item in nodes)
                {
                    var sroItem = item.SelectSingleNode("numero")?.InnerText ?? "ERRO";

                    if (sroItem != "ERRO")
                    {
                        var eventos = item.SelectNodes("evento");

                        if (eventos != null && eventos.Count > 0)
                        {
                            XmlNode evento = eventos[0];
                            var data = evento.SelectSingleNode("data")?.InnerText ?? "ERRO";
                            var hora = evento.SelectSingleNode("hora")?.InnerText ?? "ERRO";
                            var descricao = evento.SelectSingleNode("descricao")?.InnerText ?? "ERRO";

                            var dtOcorrencia = Convert.ToDateTime(data + " " + hora);
                            AtualizarStatus(pedidoPendenteTransportador, descricao, dtOcorrencia, dtOcorrencia);
                        }
                    }
                   
                };
            }


            return pedidoPendenteTransportador;
        }

        private static void AtualizarStatus(Entities.Fretter.PedidoPendenteTransportador pedidoPendenteTransportador,
            string description, DateTime dataStatus, DateTime dataAtualizacao)
        {
            pedidoPendenteTransportador.AtualizarStatusTransportadora(description);
            pedidoPendenteTransportador.AtualizarDataStatusTransportadora(dataStatus);
            pedidoPendenteTransportador.AtualizarDataAtualizacaoTransportadora(dataAtualizacao);
            pedidoPendenteTransportador.ProcessadoComSucesso();
        }
    }
}
