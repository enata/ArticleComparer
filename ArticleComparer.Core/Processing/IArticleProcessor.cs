using ArticleComparer.Core.Implementation;

namespace ArticleComparer.Core.Processing
{
    public interface IArticleProcessor
    {
        ProcessedArticle Process(IArticle article);
    }
}