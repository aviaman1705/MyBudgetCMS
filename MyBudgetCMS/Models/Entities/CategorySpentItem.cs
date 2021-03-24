using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Models.Entities
{
    public class CategorySpentItem
    {
        public decimal Sum { get; set; }
        public string CategoryName { get; set; }
    }
}