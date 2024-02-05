
using System.ComponentModel.DataAnnotations;

namespace Fretter.Domain.Dto.Webhook.Tracking.Entrada
{ 
    public class EntradaTracking
    {
        /// <summary>
        /// Data da Ocorrência
        /// ex.: yyyy-MM-dd HH:mm:ss
        /// </summary>
        [Required]
        public string data { get; set; }

        /// <summary>
        /// Código/Id da Ocorrência
        /// Exemplo de códigos padrão (sem de/para especifico para o transportador.)
        /// Cd_Ocorrencia	Ds_Ocorrencia      
        /// 1  - Entrega Realizada Normalmente,
        /// 27 - Roubo de Carga,               
        /// 78 - Avaria Total,                 
        /// 79 - Avaria Parcial,               
        /// 80 - Extravio Total,               
        /// 81 - Extravio Parcial,             
        /// 2  - Mercadoria Coletada,          
        /// 29 - Em Transferencia,             
        /// 30 - Transferido,                  
        /// 28 - Em Rota de Entrega
        /// </summary>
        [Required]
        public int id { get; set; }

        /// <summary>
        /// CNPJ Transportador
        /// </summary>
        [Required]
        public string cnpjTransportador { get; set; }

        /// <summary>
        /// Codigo DANFE (Nota fiscal eletrônica)
        /// </summary>
        public string danfe { get; set; }

        /// <summary>
        /// CNPJ da Filial
        /// Campo obrigatório caso a danfe não seja preenchida
        /// </summary>
        public string cnpjFilial { get; set; }

        /// <summary>
        /// Nota fiscal
        /// Campo obrigatório caso a danfe não seja preenchida
        /// </summary>
        public string notaFiscal { get; set; }

        /// <summary>
        /// Série nota fiscal
        /// Campo obrigatório caso a danfe não seja preenchida
        /// </summary>
        public string serie { get; set; }

        /// <summary>
        /// Complemento
        /// </summary>
        public string complemento { get; set; }
    }
}
