using System;

namespace BlackJack.BusinessLogicLayer.Exceptions
{
    public class LoginException : Exception
    {
        public LoginException(string message)
            : base(message)
        { }
    }
}
