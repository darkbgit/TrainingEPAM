using System.ComponentModel.DataAnnotations;

namespace WebOrdersInfo.Core.DTOs.Models.Users
{
    public class TokenRequestModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
