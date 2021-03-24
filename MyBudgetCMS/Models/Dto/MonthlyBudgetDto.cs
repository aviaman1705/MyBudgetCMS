using MyBudgetCMS.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Models.Dto
{
    public class MonthlyBudgetDto
    {
        public int Id { get; set; }

        [Display(Name = "תקציב")]
        [Required(ErrorMessage = "חובה להזין תקציב")]
        public decimal Budget { get; set; }

        [Display(Name = "תיאור קצר")]
        [Required(ErrorMessage = "חובה להזין תיאור קצר")]
        public string ShortDesc { get; set; }

        [Display(Name = "תאריך")]
        [Required(ErrorMessage = "חובה לבחור תאריך")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }
    }
}