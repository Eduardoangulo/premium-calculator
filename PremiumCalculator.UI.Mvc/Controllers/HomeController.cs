using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PremiumCalculator.Application.DTO;
using PremiumCalculator.UI.Mvc.Models;

namespace PremiumCalculator.UI.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration Configuration;

        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Calculator()
        {
            var model = new PremiumCalculatorModel();

            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> GetValuePremium(PremiumCalculatorModel premiumCalculatorRequest)
        {
            PremiumCalculatorRequest premiumCalculatorRequestApi = new PremiumCalculatorRequest
            {
                Age = (int)premiumCalculatorRequest.Age,
                BirthDate = premiumCalculatorRequest.DateBirth.ToString(Configuration.GetSection("MySettings").GetSection("BirthDateFormat").Value),
                State = premiumCalculatorRequest.State
            };
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(premiumCalculatorRequestApi), Encoding.UTF8, Configuration.GetSection("MySettings").GetSection("JsonFormat").Value);

                using var response = await httpClient.PostAsync(Configuration.GetSection("MySettings").GetSection("UrlApi").Value, content);
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    PremiumCalculatorResponse responseCalculator = new PremiumCalculatorResponse();
                    responseCalculator = JsonConvert.DeserializeObject<PremiumCalculatorResponse>(apiResponse);
                    premiumCalculatorRequest.PremiumValue = responseCalculator.Premium;
                    premiumCalculatorRequest.IsSuccess = true;
                }
                else
                {
                    premiumCalculatorRequest.ErrorMessage = apiResponse;
                    premiumCalculatorRequest.IsSuccess = false;
                }
            }

            return Json(premiumCalculatorRequest);
        }

        [HttpPost]
        public JsonResult CalculateOtherAmounts(PremiumCalculatorModel premiumCalculatorModel)
        {
            switch (premiumCalculatorModel.FrequencyValue)
            {
                case "M":
                    premiumCalculatorModel.MonthlyValue = premiumCalculatorModel.PremiumValue;
                    premiumCalculatorModel.AnnualValue = premiumCalculatorModel.PremiumValue * 12;
                    break;
                case "Q":
                    premiumCalculatorModel.MonthlyValue = premiumCalculatorModel.PremiumValue / 3;
                    premiumCalculatorModel.AnnualValue = premiumCalculatorModel.PremiumValue * 4;
                    break;
                case "S":
                    premiumCalculatorModel.MonthlyValue = premiumCalculatorModel.PremiumValue / 6;
                    premiumCalculatorModel.AnnualValue = premiumCalculatorModel.PremiumValue * 2;
                    break;
                case "A":
                    premiumCalculatorModel.MonthlyValue = premiumCalculatorModel.PremiumValue / 12;
                    premiumCalculatorModel.AnnualValue = premiumCalculatorModel.PremiumValue;
                    break;
                default:
                    break;
            }

            return Json(premiumCalculatorModel);
        }

    }
}