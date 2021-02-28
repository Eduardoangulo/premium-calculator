namespace PremiumCalculator.Application.Common
{
    public class Enum
    {
        public struct StaticNumbers
        {
            public const int NumberOne = 1;
            public const int StateNumberOfCharacters = 2;
            public const int GenericCodeMonth = 0;
        }

        public struct Formats
        {
            public const string DateFormatValid = "MM/dd/yyyy";
            public const string StateFormat = @"^[a-zA-Z]+$";
        }

        public struct ValidateMessages
        {
            public const string StateQuantityCharacters = "State must has only 2 characters";
            public const string DateFormatInvalid = "Date format not valid, retry using";
            public const string AgeNotCorresponding = "Age is not corresponding to birthdate";
            public const string StateInvalidCharacters = "State has invalid characters";
            public const string RuleValidation = "There is no functional rule defined for the case";
            public const string AgeOutOfRange = "Age is not corresponding to a valid range";
        }

        public struct StaticStrings
        {
            public const string GenericCodeState = "*";
        }

        public struct StaticColumns
        {
            public const string StateColumn = "STATE";
            public const string MonthColumn = "MONTH";
            public const string MinimumAgeColumn = "MINIMUM_AGE";
            public const string MaximumAgeColumn = "MAXIMUM_AGE";
            public const string PremiumColumn = "PREMIUM";
        }

        public struct StaticData
        {
            public const string NyData = "NY";
            public const string AlData = "AL";
            public const string AkData = "AK";
            public const int JanuaryCode = 1;
            public const int AugustCode = 8;
            public const int NovemberCode = 11;
            public const int DecemberCode = 12;
            public const int MinimumAgeDefined = 18;
            public const int MaximumAgeDefined = 100;
            public const int MiddleAgeDefined = 45;
            public const int MiddleSecondAgeDefined = 46;
            public const int HighAgeDefined = 65;
            public const int HighSecondAgeDefined = 64;

            public const double PremiumFirst = 150;
            public const double PremiumSecond = 200.5;
            public const double PremiumThird = 85.5;
            public const double PremiumFourth = 175.2;
            public const double PremiumFifth = 125.16;
            public const double PremiumSixth = 120.99;
            public const double PremiumSeventh = 100;
            public const double PremiumEighth = 100.8;
            public const double PremiumNinth = 90;
        }
    }
}
