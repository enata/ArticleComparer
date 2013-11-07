using System;
using System.Collections.Generic;
using System.Linq;
using ArticleComparer.Core.Loading;
using ArticleComparer.Core.Processing;

namespace ArticleComparer.ArticleLoading
{
    public sealed class HtmlCleaner : IHtmlCleaner
    {
        private readonly ISiteHtmlCleaner _defaultCleaner = new DefaultHtmlCleaner();
        private readonly Dictionary<string, ISiteHtmlCleaner> _siteCleaners;

        public HtmlCleaner(IEnumerable<ISiteHtmlCleaner> siteCleaners)
        {
            if (siteCleaners == null)
                throw new ArgumentNullException("siteCleaners");

            _siteCleaners = siteCleaners.ToDictionary(sc => sc.SiteUrl);
        }

        public IArticle Clean(string url, string html)
        {
            if (url == null || html == null)
                throw new ArgumentNullException();

            ISiteHtmlCleaner cleaner = _siteCleaners.ContainsKey(url) ? _siteCleaners[url] : _defaultCleaner;
            IArticle result = cleaner.Clean(html);
            return result;
        }
    }
}