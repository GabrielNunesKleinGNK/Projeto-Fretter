using Fretter.Domain.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Whirlpool
{
    public class NotfisDTO
    {
        [JsonConverter(typeof(SingleValueArrayConverter<Notfis>))]
        public List<Notfis> notfis { get; set; }
    }    
    public class FreightResponsable
    {
        public string cnpj { get; set; }
        public string name { get; set; }
        public string stateTaxNumber { get; set; }
        public string address { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string state { get; set; }
    }

    public class Header
    {
        public string exchangeIdentification { get; set; }
        public string postingDateTime { get; set; }
        public long cnpjBranch { get; set; }
        public long cnpjCustomer { get; set; }
        public long cnpjCarrier { get; set; }
        public long cnpjShipper { get; set; }
        public string shipperName { get; set; }
        public string stateTaxNumberShipper { get; set; }
        public string addressShipper { get; set; }
        public string cityShipper { get; set; }
        public string postalCodeShipper { get; set; }
        public string stateShipper { get; set; }
        public string dateTimeBoarding { get; set; }
    }

    public class Invoice
    {
        [JsonConverter(typeof(SingleValueArrayConverter<InvoiceData>))]
        public List<InvoiceData> invoice { get; set; }
    }

    public class InvoiceData
    {
        public string routeCode { get; set; }
        public string invoice { get; set; }
        public int serie { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string documentAction { get; set; }
        public int transportationModality { get; set; }
        public int typeFreightCargo { get; set; }
        public int typeCharge { get; set; }
        public string freightCondition { get; set; }
        public string vehiclePlateNumbers { get; set; }
        public string natureProduct { get; set; }
        public string typePacking { get; set; }
        public string volumeQuantity { get; set; }
        public double totalAmount { get; set; }
        public string totalWeightProduct { get; set; }
        public decimal weightVolumesDensity { get; set; }
        public string valueAdvalorem { get; set; }
        public string rateIcms { get; set; }
        public string incidenceIcms { get; set; }
        public string substituicaoTributaria { get; set; }
        public string valueIcms { get; set; }
        public double totalValueFreight { get; set; }
        public string taxJurisdiction { get; set; }
        public double netValueShipping { get; set; }
        public string incidenceIss { get; set; }
        public double valueFreightPaymentShipper { get; set; }
        public int shippingPoint { get; set; }
        public string logisticOperatorCode { get; set; }
        public string knowledgeNumberMlog { get; set; }
        public int knowledgeSerieMlog { get; set; }
        public string shippingNumber { get; set; }
        public double tollValue { get; set; }
        public int tollTicketNumber { get; set; }
        public string incidenceVp { get; set; }
        public string accessKey { get; set; }
        public string deliveryNumber { get; set; }
        public string estimatedDeliveryDate { get; set; }
        public string agency { get; set; }
        public string purchaseOrderNumber { get; set; }
        public string shippingType { get; set; }
        public string shippingCondition { get; set; }
        public string knowledgeMirrorNumberMlog { get; set; }
        public string mirrorAccessKey { get; set; }
        public Products products { get; set; }
        public Redispatching redispatching { get; set; }
        public FreightResponsable freightResponsable { get; set; }
    }
    public class Mdfe
    {
        public string mdfeNumber { get; set; }
        public string driverName { get; set; }
        public string cpfDriver { get; set; }
        public string accessKey { get; set; }
    }
    public class Notfis
    {
        public Header header { get; set; }
        public ShipToParties shipToParties { get; set; }
        public Summary summary { get; set; }
        public Mdfe mdfe { get; set; }
    }
    public class Product
    {
        public string material { get; set; }
        public string volumeQuantity { get; set; }
        public string description { get; set; }
        public string typePacking { get; set; }
        public string danfe { get; set; }
    }
    public class ShipToParties
    {
        [JsonConverter(typeof(SingleValueArrayConverter<ShipToPartyData>))]
        public List<ShipToPartyData> shipToParty { get; set; }
    }
    public class Products
    {
        [JsonConverter(typeof(SingleValueArrayConverter<Product>))]
        public List<Product> product { get; set; }
    }
    public class Redispatching
    {
        public long cnpj { get; set; }
        public string name { get; set; }
        public string stateTaxNumber { get; set; }
        public string address { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string state { get; set; }
    }

    public class ScheduleB2b
    {
        public string scheduleDate { get; set; }
        public string scheduleTime { get; set; }
        public string schedulePassword { get; set; }
        public string notes { get; set; }
    }

    public class ShipToPartyData
    {
        public long cnpj { get; set; }
        public string name { get; set; }
        public long cpf { get; set; }
        public string stateTaxNumber { get; set; }
        public string address { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string cityCode { get; set; }
        public string state { get; set; }
        public int typeOperation { get; set; }
        public int shipToPartyType { get; set; }
        public string comunicationNumber { get; set; }
        public string email { get; set; }
        public string customerId { get; set; }
        public Invoice invoices { get; set; }
        public ScheduleB2b scheduleB2b { get; set; }
        public WithoutScheduleB2b withoutScheduleB2b { get; set; }
    }

    public class Summary
    {
        public string totalValueInvoices { get; set; }
        public string totalWeightInvoices { get; set; }
        public string totalWeightVolumesDensity { get; set; }
        public string totalAmountVolumes { get; set; }
    }

    public class WithoutScheduleB2b
    {
        public string scheduleDate { get; set; }
    }
}
