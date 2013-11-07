using System.Net;
using ArticleComparer.Core.Loading;

namespace ArticleComparer.ArticleLoading
{
    public sealed class HtmlLoader : IHtmlLoader
    {
        public string Load(string url)
        {
            using (var client = new WebClient())
            {
                string html = client.DownloadString(url);
                return html;
            }
        }
    }
}