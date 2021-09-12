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
            UserLogin userLogin = (UserLogin)validationContext.ObjectInstance;

            User user = new User()
            {
                username = userLogin.username, password = userLogin.password
            };

            if (!APIInterface.IsValidUser(user))
            {
                return new ValidationResult("Invalid password");
            }

            return ValidationResult.Success;
        }
    }
}
