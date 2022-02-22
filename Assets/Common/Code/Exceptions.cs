using System;

namespace TatmanGames.Common
{
    /// <summary>
    /// Thrown by ServiceLocator when there is a problem
    /// </summary>
    public class ServiceLocatorException : Exception
    {
        public ServiceLocatorException(string message = "") : base(message)
        {
        }
    }
}