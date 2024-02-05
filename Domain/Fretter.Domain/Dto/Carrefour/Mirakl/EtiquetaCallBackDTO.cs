using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Carrefour.Mirakl
{
    public class EtiquetaCallBackDTO
    {
        public List<OrderAdditionalField> OrderAdditionalFields { get; set; }
    }

    public partial class OrderAdditionalField
    {
        public string Code { get; set; }
        public List<string> Value { get; set; }
    }
}
