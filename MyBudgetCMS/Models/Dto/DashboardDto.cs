using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Models.Dto
{
    public class DashboardDto
    {
        public decimal? Income { get; set; }
        public decimal? expense { get; set; }
        public List<ExpenseDashboardItemDto> FiftyPercentageNeedsList { get; set; }
        public List<ExpenseDashboardItemDto> ThirtyPercentageDesiresList { get; set; }
        public List<ExpenseDashboardItemDto> TwentyPercentageSavingsList { get; set; }
        public decimal? CurrentBudget { get; set; }
        public string Date { get; set; }

        public decimal? MonthlyBudget { get; set; }
    }
}