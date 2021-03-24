using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Models.Entities
{
    public class TypeOfPayment
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }
    }
}