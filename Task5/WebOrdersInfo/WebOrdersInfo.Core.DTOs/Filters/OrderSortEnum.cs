using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebOrdersInfo.Core.DTOs.Filters
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
