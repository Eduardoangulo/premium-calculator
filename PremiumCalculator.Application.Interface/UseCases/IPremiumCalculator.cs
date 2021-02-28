using PremiumCalculator.Application.DTO;

namespace PremiumCalculator.Application.Interface.UseCases
{
    public interface IPremiumCalculator
    {
        PremiumCalculatorResponse CalculatePremium(PremiumCalculatorRequest calculator);
    }
}
