using CSGSIWebClient.Data;
using CSGSIWebClient.Models;
using CSGSIWebClient.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSGSIWebClient.Validators
{
    public class ExistingUsernameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var registerViewModel = (RegisterViewModel)validationContext.ObjectInstance;

            APIMessage apiMessage = APIInterface.IsUsernameInDb(registerViewModel.Username).GetAwaiter().GetResult();

            if (apiMessage.Message == "User with that username already exists.")
            {
                return new ValidationResult("That username is currently registered to another user");
            }

            return ValidationResult.Success;
        }
    }
}
