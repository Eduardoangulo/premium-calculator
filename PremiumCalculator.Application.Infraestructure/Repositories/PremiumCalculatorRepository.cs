using PremiumCalculator.Application.DTO;
using PremiumCalculator.Application.Interface.Repositories;
using System;
using System.Data;
using System.Linq;
using static PremiumCalculator.Application.Common.Enum;

namespace PremiumCalculator.Application.Infraestructure.Repositories
{
    public class PremiumCalculatorRepository : IPremiumCalculatorRepository
    {
        public PremiumCalculatorResponse CalculatePremium(PremiumCalculatorDTO calculatorDTO)
        {
            DataTable dt = GetPremiumRules();

            var availableStates = (from states in dt.AsEnumerable()
                                   where states.Field<string>(StaticColumns.StateColumn) != StaticStrings.GenericCodeState
                                   select states.Field<string>(StaticColumns.StateColumn)
                            ).Distinct().ToList();

            var availableMonths = (from months in dt.AsEnumerable()
                                   where months.Field<int>(StaticColumns.MonthColumn) != StaticNumbers.GenericCodeMonth
                                   select months.Field<int>(StaticColumns.MonthColumn)
                            ).Distinct().ToList();

            if (availableStates.Contains(calculatorDTO.State))
            {
                if (!availableMonths.Contains(calculatorDTO.NumberOfMonth))
                {
                    calculatorDTO.NumberOfMonth = StaticNumbers.GenericCodeMonth;
                }
            }
            else
            {
                calculatorDTO.State = StaticStrings.GenericCodeState;
                calculatorDTO.NumberOfMonth = StaticNumbers.GenericCodeMonth;
            }

            try
            {
                PremiumCalculatorResponse premiumResponse = new PremiumCalculatorResponse
                {
                    Premium = (from stateMonthsPremium in dt.AsEnumerable()
                               where stateMonthsPremium.Field<string>(StaticColumns.StateColumn) == calculatorDTO.State
                               where stateMonthsPremium.Field<int>(StaticColumns.MinimumAgeColumn) <= calculatorDTO.Age
                               where stateMonthsPremium.Field<int>(StaticColumns.MaximumAgeColumn) >= calculatorDTO.Age
                               where stateMonthsPremium.Field<int>(StaticColumns.MonthColumn) == calculatorDTO.NumberOfMonth
                               select stateMonthsPremium.Field<double>(StaticColumns.PremiumColumn)).ToList().ElementAt(0)
                };

                return premiumResponse;
            }
            catch (Exception)
            {
                throw new Exception(ValidateMessages.RuleValidation);
            }
        }

        private DataTable GetPremiumRules()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(StaticColumns.StateColumn, typeof(string));
            dt.Columns.Add(StaticColumns.MonthColumn, typeof(int));
            dt.Columns.Add(StaticColumns.MinimumAgeColumn, typeof(int));
            dt.Columns.Add(StaticColumns.MaximumAgeColumn, typeof(int));
            dt.Columns.Add(StaticColumns.PremiumColumn, typeof(double));
            dt.Rows.Add(new object[] { StaticData.NyData, StaticData.AugustCode, StaticData.MinimumAgeDefined, StaticData.MiddleAgeDefined, StaticData.PremiumFirst });
            dt.Rows.Add(new object[] { StaticData.NyData, StaticData.JanuaryCode, StaticData.MiddleSecondAgeDefined, StaticData.HighAgeDefined, StaticData.PremiumSecond });
            dt.Rows.Add(new object[] { StaticData.AlData, StaticData.NovemberCode, StaticData.MinimumAgeDefined, StaticData.HighAgeDefined, StaticData.PremiumThird });
            dt.Rows.Add(new object[] { StaticData.AkData, StaticData.DecemberCode, StaticData.HighAgeDefined, StaticData.MaximumAgeDefined, StaticData.PremiumFourth });
            dt.Rows.Add(new object[] { StaticData.AkData, StaticData.DecemberCode, StaticData.MinimumAgeDefined, StaticData.HighSecondAgeDefined, StaticData.PremiumFifth });
            dt.Rows.Add(new object[] { StaticData.NyData, StaticNumbers.GenericCodeMonth, StaticData.MinimumAgeDefined, StaticData.HighAgeDefined, StaticData.PremiumSixth });
            dt.Rows.Add(new object[] { StaticData.AlData, StaticNumbers.GenericCodeMonth, StaticData.MinimumAgeDefined, StaticData.HighAgeDefined, StaticData.PremiumSeventh });
            dt.Rows.Add(new object[] { StaticData.AkData, StaticNumbers.GenericCodeMonth, StaticData.MinimumAgeDefined, StaticData.HighAgeDefined, StaticData.PremiumEighth });
            dt.Rows.Add(new object[] { StaticStrings.GenericCodeState, StaticNumbers.GenericCodeMonth, StaticData.MinimumAgeDefined, StaticData.HighAgeDefined, StaticData.PremiumNinth });

            return dt;
        }
    }
}
