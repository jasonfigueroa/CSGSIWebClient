using CSGSIWebClient.Models;
using CSGSIWebClient.Utilities;
using CSGSIWebClient.Validators;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSGSIWebClient.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(8, ErrorMessage = "Must be between 3 and 8 charactrers", MinimumLength = 3)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Steam Id is required")]
        [StringLength(100, ErrorMessage = "Steam Id is too long")]
        [SteamId()]
        [Display(Name = "Steam Id")]
        public string SteamId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(12, ErrorMessage = "Must be between 4 and 12 characters", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [StringLength(12, ErrorMessage = "Must be between 4 and 12 characters", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Confirm Password must match Password")]
        public string ConfirmPassword { get; set; }
    }
}
