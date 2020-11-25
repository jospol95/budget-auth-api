using System;

namespace AuthorizationAPI.Application.Exceptions
{
    public class FieldsMissingException : Exception
    {
        public FieldsMissingException(string message)
            :base(message)
        {
        }
    }
}