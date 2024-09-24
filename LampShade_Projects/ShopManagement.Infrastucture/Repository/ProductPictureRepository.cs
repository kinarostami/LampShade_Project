using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Domain;
using _0_Framework.Infrastucture;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastucture.EfCore;

namespace ShopManagement.Infrastucture.EFCore.Repository
{
    public class ProductPictureRepository : RepositoryBase<long,ProductPicture>,IProductPictureRepository
    {
        private readonly ShopContext _shopContext;

        public ProductPictureRepository(ShopContext shopContext) : base(shopContext)
        {
            _shopContext = shopContext;
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query = _shopContext.ProductPictures.Include(x => x.Products)
                .Select(x => new ProductPictureViewModel
                {
                    Id = x.Id,
                    Picture = x.Picture,
                    PictureId = x.ProductId,
                    Products = x.Products.Name,
                    IsRemove = x.IsRemoved,
                    CreationDate = x.CreationDate.ToFarsi()
                });

            if (searchModel.ProductId != 0)
            {
                query = query.Where(x => x.PictureId == searchModel.ProductId);
            }
            return query.OrderByDescending(x => x.Id).ToList();
        }

        public ProductPicture GetWithProductAndCategory(long id)
        {
            return _shopContext.ProductPictures
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .FirstOrDefault(x => x.Id == id);
        }

        public EditProductPicture GetDetails(long id)
        {
            return _shopContext.ProductPictures.Include(x => x.Products)
                .Select(x => new EditProductPicture
                {
                    Id = x.Id,
                    //Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    ProductId = x.ProductId

                }).FirstOrDefault(x => x.Id == id);
        }
    }
}
