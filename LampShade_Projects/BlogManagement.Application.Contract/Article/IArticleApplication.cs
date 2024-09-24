using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace BlogManagement.Application.Contract.Article
{
    public interface IArticleApplication
    {
        OperationResult Create(CreateArticle command);
        OperationResult Edit(EditArticle command);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        List<ArticleViewModel> SearchModel(ArticleSearchModel searchModel);
        EditArticle GetDetails(long id);
    }
}
