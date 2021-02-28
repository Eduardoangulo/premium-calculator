using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PremiumCalculator.UI.Mvc.Models
{
    public class PremiumCalculatorModel
    {
        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "Date of Birth is required")]
        public DateTime DateBirth { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "Age is required")]
        public int? Age { get; set; }

        public double? PremiumValue { get; set; }

        [Display(Name = "Annual")]
        public double? AnnualValue { get; set; }

        [Display(Name = "Monthly")]
        public double? MonthlyValue { get; set; }

        public string FrequencyValue { get; set; }

        public List<SelectListItem> FrequencyList { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "Select" },
            new SelectListItem { Value = "M", Text = "Month" },
            new SelectListItem { Value = "Q", Text = "Quarte" },
            new SelectListItem { Value = "S", Text = "Semi-A"  },
            new SelectListItem { Value = "A", Text = "Annuall"  },
        };

        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }

    }
}
