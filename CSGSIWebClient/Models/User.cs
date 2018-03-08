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
        [StringLength(100, ErrorMessage = "A username is required")]
        [Display(Name = "Username")]
        public string username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
    }
}
