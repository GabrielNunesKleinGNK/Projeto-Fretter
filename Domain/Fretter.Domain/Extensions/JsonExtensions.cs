using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Fretter.Domain.Extensions
{
    public static partial class JsonExtensions
    {
        public static JsonDocument JsonDocumentFromObject<TValue>(TValue value, JsonSerializerOptions options = default)
            => JsonDocumentFromObject(value, typeof(TValue), options);

        public static JsonDocument JsonDocumentFromObject(object value, Type type, JsonSerializerOptions options = default)
        {
            var bytes = JsonSerializer.SerializeToUtf8Bytes(value, options);
            return JsonDocument.Parse(bytes);
        }

        public static JsonElement JsonElementFromObject<TValue>(TValue value, JsonSerializerOptions options = default)
            => JsonElementFromObject(value, typeof(TValue), options);

        public static JsonElement JsonElementFromObject(object value, Type type, JsonSerializerOptions options = default)
        {
            using var doc = JsonDocumentFromObject(value, type, options);
            return doc.RootElement.Clone();
        }
    }
}
