using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Models.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public int? ParentID { get; set; }

        [ForeignKey("ParentID")]
        public virtual Category Parent { set; get; }

        public int TypeOfPaymentID { get; set; }

        [ForeignKey("TypeOfPaymentID")]
        public virtual TypeOfPayment TypeOfPayment { set; get; }

        public virtual List<PaymentPerMonth> PaymentPerMonths { get; set; }
    }
}