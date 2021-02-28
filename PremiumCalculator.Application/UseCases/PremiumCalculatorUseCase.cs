using PremiumCalculator.Application.DTO;
using PremiumCalculator.Application.Exceptions;
using PremiumCalculator.Application.Interface.Repositories;
using PremiumCalculator.Application.Interface.UseCases;
using System;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using static PremiumCalculator.Application.Common.Enum;

namespace PremiumCalculator.Application.UseCases
{
    public class PremiumCalculatorUseCase : IPremiumCalculator
    {
        private readonly IPremiumCalculatorRepository _premiumCalculatorRepository;

        public PremiumCalculatorUseCase(IPremiumCalculatorRepository premiumCalculatorRepository)
        {
            this._premiumCalculatorRepository = premiumCalculatorRepository;
        }

        public PremiumCalculatorResponse CalculatePremium(PremiumCalculatorRequest calculator)
        {
            try
            {
                ValidatePremiumCalculatorRequest(calculator);

                PremiumCalculatorDTO calculatorDTO = new PremiumCalculatorDTO
                {
                    Age = calculator.Age,
                    BirthDate = calculator.BirthDate,
                    State = calculator.State,
                    BirthDateFormatted = DateTime.ParseExact(calculator.BirthDate, Formats.DateFormatValid, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind)
                };

                calculatorDTO.NumberOfMonth = calculatorDTO.BirthDateFormatted.Month;

                return _premiumCalculatorRepository.CalculatePremium(calculatorDTO);
            }
            catch (Exception e)
            {
                HttpResponseException ex = new HttpResponseException
                {
                    Value = e.Message,
                    Status = (int)HttpStatusCode.InternalServerError
                };
                throw ex;
            }
        }

        private void ValidatePremiumCalculatorRequest(PremiumCalculatorRequest calculator)
        {
            try
            {
                if (calculator.Age < StaticData.MinimumAgeDefined || calculator.Age > StaticData.MaximumAgeDefined)
                    throw new Exception(ValidateMessages.AgeOutOfRange);

                if (!DateTime.TryParseExact(calculator.BirthDate, Formats.DateFormatValid, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out DateTime birthDate))
                    throw new Exception(ValidateMessages.DateFormatInvalid + " " + Formats.DateFormatValid);

                DateTime today = DateTime.Today;
                DateTime zeroTime = new DateTime(StaticNumbers.NumberOne, StaticNumbers.NumberOne, StaticNumbers.NumberOne);
                TimeSpan span = today - birthDate;
                int years = (zeroTime + span).Year - StaticNumbers.NumberOne;
                if (years != calculator.Age)
                    throw new Exception(ValidateMessages.AgeNotCorresponding);

                if (!Regex.IsMatch(calculator.State, Formats.StateFormat))
                    throw new Exception(ValidateMessages.StateInvalidCharacters);

                if (calculator.State.Length > StaticNumbers.StateNumberOfCharacters)
                    throw new Exception(ValidateMessages.StateQuantityCharacters);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
