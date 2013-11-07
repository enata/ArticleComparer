using ArticleComparer.Core.Implementation;
using ArticleComparer.Core.Loading;
using ArticleComparer.Core.Processing;
using HtmlAgilityPack;

namespace ArticleComparer.ArticleLoading
{
    public sealed class DefaultHtmlCleaner : ISiteHtmlCleaner
    {
        public string SiteUrl
        {
            get { return string.Empty; }
        }

        public IArticle Clean(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            string txt = doc.DocumentNode.InnerText.Trim();

            return new Article(txt);
        }
    }
}