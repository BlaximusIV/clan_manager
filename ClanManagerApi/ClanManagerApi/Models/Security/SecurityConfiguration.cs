using System.ComponentModel.DataAnnotations;

namespace ClanManagerApi.Models.Security
{
    public class SecurityConfiguration
    {
        [StringLength(maximumLength: 32, MinimumLength = 32)]
        [Required]
        public string SecurityKey { get; set; }
        [Required]
        public string TokenIssuer { get; set; }
        [Required]
        public string TokenAudience { get; set; }
    }
}
