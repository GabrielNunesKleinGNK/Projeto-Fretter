using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Fretter.Domain.Dto.Vtex
{
    public partial class VtexSkuDto
    {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("ProductId")]
        public long ProductId { get; set; }

        [JsonProperty("NameComplete")]
        public string NameComplete { get; set; }

        [JsonProperty("ComplementName")]
        public string ComplementName { get; set; }

        [JsonProperty("ProductName")]
        public string ProductName { get; set; }

        [JsonProperty("ProductDescription")]
        public string ProductDescription { get; set; }

        [JsonProperty("ProductRefId")]
        public string ProductRefId { get; set; }

        [JsonProperty("TaxCode")]
        public string TaxCode { get; set; }

        [JsonProperty("SkuName")]
        public string SkuName { get; set; }

        [JsonProperty("IsActive")]
        public bool IsActive { get; set; }

        [JsonProperty("IsTransported")]
        public bool IsTransported { get; set; }

        [JsonProperty("IsInventoried")]
        public bool IsInventoried { get; set; }

        [JsonProperty("IsGiftCardRecharge")]
        public bool IsGiftCardRecharge { get; set; }

        [JsonProperty("ImageUrl")]
        public object ImageUrl { get; set; }

        [JsonProperty("DetailUrl")]
        public string DetailUrl { get; set; }

        [JsonProperty("CSCIdentification")]
        public object CscIdentification { get; set; }

        [JsonProperty("BrandId")]
        public long BrandId { get; set; }

        [JsonProperty("BrandName")]
        public string BrandName { get; set; }

        [JsonProperty("IsBrandActive")]
        public bool IsBrandActive { get; set; }

        [JsonProperty("Dimension")]
        public Dimension Dimension { get; set; }

        [JsonProperty("RealDimension")]
        public RealDimension RealDimension { get; set; }

        [JsonProperty("ManufacturerCode")]
        public string ManufacturerCode { get; set; }

        [JsonProperty("IsKit")]
        public bool IsKit { get; set; }

        [JsonProperty("KitItems")]
        public List<object> KitItems { get; set; }

        [JsonProperty("Services")]
        public List<object> Services { get; set; }

        [JsonProperty("Categories")]
        public List<object> Categories { get; set; }

        [JsonProperty("CategoriesFullPath")]
        public List<string> CategoriesFullPath { get; set; }

        [JsonProperty("Attachments")]
        public List<object> Attachments { get; set; }

        [JsonProperty("Collections")]
        public List<object> Collections { get; set; }

        [JsonProperty("SkuSellers")]
        public List<SkuSeller> SkuSellers { get; set; }

        public List<SkuSellerComplete> SkuSellersComplete { get; set; }

        [JsonProperty("SalesChannels")]
        public List<long> SalesChannels { get; set; }

        [JsonProperty("Images")]
        public List<object> Images { get; set; }

        [JsonProperty("Videos")]
        public List<object> Videos { get; set; }

        [JsonProperty("SkuSpecifications")]
        public List<object> SkuSpecifications { get; set; }

        [JsonProperty("ProductSpecifications")]
        public List<object> ProductSpecifications { get; set; }

        [JsonProperty("ProductClustersIds")]
        public string ProductClustersIds { get; set; }

        [JsonProperty("PositionsInClusters")]
        public PositionsInClusters PositionsInClusters { get; set; }

        [JsonProperty("ProductClusterNames")]
        public ProductClusterNames ProductClusterNames { get; set; }

        [JsonProperty("ProductClusterHighlights")]
        public AlternateIds ProductClusterHighlights { get; set; }

        [JsonProperty("ProductCategoryIds")]
        public string ProductCategoryIds { get; set; }

        [JsonProperty("IsDirectCategoryActive")]
        public bool IsDirectCategoryActive { get; set; }

        [JsonProperty("ProductGlobalCategoryId")]
        public string ProductGlobalCategoryId { get; set; }

        [JsonProperty("ProductCategories")]
        public Dictionary<string, string> ProductCategories { get; set; }

        [JsonProperty("CommercialConditionId")]
        public long CommercialConditionId { get; set; }

        [JsonProperty("RewardValue")]
        public double RewardValue { get; set; }

        [JsonProperty("AlternateIds")]
        public AlternateIds AlternateIds { get; set; }

        [JsonProperty("AlternateIdValues")]
        public List<object> AlternateIdValues { get; set; }

        [JsonProperty("EstimatedDateArrival")]
        public object EstimatedDateArrival { get; set; }

        [JsonProperty("MeasurementUnit")]
        public string MeasurementUnit { get; set; }

        [JsonProperty("UnitMultiplier")]
        public long UnitMultiplier { get; set; }

        [JsonProperty("InformationSource")]
        public string InformationSource { get; set; }

        [JsonProperty("ModalType")]
        public object ModalType { get; set; }

        [JsonProperty("KeyWords")]
        public string KeyWords { get; set; }

        [JsonProperty("ReleaseDate")]
        public DateTimeOffset ReleaseDate { get; set; }

        [JsonProperty("ProductIsVisible")]
        public bool ProductIsVisible { get; set; }

        [JsonProperty("ShowIfNotAvailable")]
        public bool ShowIfNotAvailable { get; set; }

        [JsonProperty("IsProductActive")]
        public bool IsProductActive { get; set; }

        [JsonProperty("ProductFinalScore")]
        public long ProductFinalScore { get; set; }
    }

    public partial class AlternateIds
    {
    }

    public partial class Dimension
    {
        [JsonProperty("cubicweight")]
        public double Cubicweight { get; set; }

        [JsonProperty("height")]
        public double Height { get; set; }

        [JsonProperty("length")]
        public double Length { get; set; }

        [JsonProperty("weight")]
        public double Weight { get; set; }

        [JsonProperty("width")]
        public double Width { get; set; }
    }

    public partial class PositionsInClusters
    {
        [JsonProperty("141")]
        public long The141 { get; set; }
    }

    public partial class ProductClusterNames
    {
        [JsonProperty("141")]
        public string The141 { get; set; }
    }

    public partial class RealDimension
    {
        [JsonProperty("realCubicWeight")]
        public double RealCubicWeight { get; set; }

        [JsonProperty("realHeight")]
        public double RealHeight { get; set; }

        [JsonProperty("realLength")]
        public double RealLength { get; set; }

        [JsonProperty("realWeight")]
        public double RealWeight { get; set; }

        [JsonProperty("realWidth")]
        public double RealWidth { get; set; }
    }

    public partial class SkuSeller
    {
        [JsonProperty("SellerId")]
        public string SellerId { get; set; }

        [JsonProperty("StockKeepingUnitId")]
        public long StockKeepingUnitId { get; set; }

        [JsonProperty("SellerStockKeepingUnitId")]
        public long SellerStockKeepingUnitId { get; set; }

        [JsonProperty("IsActive")]
        public bool IsActive { get; set; }

        [JsonProperty("FreightCommissionPercentage")]
        public long FreightCommissionPercentage { get; set; }

        [JsonProperty("ProductCommissionPercentage")]
        public long ProductCommissionPercentage { get; set; }
    }

    public partial class SkuSellerComplete
    {
        [JsonProperty("SellerId")]
        public string SellerId { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("ExchangeReturnPolicy")]
        public string ExchangeReturnPolicy { get; set; }

        [JsonProperty("DeliveryPolicy")]
        public string DeliveryPolicy { get; set; }

        [JsonProperty("UseHybridPaymentOptions")]
        public object UseHybridPaymentOptions { get; set; }

        [JsonProperty("UserName")]
        public object UserName { get; set; }

        [JsonProperty("Password")]
        public object Password { get; set; }

        [JsonProperty("SecutityPrivacyPolicy")]
        public string SecutityPrivacyPolicy { get; set; }

        [JsonProperty("CNPJ")]
        public string Cnpj { get; set; }

        [JsonProperty("CSCIdentification")]
        public string CscIdentification { get; set; }

        [JsonProperty("ArchiveId")]
        public long ArchiveId { get; set; }

        [JsonProperty("UrlLogo")]
        public string UrlLogo { get; set; }

        [JsonProperty("ProductCommissionPercentage")]
        public long ProductCommissionPercentage { get; set; }

        [JsonProperty("FreightCommissionPercentage")]
        public long FreightCommissionPercentage { get; set; }

        [JsonProperty("CategoryCommissionPercentage")]
        public string CategoryCommissionPercentage { get; set; }

        [JsonProperty("FulfillmentEndpoint")]
        public Uri FulfillmentEndpoint { get; set; }

        [JsonProperty("CatalogSystemEndpoint")]
        public object CatalogSystemEndpoint { get; set; }

        [JsonProperty("IsActive")]
        public bool IsActive { get; set; }

        [JsonProperty("MerchantName")]
        public object MerchantName { get; set; }

        [JsonProperty("FulfillmentSellerId")]
        public object FulfillmentSellerId { get; set; }

        [JsonProperty("SellerType")]
        public long SellerType { get; set; }

        [JsonProperty("IsBetterScope")]
        public bool IsBetterScope { get; set; }

        [JsonProperty("TrustPolicy")]
        public string TrustPolicy { get; set; }
    }
}
