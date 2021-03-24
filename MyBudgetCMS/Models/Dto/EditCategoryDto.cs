using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Models.Dto
{
    public class EditCategoryDto
    {
        public int Id { get; set; }

        [Display(Name = "כותרת")]
        [Required(ErrorMessage = "חובה להזין כותרת")]
        public string Title { get; set; }

        [Display(Name = "קטגוריית אב")]
        public int? ParentID { get; set; }

        [Display(Name = "סוג הוצאה")]
        [Required(ErrorMessage = "חובה להזין הוצאה")]
        public int TypeOfPaymentID { get; set; }
    }
}