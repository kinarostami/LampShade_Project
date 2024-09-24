using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public class EditProductPicture : CreateProductPicture
    {
        public long Id { get; set; }
    }
}
