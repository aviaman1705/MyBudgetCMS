using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Models.Dto
{
    public class CategorySpentItemDto
    {
        public decimal Sum { get; set; }
        public string CategoryName { get; set; }
    }
}