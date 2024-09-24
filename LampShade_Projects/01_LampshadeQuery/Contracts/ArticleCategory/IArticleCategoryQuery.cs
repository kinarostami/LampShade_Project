using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_LampshadeQuery.Contracts.Article;

namespace _01_LampshadeQuery.Contracts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        ArticleCategoryQueryModel GetDetails(string slug);
        List<ArticleCategoryQueryModel> GetArticleCategory();
    }
}
