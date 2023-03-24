using System.ComponentModel.DataAnnotations;

namespace CSGSIWebClient.Models
{
    public class UserLogin
    {
        [Required]
        [StringLength(100, ErrorMessage = "The provided username is too long")]
        [Display(Name = "Username")]
        public string username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The provided password is too long")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        public bool stayLoggedIn { get; set; } = false;
    }
}
