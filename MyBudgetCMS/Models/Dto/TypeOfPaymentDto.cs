using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Models.Dto
{
    public class TypeOfPaymentDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }
    }
}