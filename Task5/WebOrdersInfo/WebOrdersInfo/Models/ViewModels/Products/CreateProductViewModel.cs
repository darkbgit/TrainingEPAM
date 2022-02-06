using System.ComponentModel.DataAnnotations;

namespace WebOrdersInfo.Models.ViewModels.Products
{
    public class CreateProductViewModel
    {
        [Required]
        [Display(Name = "Название продукта")]
        public string Name { get; set; }
    }
}
