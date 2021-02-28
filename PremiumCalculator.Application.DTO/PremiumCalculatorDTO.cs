using System;

namespace PremiumCalculator.Application.DTO
{
    public class PremiumCalculatorDTO
    {
        public int Age { get; set; }

        public string BirthDate { get; set; }

        public string State { get; set; }

        public DateTime BirthDateFormatted { get; set; }

        public int NumberOfMonth { get; set; }
    }
}
