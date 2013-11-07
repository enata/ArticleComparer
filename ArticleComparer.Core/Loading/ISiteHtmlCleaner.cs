using ArticleComparer.Core.Processing;

namespace ArticleComparer.Core.Loading
{
    public interface ISiteHtmlCleaner
    {
        string SiteUrl { get; }

        IArticle Clean(string html);
    }
}