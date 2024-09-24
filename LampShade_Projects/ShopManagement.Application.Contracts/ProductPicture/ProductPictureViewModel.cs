using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Application.Contracts.Product;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public class ProductPictureViewModel
    {
        public long Id { get; set; }
        public string Picture { get; set; }
        public string CreationDate { get; set; }
        public long PictureId { get; set; }
        public string Products { get; set; }
        public bool IsRemove { get; set; }
        public List<ProductViewModel> ProductView { get; set; }
    }

}
