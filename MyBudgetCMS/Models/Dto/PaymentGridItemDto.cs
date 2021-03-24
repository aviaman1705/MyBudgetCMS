using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Models.Dto
{
    public class PaymentGridItemDto
    {
        public int Id { get; set; }

        [Display(Name = "קטגוריה")]
        public string CategoryName { get; set; }

        public decimal Sum { get; set; }

        public string Date { get; set; }
    }
}