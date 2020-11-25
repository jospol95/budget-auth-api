using System.Collections.Generic;

namespace AuthorizationAPI.Infrastructure
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public List<string> Audience { get; set; }
        public string SecretKey { get; set; }
    }
}