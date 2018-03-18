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
    public class ExistingSteamIdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var registerViewModel = (RegisterViewModel)validationContext.ObjectInstance;

            APIMessage apiMessage = APIInterface.IsSteamIdInDb(registerViewModel.SteamId).GetAwaiter().GetResult();

            if (apiMessage.Message == "User with that steam id already exists.")
            {
                return new ValidationResult("That steam id is currently registered to another user");
            }

            return ValidationResult.Success;
        }
    }
}
