using System;
using System.ComponentModel.DataAnnotations;

namespace WebOrdersInfo.Models.ViewModels.Clients
{
    public class ClientViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Фамилия клиента")]
        public string Name { get; set; }
    }
}
