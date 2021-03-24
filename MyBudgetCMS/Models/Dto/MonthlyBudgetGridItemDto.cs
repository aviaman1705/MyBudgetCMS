using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Models.Dto
{
    public class MonthlyBudgetGridItemDto
    {
        public int Id { get; set; }

        public decimal Budget { get; set; }

        public string Date { get; set; }
        
        public string ShortDesc { get; set; }

        public int PaymentsCount { set; get; }
    }
}