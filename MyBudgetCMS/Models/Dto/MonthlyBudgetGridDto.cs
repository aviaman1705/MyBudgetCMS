using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Models.Dto
{
    public class MonthlyBudgetGridDto
    {
        public List<MonthlyBudgetGridItemDto> Budgets { get; set; }
        public decimal TotalSum { get; set; }
    }
}