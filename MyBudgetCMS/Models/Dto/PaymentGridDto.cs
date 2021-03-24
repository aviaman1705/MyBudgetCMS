using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Models.Dto
{
    public class PaymentGridDto
    {
        public List<PaymentGridItemDto> Expenses { get; set; }

        public decimal TotalSum { get; set; }
    }
}