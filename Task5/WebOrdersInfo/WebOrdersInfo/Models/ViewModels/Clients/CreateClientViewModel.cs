using System.ComponentModel.DataAnnotations;

namespace WebOrdersInfo.Models.ViewModels.Clients
{
    public class CreateClientViewModel
    {
        [Required]
        [Display(Name = "Фамилия клиента")]
        public string Name { get; set; }
    }
}
