using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Models.Dto
{
    public class Sentence
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
    }
}