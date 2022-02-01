using System;
using System.ComponentModel.DataAnnotations;

namespace WebOrdersInfo.Models.ViewModels.Products
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Название продукта")]
        public string Name { get; set; }
    }
}
