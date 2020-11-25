using System;

namespace AuthorizationAPI.Application.Exceptions
{
    public class EmailAndOrPasswordIncorrectException : Exception
    {

        public EmailAndOrPasswordIncorrectException()
            :base("Incorrect email or password.")
        {
        }
        
        public EmailAndOrPasswordIncorrectException(string message)
            :base(message)
        {
        }
    }
}