using System;

namespace PremiumCalculator.Application.Exceptions
{
    public class HttpResponseException : Exception
    {
        public int Status { get; set; }

        public object Value { get; set; }
    }
}
