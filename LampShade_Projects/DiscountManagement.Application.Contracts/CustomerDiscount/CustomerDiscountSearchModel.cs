using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Application.Contracts.CustomerDiscount
{
    public class CustomerDiscountSearchModel
    {
        public long ProductId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
