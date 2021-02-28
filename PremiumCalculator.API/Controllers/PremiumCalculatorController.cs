using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PremiumCalculator.Application.DTO;
using PremiumCalculator.Application.Interface.UseCases;

namespace PremiumCalculator.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PremiumCalculatorController : ControllerBase
    {
        private readonly IPremiumCalculator _calculator;

        public PremiumCalculatorController(IPremiumCalculator calculator)
        {
            _calculator = calculator;
        }

        [HttpPost("calculatePremium")]
        public PremiumCalculatorResponse CalculatePremium(PremiumCalculatorRequest calculator)
        {
            var premium = _calculator.CalculatePremium(calculator);
            return premium;
        }
    }

    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error-local-development")]
        public IActionResult ErrorLocalDevelopment(
            [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message);
        }

        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}