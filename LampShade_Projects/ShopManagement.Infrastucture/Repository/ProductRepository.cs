using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Domain;
using _0_Framework.Infrastucture;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastucture.EfCore;

namespace ShopManagement.Infrastucture.EFCore.Repository
{
    public class ProductRepository : RepositoryBase<long,Product>,IProductRepository
    {
        private readonly ShopContext _shopContext;
        public ProductRepository(ShopContext shopContext) : base(shopContext)
        {
            _shopContext = shopContext;
        }

        public EditProduct GetDetails(long id)
        {
            return _shopContext.Products.Include(x => x.Category)
                .Select(x => new EditProduct
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    //Picture = x.Picture,
                    CategoryId  = x.CategoryId,
                    Code = x.Code,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    ShortDescription = x.ShortDescription
                }).FirstOrDefault(x => x.Id == id);
        }

        public Product GetProductWithCategory(long id)
        {
            return _shopContext.Products
                .Include(x => x.Category)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            //Read
            var query =  _shopContext.Products.Include(x => x.Category)
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category.Name,
                    CategoryId = x.CategoryId,
                    Picture = x.Picture,
                    Code = x.Code,
                    CreationDate = x.CreationDate.ToFarsi()
                });

            //Search And ,Name,Code,Category
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Code))
            {
                query = query.Where(x => x.Code.Contains(searchModel.Code));
            }
            if (searchModel.CategoryId != 0)
            {
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);
            }
            //Sort
            return query.OrderByDescending(x => x.Id).ToList();
        }

        public List<ProductViewModel> GetProducts()
        {
           return _shopContext.Products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}
