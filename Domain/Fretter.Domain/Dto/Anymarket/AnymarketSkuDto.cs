using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Fretter.Domain.Dto.Anymarket
{
    public partial class AnymarketSkuDto
    {
        [JsonProperty("links")]
        public List<Link> Links { get; set; }

        [JsonProperty("content")]
        public List<ContentAnymarket> Content { get; set; }

        [JsonProperty("page")]
        public Page Page { get; set; }
    }

    public partial class ContentAnymarket
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("brand")]
        public Brand Brand { get; set; }

        [JsonProperty("height")]
        public double Height { get; set; }

        [JsonProperty("width")]
        public double Width { get; set; }

        [JsonProperty("weight")]
        public double Weight { get; set; }

        [JsonProperty("length")]
        public double Length { get; set; }

        [JsonProperty("priceFactor")]
        public long PriceFactor { get; set; }

        [JsonProperty("calculatedPrice")]
        public bool CalculatedPrice { get; set; }

        [JsonProperty("definitionPriceScope")]
        public string DefinitionPriceScope { get; set; }

        [JsonProperty("hasVariations")]
        public bool HasVariations { get; set; }

        [JsonProperty("isProductActive")]
        public bool IsProductActive { get; set; }

        [JsonProperty("characteristics")]
        public List<Characteristic> Characteristics { get; set; }

        [JsonProperty("images", NullValueHandling = NullValueHandling.Ignore)]
        public List<Image> Images { get; set; }

        [JsonProperty("skus")]
        public List<Skus> Skus { get; set; }

        [JsonProperty("allowAutomaticSkuMarketplaceCreation")]
        public bool AllowAutomaticSkuMarketplaceCreation { get; set; }

        [JsonProperty("gender", NullValueHandling = NullValueHandling.Ignore)]
        public string Gender { get; set; }

        [JsonProperty("origin", NullValueHandling = NullValueHandling.Ignore)]
        public Origin Origin { get; set; }

        [JsonProperty("warrantyTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? WarrantyTime { get; set; }

        [JsonProperty("warrantyText", NullValueHandling = NullValueHandling.Ignore)]
        public string WarrantyText { get; set; }
    }

    public partial class Brand
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("reducedName")]
        public string ReducedName { get; set; }

        [JsonProperty("partnerId")]
        public string PartnerId { get; set; }
    }

    public partial class Category
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }

    public partial class Characteristic
    {
        [JsonProperty("index")]
        public long Index { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public partial class Image
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("index")]
        public long Index { get; set; }

        [JsonProperty("main")]
        public bool Main { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("thumbnailUrl")]
        public Uri ThumbnailUrl { get; set; }

        [JsonProperty("lowResolutionUrl")]
        public Uri LowResolutionUrl { get; set; }

        [JsonProperty("standardUrl")]
        public Uri StandardUrl { get; set; }

        [JsonProperty("originalImage")]
        public Uri OriginalImage { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("standardWidth")]
        public long StandardWidth { get; set; }

        [JsonProperty("standardHeight")]
        public long StandardHeight { get; set; }

        [JsonProperty("originalWidth")]
        public long OriginalWidth { get; set; }

        [JsonProperty("originalHeight")]
        public long OriginalHeight { get; set; }
    }

    public partial class Origin
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public partial class Skus
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("partnerId")]
        public string PartnerId { get; set; }

        [JsonProperty("ean")]
        public string Ean { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("additionalTime")]
        public long AdditionalTime { get; set; }

        [JsonProperty("variations")]
        public List<Variation> Variations { get; set; }

        [JsonProperty("stockLocalId")]
        public long StockLocalId { get; set; }

        [JsonProperty("sellPrice")]
        public decimal SellPrice { get; set; }
    }

    public partial class Variation
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("type")]
        public TypeClass Type { get; set; }
    }

    public partial class TypeClass
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("visualVariation")]
        public bool VisualVariation { get; set; }
    }

    public partial class Link
    {
        [JsonProperty("rel")]
        public string Rel { get; set; }

        [JsonProperty("href")]
        public Uri Href { get; set; }
    }

    public partial class Page
    {
        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("totalElements")]
        public long TotalElements { get; set; }

        [JsonProperty("totalPages")]
        public long TotalPages { get; set; }

        [JsonProperty("number")]
        public long Number { get; set; }
    }
}