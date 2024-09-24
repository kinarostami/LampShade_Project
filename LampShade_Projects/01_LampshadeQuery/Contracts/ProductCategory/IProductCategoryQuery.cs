namespace _01_LampshadeQuery.Contracts.ProductCategory;

public interface IProductCategoryQuery
{
    ProductCategoryQueryModel GetProductCategoryWithProductsBy(string slug);
    List<ProductCategoryQueryModel> GetCategories();
    List<ProductCategoryQueryModel> GetProductCategoryWithProduct();
}