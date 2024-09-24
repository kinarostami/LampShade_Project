using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductCategoryRepository _categoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository categoryRepository, IFileUploader fileUploader)
        {
            _categoryRepository = categoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            if (_categoryRepository.Exist(x => x.Name == command.Name))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else
            {
                var slug = command.Slug.Slugify();

                var picturePath = $"{command.Slug}";
                var pictureName = _fileUploader.Upload(command.Picture, picturePath);
                var product = new ProductCategory(command.Name, command.Description, pictureName, command.PictureAlt
                    , command.PictureTitle, command.Keywords, command.MetaDescription, slug);

                _categoryRepository.Create(product);
                _categoryRepository.SaveChanges();
                return operation.Succeeded();
            }
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();


            var productCategory = _categoryRepository.Get(command.Id);
            if (productCategory == null)
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            if (_categoryRepository.Exist(x => x.Name == command.Name && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else
            {
                var slug = command.Slug.Slugify();

                var picturePath = $"{command.Slug}";
                var fileName = _fileUploader.Upload(command.Picture, picturePath);
                productCategory.Edit(command.Name, command.Description, fileName, command.PictureAlt
                , command.PictureTitle, command.Keywords, command.MetaDescription, slug);

                _categoryRepository.SaveChanges();
                return operation.Succeeded();
            }

        }

        public EditProductCategory GetDetails(long id)
        {
            return _categoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _categoryRepository.Search(searchModel);
        }

        public List<ProductCategoryViewModel> GetProductCategory()
        {
            return _categoryRepository.GetProductCategories();
        }
    }
}
