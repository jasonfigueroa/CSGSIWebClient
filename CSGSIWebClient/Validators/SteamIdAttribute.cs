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
    public class SteamIdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var id = (RegisterViewModel)validationContext.ObjectInstance;

            List<SteamPlayer> playerList = SteamApiInterface.GetSteamPlayers(new SteamId { steam_id = id.SteamId });

            if (playerList.Count == 0)
            {
                return new ValidationResult("Please make sure you provide a valid steam id");
            }

            return ValidationResult.Success;
        }
    }
}
