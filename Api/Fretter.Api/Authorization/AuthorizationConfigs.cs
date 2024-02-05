using System.Security.Cryptography;

namespace Fretter.Api.Authorization
{
    public class AuthorizationConfigs
    {
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public string AuthUrl { get; set; }
        public string FusionClientId { get; set; }
        public string FusionSecret { get; set; }
        public string FusionUrlRetorno { get; set; }
        public string FusionAuthorizationEndpoint { get; set; }
        public string FusionTokenEndpoint { get; set; }
        public string FusionUserInformationEndpoint { get; set; }
    }

    public class AuthorizationPortalConfigs
    {
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public string AuthUrl { get; set; }
    }
    
    public class TemporaryRsaKey
    {
        public string KeyId { get; set; }
        public RSAParameters Parameters { get; set; }
    }
}
