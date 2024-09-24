using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Application.Contracts.Product
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct entity);
        OperationResult Edit(EditProduct entity);
        List<ProductViewModel> Search(ProductSearchModel  searchModel);
        EditProduct GetDetails(long id);
        List<ProductViewModel> GetProducts();
    }
}
