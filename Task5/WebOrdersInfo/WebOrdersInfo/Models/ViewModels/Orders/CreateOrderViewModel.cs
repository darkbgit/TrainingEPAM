using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebOrdersInfo.Core.DTOs;

namespace WebOrdersInfo.Models.ViewModels.Orders
{
    public class CreateOrderViewModel
    {
        [Required]
        [Display(Name = "Дата заказа")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Date { get; set; }

        [Required]
        [Display(Name = "Цена")]
        public double? Price { get; set; }

        [Required]
        public Guid? ClientId { get; set; }

        [Display(Name = "Клиент")]
        public SelectList Clients { get; set; }
        [Required]
        public Guid? ManagerId { get; set; }
        [Display(Name = "Менеджер")]
        public SelectList Managers { get; set; }
        [Required]
        public Guid? ProductId { get; set; }
        [Display(Name = "Продукт")]
        public SelectList Products { get; set; }
    }
}