using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CSGSIWebClient.Models
{
    public class User
    {
        [Required]
        [StringLength(100, ErrorMessage = "The provided username is too long")]
        [Display(Name = "Username")]
        [JsonProperty("username")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The provided password is too long")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
