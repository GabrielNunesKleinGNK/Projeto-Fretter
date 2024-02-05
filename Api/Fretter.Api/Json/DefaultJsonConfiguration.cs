using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Fretter.Api.Json
{
    public static class DefaultJsonConfiguration
    {
        public static void SetDefaultSerializerSettings(JsonSerializerSettings settings)
        {
            Formatting formatting = Formatting.None;
#if DEBUG
            formatting = Formatting.Indented;
#endif
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.Formatting = formatting;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
