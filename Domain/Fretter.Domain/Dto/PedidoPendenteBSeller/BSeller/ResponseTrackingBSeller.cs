using System;

namespace Fretter.Domain.Dto.PedidoPendenteBSeller.BSeller
{
    public class ResponseTrackingBSeller
    {
        public string Contrato_Externo { get; set; }
        public string Dt_Emissao { get; set; }
        public string Transp_Nome { get; set; }
        public double Nota { get; set; }

        private string _id_Transportadora { get; set; }
        public string Id_Transportadora
        {
            get { return _id_Transportadora.Replace(".", string.Empty).Split("E")[0]; }
            set
            {
                _id_Transportadora = value;
            }
        }
        public string Serie { get; set; }
        public string Ult_Ponto { get; set; }
        public double Filial { get; set; }
        public string Data_Prometida { get; set; }
        public string Data_Etr { get; set; }
        public string Status { get; set; }
        public string Nome_Ponto { get; set; }
        
        private string _entrega { get; set; }
        public string Entrega
        {
            get { return _entrega.Replace(".", string.Empty).Split("E")[0].PadRight(8,'0'); }
            set
            {
                _entrega = value;
            }
        }
        public double Id_Contrato { get; set; }
        public string Data_Ajustada { get; set; }
        public string Dt_Ult_Ponto { get; set; }
    }
}
