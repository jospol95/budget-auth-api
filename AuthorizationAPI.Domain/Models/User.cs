using System.ComponentModel.DataAnnotations;

namespace AuthorizationAPI.Domain.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        private int AuthenticationTypeId { get; set; }
        private AuthenticationType AuthenticationType { get; set; }

        public void GetPasswordHashAndSalt(out byte[] passwordHash, out byte [] passwordSalt )
        {
            passwordHash = PasswordHash;
            passwordSalt = PasswordSalt;
        }

        public User()
        {
            // apparently needed by .NET Core
        }

        public int GetAuthenticationId()
        {
            return AuthenticationTypeId;
        }

        public User(string id, string email, byte[] passwordHash, byte[] passwordSalt, string firstName,
            string lastName, int authenticationTypeId)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            FirstName = firstName;
            LastName = lastName;
            AuthenticationTypeId = authenticationTypeId;
        }
    }
}