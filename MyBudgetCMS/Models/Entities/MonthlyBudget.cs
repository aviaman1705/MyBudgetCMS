using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Models.Entities
{
    public class MonthlyBudget
    {
        [Key]
        public int Id { get; set; }
        public decimal? Budget { get; set; }
        public string ShortDesc { get; set; }
        public DateTime Date { get; set; }
        
        public string ShortDate()
        {            
            return this.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
    }
}