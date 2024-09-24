using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Application.Contracts.CustomerDiscount
{
    public class CustomerDiscountViewModel
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string Product { get; set; }
        public int DiscountRate { get; set; }
        public string StartTime { get; set; }
        public DateTime StartTimeGr { get; set; }
        public string EndTime { get; set; }
        public DateTime EndTimeGr { get; set; }
        public string Reason { get; set; }
        public string CreationDate { get; set; }
    }
}
