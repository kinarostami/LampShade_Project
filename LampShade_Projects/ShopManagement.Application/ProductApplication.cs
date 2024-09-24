using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository _repository;
        private readonly IProductCategoryRepository _categoryRepository;

        public ProductApplication(IProductRepository repository, IFileUploader fileUploader, IProductCategoryRepository categoryRepository)
        {
            _repository = repository;
            _fileUploader = fileUploader;
            _categoryRepository = categoryRepository;
        }

        public OperationResult Create(CreateProduct entity)
        {
            var operation = new OperationResult();
            if (_repository.Exist(x => x.Name == entity.Name))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else
            {
                var slug = entity.Slug.Slugify();
                var categorySlug = _categoryRepository.GetSlugById(entity.CategoryId);
                var path = $"{categorySlug}//{slug}";
                var pictureName = _fileUploader.Upload(entity.Picture, path);

                var product = new Product(entity.Name, entity.Code, entity.ShortDescription,
                    entity.Description,
                    pictureName, entity.PictureAlt, entity.PictureTitle, slug, entity.Keywords,
                    entity.MetaDescription,entity.CategoryId);

                _repository.Create(product);
                _repository.SaveChanges();
                return operation.Succeeded();
            }
        }

        public OperationResult Edit(EditProduct entity)
        {
            var operation = new OperationResult();
            var product = _repository.GetProductWithCategory(entity.Id);
            if (product == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_repository.Exist(x => x.Name == entity.Name && x.Id != entity.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else
            {
                var slug = entity.Slug.Slugify();
                
                var path = $"{product.Category.Slug}/{slug}";
                var pictureName = _fileUploader.Upload(entity.Picture, path);
                product.Edit(entity.Name, entity.Code, entity.ShortDescription,
                    entity.Description,
                    pictureName, entity.PictureAlt, entity.PictureTitle, slug, entity.Keywords,
                    entity.MetaDescription,entity.CategoryId);
                
                _repository.SaveChanges();
                return operation.Succeeded();
            }
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }

        public EditProduct GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _repository.GetProducts();
        }
    }
}
