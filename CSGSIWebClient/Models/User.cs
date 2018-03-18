using CSGSIWebClient.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSGSIWebClient.Models
{
    public class User
    {
        [Required]
        [StringLength(100, ErrorMessage = "The provided username is too long")]
        [NonExistantUsername()]
        [Display(Name = "Username")]
        public string username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The provided password is too long")]
        [ValidPassword()]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
    }
}
