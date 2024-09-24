using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IProductRepository _productRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IFileUploader fileUploader, IProductRepository productRepository)
        {
            _productPictureRepository = productPictureRepository;
            _fileUploader = fileUploader;
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();
            //if (_productPictureRepository.Exist(x => x.Picture.ToString() == command.Picture.ToString() && x.ProductId == command.ProductId))
            //{
            //   return operation.Failed(ApplicationMessages.DuplicatedRecord);
            //}

            var product = _productRepository.GetProductWithCategory(command.ProductId);

            var path = $"{product.Category.Slug}//{product.Slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);
            var productPicture = new ProductPicture(picturePath, command.PictureAlt, command.PictureTitle,
                    command.ProductId);
                
                _productPictureRepository.Create(productPicture);
                _productPictureRepository.SaveChanges();
                 return operation.Succeeded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.GetWithProductAndCategory(command.Id);
            if (productPicture == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            //if (_productPictureRepository.Exist(x => x.Picture.ToString() == command.Picture.ToString() && x.ProductId != command.ProductId))
            //{
            //    return operation.Failed(ApplicationMessages.DuplicatedRecord);
            //}

            var path = $"{productPicture.Products.Category.Slug}//{productPicture.Products.Slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);
            productPicture.Edit(picturePath, command.PictureAlt, command.PictureTitle,command.ProductId);
                
                _productPictureRepository.SaveChanges();
                return operation.Succeeded();

        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            else
            {
                productPicture.Remove();
                _productPictureRepository.SaveChanges();
                return operation.Succeeded();
            }
        }
        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            else
            {
                productPicture.Restore();
                _productPictureRepository.SaveChanges();
                return operation.Succeeded();
            }
        }

        public EditProductPicture GetDetails(long id)
        {
            return _productPictureRepository.GetDetails(id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }
    }
}
