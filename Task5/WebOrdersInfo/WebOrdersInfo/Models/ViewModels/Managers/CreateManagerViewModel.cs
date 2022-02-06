using System;
using System.ComponentModel.DataAnnotations;

namespace WebOrdersInfo.Models.ViewModels.Managers
{
    public class CreateManagerViewModel
    {
        [Required]
        [Display(Name = "Фамилия менеджера")]
        public string Name { get; set; }
    }
}
