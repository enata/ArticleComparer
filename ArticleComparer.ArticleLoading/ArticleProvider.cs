using System;
using System.Text.RegularExpressions;
using ArticleComparer.Core.Loading;
using ArticleComparer.Core.Processing;

namespace ArticleComparer.ArticleLoading
{
    public sealed class ArticleProvider : IArticleProvider
    {
        private readonly IHtmlCleaner _cleaner;
        private readonly IHtmlLoader _loader;

        private readonly Regex _siteNameRegex =
            new Regex(
                @"^(http|https|ftp)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&amp;%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{2}))/");

        public ArticleProvider(IHtmlCleaner cleaner, IHtmlLoader loader)
        {
            if (cleaner == null)
                throw new ArgumentNullException("cleaner");
            if (loader == null)
                throw new ArgumentNullException("loader");

            _cleaner = cleaner;
            _loader = loader;
        }

        public IArticle Get(string url)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            string html = _loader.Load(url);
            Match siteNameMatch = _siteNameRegex.Match(url);
            string siteName = siteNameMatch.Success ? siteNameMatch.Captures[0].Value : url;
            IArticle result = _cleaner.Clean(siteName.ToLower(), html);
            return result;
        }
    }
}