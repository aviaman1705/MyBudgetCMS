using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Models.Entities
{
    public class BudgetDropdownItem
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public decimal TotalSum { get; set; }
    }
}