using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BlogManagement.Application
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleApplication(IArticleRepository articleRepository, IFileUploader fileUploader, IArticleCategoryRepository articleCategoryRepository)
        {
            _articleRepository = articleRepository;
            _fileUploader = fileUploader;
            _articleCategoryRepository = articleCategoryRepository;
        }

        public OperationResult Create(CreateArticle command)
        {
            var operation = new OperationResult();
            if (_articleRepository.Exist(x => x.Title == command.Title))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else
            {
                var slug = command.Slug.Slugify();
                var categorySlu = _articleCategoryRepository.GetSlugBy(command.CategoryId);
                var path = $"{categorySlu}/{slug}";
                var PictureName = _fileUploader.Upload(command.Picture, path);
                var publishDate = command.PublishDate.ToGeorgianDateTime();

                var article = new Article(command.Title, command.ShortDescription,command.Description,PictureName,command.PictureAlt,command.PictureTitle,
                   publishDate, command.Slug,command.Keywords,command.MetaDescription,command.CanonicalAddress,command.CategoryId);
                
                _articleRepository.Create(article);
                _articleRepository.SaveChanges();
                return operation.Succeeded();
            }
        }

        public OperationResult Edit(EditArticle command)
        {
            var operation = new OperationResult();
            var articles = _articleRepository.GetWithCategory(command.Id);
            if (articles == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_articleRepository.Exist(x => x.Title == command.Title && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else
            {
                var slug = command.Slug.Slugify();
                var path = $"{articles.Category.Slug}/{slug}";
                var PictureName = _fileUploader.Upload(command.Picture, path);
                var publishDate = command.PublishDate.ToGeorgianDateTime();

                articles.Edit(command.Title, command.ShortDescription, command.Description, PictureName, command.PictureAlt, command.PictureTitle,
                    publishDate, command.Slug, command.Keywords, command.MetaDescription, command.CanonicalAddress, command.CategoryId);

                _articleRepository.SaveChanges();
                return operation.Succeeded();
            }
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var articles = _articleRepository.Get(id);
            if (articles == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            else
            {
                articles.Remove();
                _articleRepository.SaveChanges();
                return operation.Succeeded();
            }
        }
        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var articles = _articleRepository.Get(id);
            if (articles == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            else
            {
                articles.Restore();
                _articleRepository.SaveChanges();
                return operation.Succeeded();
            }
        }
        public List<ArticleViewModel> SearchModel(ArticleSearchModel searchModel)
        {
            return _articleRepository.Search(searchModel);
        }

        public EditArticle GetDetails(long id)
        {
            return _articleRepository.GetDetails(id);
        }
    }
}
