using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Models.Entities
{
    public class Dashboard
    {
        public decimal? Income { get; set; }
        public decimal? expense { get; set; }
        public List<ExpenseDashboardItem> FiftyPercentageNeedsList { get; set; }
        public List<ExpenseDashboardItem> ThirtyPercentageDesiresList { get; set; }
        public List<ExpenseDashboardItem> TwentyPercentageSavingsList { get; set; }
        public decimal? CurrentBudget { get; set; }

        public string GetDate()
        {
            return $"{ CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month) } {DateTime.Now.Year}";
        }

        public decimal? MonthlyBudget { get; set; }
    }
}