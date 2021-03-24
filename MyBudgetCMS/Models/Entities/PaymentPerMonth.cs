using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Models.Entities
{
    public class PaymentPerMonth
    {
        private DateTime? dateCreated = null;

        [Key]
        public int Id { get; set; }

        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category Category { set; get; }

        public decimal Sum { get; set; }

        public DateTime ActionDate { get; set; }

        public DateTime CreatedDate
        {
            get
            {
                return this.dateCreated.HasValue
                   ? this.dateCreated.Value
                   : DateTime.Now;
            }

            set { this.dateCreated = value; }
        }
    }
}