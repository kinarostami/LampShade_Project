using BlogManagement.Application.Contract.ArticleCategory;

namespace BlogManagement.Application.Contract.Article;

public class ArticleSearchModel
{
    public string Title { get; set; }
    public long CategoryId { get; set; }
}