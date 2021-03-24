using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Models.Dto
{
    public class AddPaymentPerMonthDto
    {
        [Display(Name = "תאריך")]
        [Required(ErrorMessage = "חובה לבחור תאריך")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime ActionDate { get; set; }

        [Display(Name = "קטגוריה")]
        [Required(ErrorMessage = "חובה לבחור קטגוריה")]
        public int CategoryID { get; set; }

        [Display(Name = "סכום")]
        [Required(ErrorMessage = "חובה לבחור סכום")]
        public decimal Sum { get; set; }
    }
}