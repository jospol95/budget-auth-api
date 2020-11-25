using System.ComponentModel.DataAnnotations;

namespace AuthorizationAPI.Domain.Models
{
    public class AuthenticationType
    {
        [Key]
        public int Id { get; set; }
        public string AuthenticationTypeName { get; set; }
        
        public AuthenticationType()
        {
            // apparently needed by .NET Core
        }
    }
}