using System.Collections.Immutable;
using System.Linq.Expressions;
using _0_Framework.Application;
using _0_Framework.Domain;
using _0_Framework.Infrastucture;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Infrastucture.EfCore;

namespace ShopManagement.Infrastucture.EFCore.Repository
{
    public class ProductCategoryRepository : RepositoryBase<long,ProductCategory>, IProductCategoryRepository
    {
        private readonly ShopContext _shopContext;

        public ProductCategoryRepository(ShopContext shopContext) : base(shopContext)
        {
            _shopContext = shopContext;
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _shopContext.ProductCategories.Select(x => new ProductCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public EditProductCategory GetDetails(long id)
        {
            return _shopContext.ProductCategories.Select(x => new EditProductCategory
            {
                Name = x.Name,
                Description = x.Description,
                //Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                Id = x.Id
            }).FirstOrDefault(x => x.Id == id);
        }

        public string GetSlugById(long id)
        {
            return _shopContext.ProductCategories.Select(x => new { x.Id, x.Slug }).FirstOrDefault(x => x.Id == id)
                .Slug;
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            //List Or Read
            var query = _shopContext.ProductCategories.Select(x => new ProductCategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToFarsi()

            });
            //Search
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }
            //Sort
            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
