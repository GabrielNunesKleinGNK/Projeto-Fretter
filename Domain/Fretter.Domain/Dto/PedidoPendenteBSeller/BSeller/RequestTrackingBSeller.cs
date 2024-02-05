using System;

namespace Fretter.Domain.Dto.PedidoPendenteBSeller.BSeller
{
    public class RequestTrackingBSeller
    {
        public Parametro parametros { get; set; }
    }

    public class Parametro
    {
        public long? P_ID_TRANSP { get; set; }
        public int? P_ID_FILIAL { get; set; }
        public int? P_ID_CONTRATO { get; set; }
        public string P_STATUS { get; set; }
        public int P_ID_ESTABELECIMENTO { get; set; }
        public string P_DT_INI { get; set; }
        public string P_DT_FIM { get; set; }
    }
}
