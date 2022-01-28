using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebOrdersInfo.Core.DTOs;
using WebOrdersInfo.DAL.Core.Entities;

namespace WebOrdersInfo.Models.ViewModels.Orders
{
    public class EditOrderViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Дата заказа")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Цена")]
        [Range(0, int.MaxValue, ErrorMessage = "Цена должна быть больше нуля")]
        public double Price { get; set; }

        public Guid ClientId { get; set; }

        [Display(Name = "Клиент")]
        public SelectList Clients { get; set; }

        public Guid ManagerId { get; set; }
        [Display(Name = "Менеджер")]
        public SelectList Managers { get; set; }

        public Guid ProductId { get; set; }
        [Display(Name = "Продукт")]
        public SelectList Products { get; set; }
    }
}