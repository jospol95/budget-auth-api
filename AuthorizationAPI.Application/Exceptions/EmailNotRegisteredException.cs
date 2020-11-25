using System;

namespace AuthorizationAPI.Application.Exceptions
{
    public class EmailNotRegisteredException : Exception
    {
        public EmailNotRegisteredException()
            : base("We couldn't find an account for this email.")
        {
            
        }

        public EmailNotRegisteredException(string message)
        : base(message)
        {
        }
    }
}