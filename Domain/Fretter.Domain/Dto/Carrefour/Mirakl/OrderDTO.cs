using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fretter.Domain.Dto.Carrefour.Mirakl
{
    public class ResultMiraklDTO
    {
        public List<OrderDTO> orders { get; set; }
        public int total_count { get; set; }
    }

    public class OrderDTO
    {
        public string order_id { get; set; }
        public string order_state { get; set; }
        public string order_state_clean { get => order_state.Trim().RemoveSpecialCharacters(); }
        public string commercial_id { get; set; }
        public DateTime? created_date { get; set; }
        public decimal price { get; set; }
        public decimal total_price { get; set; }
        public string shop_name { get; set; }
        public OrderDeliveryDateDTO delivery_date { get; set; }
        public DateTime? shipping_deadline { get; set; }
        public decimal shipping_price { get; set; }
        public int? shop_id { get; set; }
        public bool has_incident { get; set; }
        public string shipping_type_code { get; set; }
        public List<OrderLineDTO> order_lines { get; set; }
        public OrderCustomerDTO customer { get; set; }
        public List<OrderAdditionalFieldsDTO> order_additional_fields { get; set; }
        public bool is_envios { get => order_additional_fields.Any(y => y.code == "shipping-priority-name" && y.value.ToUpper() == "ENVIOS"); } 
    }

    public class OrderAdditionalFieldsDTO
    {
        public string code { get; set; }
        public string type { get; set; }
        public string value { get; set; }
    }

    public class OrderLineDTO
    {
        public string category_label { get; set; }
        public string product_title { get; set; }
        public string product_sku { get; set; }
        public string order_line_id { get; set; }
        public int quantity { get; set; }
        public decimal price_unit { get; set; }
        public decimal shipping_price { get; set; }
        public decimal total_price { get; set; }
        public string order_line_state_reason_code { get; set; }
        public string order_line_state_reason_label { get; set; }

        public List<OrderAdditionalFieldsDTO> order_line_additional_fields { get; set; }
    }

    public class OrderCustomerDTO
    {
        public string customer_id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public OrderCustomerAddress shipping_address { get; set; }
        public OrderCustomerAddress billing_address { get; set; }
    }

    public class OrderCustomerAddress
    {
        public string city { get; set; }
        public string zip_code { get; set; }
        public string state { get; set; }
        public string street_1 { get; set; }
        public string street_2 { get; set; }
    }

    public class OrderDeliveryDateDTO
    {
        public DateTime earliest { get; set; }
        public DateTime latest { get; set; }
    }
}
