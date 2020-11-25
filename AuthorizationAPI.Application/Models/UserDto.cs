namespace AuthorizationAPI.Application.Models
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AuthenticationMethodId { get; set; }
    }
}