using System;
using System.ComponentModel.DataAnnotations;

namespace WebOrdersInfo.Models.ViewModels.Orders
{
    public class OrderViewModel
    {
        [Display(Name = "N")]
        public int Id { get; set; }

        [Display(Name = "Дата заказа")]
        public DateTime Date { get; set; }

        [Display(Name = "Товар")]
        public string ProductName { get; set; }

        [Display(Name = "Цена")]
        public double Price { get; set; }

        [Display(Name = "Клиент")]
        public string ClientName { get; set; }

        [Display(Name = "Менеджер")]
        public string ManagerName { get; set; }
    }
}