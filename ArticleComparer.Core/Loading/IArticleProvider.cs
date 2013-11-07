using ArticleComparer.Core.Processing;

namespace ArticleComparer.Core.Loading
{
    public interface IArticleProvider
    {
        IArticle Get(string url);
    }
}