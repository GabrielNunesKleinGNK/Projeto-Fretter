namespace Fretter.Domain.Entities
{
    public class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string refresh_token { get; set; }
    }
}
