using CSGSIWebClient.Data;
using CSGSIWebClient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSGSIWebClient.Validators
{
    public class NonExistantUsernameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            User user = (User)validationContext.ObjectInstance;

            APIMessage apiMessage = APIInterface.IsUsernameInDb(user.username).GetAwaiter().GetResult();

            if (apiMessage.Message == "That username is currently available.")
            {
                return new ValidationResult("That username is not currently found in the database");
            }

            return ValidationResult.Success;
        }
    }
}
