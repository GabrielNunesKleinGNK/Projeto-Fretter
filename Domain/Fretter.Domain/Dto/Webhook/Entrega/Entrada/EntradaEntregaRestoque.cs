using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Webhook.Entrega.Entrada
{
    public class Customer
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string cellphone { get; set; }
        public bool is_company { get; set; }
        public string federal_tax_payer_id { get; set; }
        public string state_tax_payer_id { get; set; }
        public string shipping_address { get; set; }
        public string shipping_number { get; set; }
        public string shipping_additional { get; set; }
        public string shipping_reference { get; set; }
        public string shipping_quarter { get; set; }
        public string shipping_city { get; set; }
        public string shipping_state { get; set; }
        public string shipping_zip_code { get; set; }
        public string shipping_country { get; set; }
    }

    public class ShipmentOrderVolumeInvoice
    {
        public string invoice_series { get; set; }
        public string invoice_number { get; set; }
        public string invoice_key { get; set; }
        public DateTime invoice_date { get; set; }
        public decimal? invoice_total_value { get; set; }
        public decimal? invoice_products_value { get; set; }
        public string invoice_cfop { get; set; }
    }

    public class ShipmentOrderVolumeArray
    {
        public string name { get; set; }
        public int shipment_order_volume_number { get; set; }
        public decimal? weight { get; set; }
        public string volume_type_code { get; set; }
        public decimal? width { get; set; }
        public decimal? height { get; set; }
        public decimal? length { get; set; }
        public string products_nature { get; set; }
        public int products_quantity { get; set; }
        public bool is_icms_exempt { get; set; }
        public string tracking_code { get; set; }
        public ShipmentOrderVolumeInvoice shipment_order_volume_invoice { get; set; }
    }

    public class Shipment
    {
        public decimal? customer_shipping_costs { get; set; }
        public string client_id { get; set; }
        public string carrierId { get; set; }
        public string cnpj_transportadora { get; set; }
        public string sales_channel { get; set; }
        public bool scheduled { get; set; }
        public object scheduling_window_start { get; set; }
        public object scheduling_window_end { get; set; }
        public DateTime shipped_date { get; set; }
        public string order_number { get; set; }
        public string shipment_order_type { get; set; }
        public string delivery_method_id { get; set; }
        public DateTime created { get; set; }
        public Customer end_customer { get; set; }
        public string origin_warehouse_code { get; set; }
        public string origin_name { get; set; }
        public string origin_federal_tax_payer_id { get; set; }
        public string origin_customer_phone { get; set; }
        public string origin_customer_email { get; set; }
        public string origin_street { get; set; }
        public string origin_number { get; set; }
        public string origin_additional { get; set; }
        public string origin_reference { get; set; }
        public string origin_zip_code { get; set; }
        public string origin_city { get; set; }
        public string origin_quarter { get; set; }
        public string origin_state_code { get; set; }
        public List<ShipmentOrderVolumeArray> shipment_order_volume_array { get; set; }
        public DateTime estimated_delivery_date { get; set; }
        public DateTime estimated_delivery_date_lp { get; set; }
    }
}
