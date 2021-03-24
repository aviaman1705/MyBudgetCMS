using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Models.Dto
{
    public class CategoryGridItemDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ParentName { get; set; }

        public string TypeOfPaymentName { set; get; }

        public int PaymentPerMonthsCount { get; set; }
    }
}