using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Application.Contracts.CustomerDiscount
{
    public class EditCustomerDiscount : DefineCustomerDiscount
    {
        public long Id { get; set; }
    }
}
