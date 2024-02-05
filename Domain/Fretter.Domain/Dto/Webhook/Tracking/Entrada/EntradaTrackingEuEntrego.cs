using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Webhook.Tracking.Entrada
{
    public class Address
    {
        public string address { get; set; }
        public string number { get; set; }
        public string complement { get; set; }
        public string neighborhood { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
    }

    public class Corporate
    {
        public string id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string cnpj { get; set; }
    }

    public class Customer
    {
        public string name { get; set; }
        public string cpf { get; set; }
    }

    public class Deliveryman
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string cpf { get; set; }
        public string licensePlate { get; set; }
    }

    public class Invoice
    {
        public string invoiceNumber { get; set; }
        public string invoiceSeries { get; set; }
        public string invoiceKey { get; set; }
        public string invoiceIssuerDocument { get; set; }
    }

    public class Package
    {
        public string code { get; set; }
        public int itemQuantity { get; set; }
    }

    public class PickupPoint
    {
        public string id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string cnpj { get; set; }
        public Address address { get; set; }
        public Corporate corporate { get; set; }
    }

    public class EntradaTrackingEuEntrego
    {
        public int internalId { get; set; }
        public string orderId { get; set; }
        public List<Package> packages { get; set; }
        public string status { get; set; }
        public string statusLabel { get; set; }
        public DateTime processedAt { get; set; }
        public DateTime createdAt { get; set; }
        public string trackingLink { get; set; }
        public string observation { get; set; }
        public Customer customer { get; set; }
        public Invoice invoice { get; set; }
        public PickupPoint pickupPoint { get; set; }
        public Route route { get; set; }
        public List<string> receiptUrls { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string recipientName { get; set; }
        public string recipientDocument { get; set; }
        public string recipientDocumentType { get; set; }
    }

    public class Route
    {
        public string id { get; set; }
        public int order { get; set; }
        public int distance { get; set; }
        public Deliveryman deliveryman { get; set; }
    }


}
