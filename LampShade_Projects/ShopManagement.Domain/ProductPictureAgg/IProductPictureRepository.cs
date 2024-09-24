using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository : IRepository<long,ProductPicture>
    {
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
        ProductPicture GetWithProductAndCategory(long id);
        EditProductPicture GetDetails(long id);
    }
}
