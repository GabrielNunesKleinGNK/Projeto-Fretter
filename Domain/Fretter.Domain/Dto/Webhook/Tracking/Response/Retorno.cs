using Fretter.Domain.Helpers.Webhook;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Webhook.Tracking.Response
{
    public class Retorno : RetornoWsErro<ECriticaArquivo>
    {
        public int _arquivoId { get; set; }
        public int _transportadorId { get; set; }
        public byte _idCriticaTipo { get; set; }
        public byte _linha { get; set; }

        /// <summary>
        /// Protocolo
        /// </summary>
        public int protocolo { get; set; }

        /// <summary>
        /// Quantidade de Registros
        /// </summary>
        public int qtd_registros { get; set; }

        /// <summary>
        /// Status de Retrono
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// Registros válidos
        /// </summary>
        public List<ERastreamentoTransportadores> registrosValidos { get; set; }

        /// <summary>
        /// Registros Inválidos
        /// </summary>
        public List<ERastreamentoTransportadores> registrosInvalidos { get; set; }

        public override ECriticaArquivo RetTb()
        {
            return new ECriticaArquivo
            {
                Id_Arquivo = _arquivoId,
                Ds_Erro = referenciaErro,
                Id_Transportador = _transportadorId,
                Id_CriticaTipo = _idCriticaTipo,
                Nr_Linha = _linha
            };
        }
    }
}
