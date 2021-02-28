using System.ComponentModel.DataAnnotations;

namespace PremiumCalculator.Application.DTO
{
    public class PremiumCalculatorRequest
    {
        [Required(ErrorMessage = "Age is required.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Birthdate is required.")]
        public string BirthDate { get; set; }

        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; }
    }
}
