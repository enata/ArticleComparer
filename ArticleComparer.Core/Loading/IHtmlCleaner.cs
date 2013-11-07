using ArticleComparer.Core.Processing;

namespace ArticleComparer.Core.Loading
{
    public interface IHtmlCleaner
    {
        IArticle Clean(string url, string html);
    }
}