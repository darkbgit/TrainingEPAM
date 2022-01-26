using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebOrdersInfo.Core.DTOs.Models.Filters;

namespace WebOrdersInfo.Models.ViewModels.OrdersFilter
{
    public class OrdersFilterViewModel
    {
        public OrdersFilterViewModel()
        {
            Clients = new List<ClientForFilter>();
            Products = new List<ProductForFilter>();
            Managers = new List<ManagerForFilter>();
        }

        public IList<ClientForFilter> Clients { get; set; }
        public IList<ProductForFilter> Products { get; set; }
        public IList<ManagerForFilter> Managers { get; set; }

        [Display(Name = "с")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DateFrom { get; set; }

        [Display(Name = "по")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DateTo { get; set; }

        [Display(Name = "от")]
        [Range(0, int.MaxValue, ErrorMessage = "Цена должна быть больше либо равна 0")]
        public double? PriceFrom { get; set; }

        [Display(Name = "до")]
        [Range(0, int.MaxValue, ErrorMessage = "Цена должна быть больше либо равна 0")]
        public double? PriceTo { get; set; }
        public bool IsClear { get; set; }

        [Display(Name = "Сортировать по")]
        public OrderSortEnum OrderBy { get; set; }
    }
}