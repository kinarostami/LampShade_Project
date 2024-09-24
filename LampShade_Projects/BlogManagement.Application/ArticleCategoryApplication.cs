using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository, IFileUploader fileUploader)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticleCategory command)
        {
            var operation = new OperationResult();
            if (_articleCategoryRepository.Exist(x => x.Name == command.Name))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else
            {
                var slug = command.Slug.Slugify();
                var pictureName = _fileUploader.Upload(command.Picture, slug);
                
                var articleCategory = new ArticleCategory(command.Name, pictureName,command.PictureAlt,command.PictureTitle, 
                    command.Description, slug, command.Keywords, command.MetaDescription,command.CanonicalAddress,command.ShowOrder);
                
                _articleCategoryRepository.Create(articleCategory);
                _articleCategoryRepository.SaveChanges();
                return operation.Succeeded();
            }
            
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var operation = new OperationResult();
            var category = _articleCategoryRepository.Get(command.Id);
            if (category == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            if (_articleCategoryRepository.Exist(x => x.Name == command.Name && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else
            {
                var slug = command.Slug.Slugify();
                var pictureName = _fileUploader.Upload(command.Picture, slug);

                category.Edit(command.Name, pictureName,command.PictureAlt,command.PictureTitle, command.Description
                    , slug, command.Keywords, command.MetaDescription,command.CanonicalAddress,command.ShowOrder);

                _articleCategoryRepository.SaveChanges();
                return operation.Succeeded();
            }
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _articleCategoryRepository.GetDetails(id);
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _articleCategoryRepository.Search(searchModel);
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            return _articleCategoryRepository.GetArticleCategories();
        }
    }
}
