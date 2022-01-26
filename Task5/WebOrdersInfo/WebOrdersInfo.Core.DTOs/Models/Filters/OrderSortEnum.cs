using System.ComponentModel.DataAnnotations;

namespace WebOrdersInfo.Core.DTOs.Models.Filters
{
    public enum OrderSortEnum
    {
        [Display(Name = "Дата заказа")]
        Date,
        [Display(Name = "Название продукта")]
        Product,
        [Display(Name = "Цена")]
        Price,
        [Display(Name = "Фамилия клиента")]
        Client,
        [Display(Name = "Фамилия менеджера")]
        Manager
    }
}
