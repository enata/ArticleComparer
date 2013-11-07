using System;
using System.Linq;
using ArticleComparer.Core.Implementation;
using ArticleComparer.Core.Loading;
using ArticleComparer.Core.Processing;
using HtmlAgilityPack;

namespace ArticleComparer.ArticleLoading
{
    public sealed class MirrorHtmlCleaner : ISiteHtmlCleaner
    {
        private const string Site = @"http://www.mirror.co.uk/";

        public string SiteUrl
        {
            get { return Site; }
        }

        public IArticle Clean(string html)
        {
            if (html == null)
                throw new ArgumentNullException("html");

            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            HtmlNode bodyElement = doc.DocumentNode.Descendants("div")
                                      .FirstOrDefault(el => el.GetAttributeValue("class", string.Empty)
                                                              .TrimEnd() == "body");
            if (bodyElement == null)
                throw new InvalidOperationException("Invalid page formatting");
            return new Article(bodyElement.InnerText);
        }
    }
}