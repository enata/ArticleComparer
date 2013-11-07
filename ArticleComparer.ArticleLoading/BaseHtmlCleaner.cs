using System;
using ArticleComparer.Core.Implementation;
using ArticleComparer.Core.Loading;
using ArticleComparer.Core.Processing;
using HtmlAgilityPack;

namespace ArticleComparer.ArticleLoading
{
    public abstract class BaseHtmlCleaner : ISiteHtmlCleaner
    {
        protected abstract string BodyTagId { get; }
        public abstract string SiteUrl { get; }

        public IArticle Clean(string html)
        {
            if (html == null)
                throw new ArgumentNullException("html");

            string text = GetCleanText(html);

            var result = new Article(text);
            return result;
        }

        protected virtual string GetCleanText(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            HtmlNode bodyElement = doc.GetElementbyId(BodyTagId);

            if (bodyElement == null)
            {
                throw new InvalidOperationException("Incorrect document format");
            }

            string result = bodyElement.InnerText;
            return result.Trim();
        }
    }
}