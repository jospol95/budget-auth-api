using System;

namespace AuthorizationAPI.Application.Exceptions
{
    public class EmailAlreadyRegisteredException : Exception
    {
        public EmailAlreadyRegisteredException()
            :base("There's already an account associated to this email.")
        {
        }
        
        public EmailAlreadyRegisteredException(string message)
            :base(message)
        {
        }
    }
}