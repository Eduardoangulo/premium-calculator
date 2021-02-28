using PremiumCalculator.Application.DTO;

namespace PremiumCalculator.Application.Interface.Repositories
{
    public interface IPremiumCalculatorRepository
    {
        PremiumCalculatorResponse CalculatePremium(PremiumCalculatorDTO calculator);
    }
}
