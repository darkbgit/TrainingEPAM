using System;
using System.ComponentModel.DataAnnotations;

namespace WebOrdersInfo.Models.ViewModels.Managers
{
    public class ManagerViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Фамилия менеджера")]
        public string Name { get; set; }
    }
}
