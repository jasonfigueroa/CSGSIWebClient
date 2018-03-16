using CSGSIWebClient.Data;
using CSGSIWebClient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSGSIWebClient.Validators
{
    public class ValidPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            User user = (User)validationContext.ObjectInstance;

            if (!APIInterface.IsValidUser(user))
            {
                return new ValidationResult("Invalid password");
            }

            return ValidationResult.Success;
        }
    }
}
