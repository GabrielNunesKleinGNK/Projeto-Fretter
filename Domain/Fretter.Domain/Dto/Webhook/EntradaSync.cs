using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Webhook
{
    public class EntradaSync
    {
        /// <summary>
        /// History
        /// </summary>
        public EntradaSyncHistory history { get; set; }

        /// <summary>
        /// Invoice
        /// </summary>
        public EntradaSyncInvoice invoice { get; set; }

        /// <summary>
        /// Order Number
        /// </summary>
        public string order_number { get; set; }

        /// <summary>
        /// Sales Order Number
        /// </summary>
        public string sales_order_number { get; set; }

        /// <summary>
        /// Tracking Code
        /// </summary>
        public string tracking_code { get; set; }

        /// <summary>
        /// Volume Number
        /// </summary>
        public string volume_number { get; set; }
    }

    /// <summary>
    /// Invoice da Ocorrência Sync
    /// </summary>
    public class EntradaSyncInvoice
    {
        /// <summary>
        /// Invoice Series
        /// </summary>
        public string invoice_series { get; set; }

        /// <summary>
        /// Invoice Number
        /// </summary>
        public string invoice_number { get; set; }

        /// <summary>
        /// Invoice Key
        /// </summary>
        public string invoice_key { get; set; }
    }

    /// <summary>
    /// Histórico da Ocorrência Sync
    /// </summary>
    public class EntradaSyncHistory
    {
        /// <summary>
        /// Volume Id
        /// </summary>
        public long shipment_order_volume_id { get; set; }

        /// <summary>
        /// Volume State
        /// </summary>
        public string shipment_order_volume_state { get; set; }

        /// <summary>
        /// Created
        /// </summary>
        public long created { get; set; }

        /// <summary>
        /// Created Iso
        /// </summary>
        public DateTime created_iso { get; set; }

        /// <summary>
        /// Provider Message
        /// </summary>
        public string provider_message { get; set; }

        /// <summary>
        /// Provider State
        /// </summary>
        public string provider_state { get; set; }

        /// <summary>
        /// Esprintes Message
        /// </summary>
        public string esprinter_message { get; set; }

        /// <summary>
        /// Shipment Volume Micro State
        /// </summary>
        public EntradaSyncShipmentVolume shipment_volume_micro_state { get; set; }

        /// <summary>
        /// Shipment Order Volume State Localized
        /// </summary>
        public string shipment_order_volume_state_localized { get; set; }

        /// <summary>
        /// Shipment Order Volume State History
        /// </summary>
        public long shipment_order_volume_state_history { get; set; }

        /// <summary>
        /// Event Date
        /// </summary>
        public long event_date { get; set; }

        /// <summary>
        /// Event Date Iso
        /// </summary>
        public DateTime? event_date_iso { get; set; }
    }

    /// <summary>
    /// Informações da Entrega Sync
    /// </summary>
    public class EntradaSyncShipmentVolume
    {
        /// <summary>
        /// Id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// Default Name
        /// </summary>
        public string default_name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Shipment Order Volume State Id
        /// </summary>
        public int shipment_order_volume_state_id { get; set; }

        /// <summary>
        /// Shipment Volume State Source Id
        /// </summary>
        public long shipment_volume_state_source_id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string name { get; set; }
    }

}
