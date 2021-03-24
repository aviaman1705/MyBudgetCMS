using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Validators
{
    public class CustomDateValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime parsed;

                bool valid = DateTime.TryParseExact(value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed);
                if (valid)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("פורמט תאריך לא נכון. dd/MM/yyyy");
                }
            }
            else
            {
                return new ValidationResult("" + validationContext.DisplayName + " הוא שדה חובה");
            }
        }
    }
}