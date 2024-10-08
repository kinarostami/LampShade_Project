﻿using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using System.ComponentModel.DataAnnotations;

namespace DiscountManagement.Application.Contracts.CustomerDiscount
{
    public class DefineCustomerDiscount
    {
        [Range(1, 100000, ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get; set; }

        [Range(1, 99, ErrorMessage = ValidationMessages.IsRequired)]
        public int DiscountRate { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string StartTime { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string EndTime { get; set; }

        public string Reason { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
