using System;

namespace ArticleComparer.ArticleLoading
{
    public sealed class DailyHtmlCleaner : BaseHtmlCleaner
    {
        private const string Site = @"http://www.dailymail.co.uk/";
        private const string Tag = "js-article-text";

        public override string SiteUrl
        {
            get { return Site; }
        }

        protected override string BodyTagId
        {
            get { return Tag; }
        }

        protected override string GetCleanText(string html)
        {
            string result = base.GetCleanText(html);
            const string startRemoveBlock = "PUBLISHED:";
            const string endRemoveBlock = "View  comments";
            result = RemoveFragment(result, startRemoveBlock, endRemoveBlock);
            result = RemoveFragment(result, "DM.has", ";");

            const string borderLineText = "Share or comment on this article";
            int articleLastPosition = result.IndexOf(borderLineText, StringComparison.Ordinal);
            if (articleLastPosition >= 0)
            {
                result = result.Substring(0, articleLastPosition);
            }

            return result.Trim();
        }

        private string RemoveFragment(string txt, string start, string end)
        {
            int startRemoveIndex = txt.IndexOf(start, StringComparison.Ordinal);

            if (startRemoveIndex >= 0)
            {
                int endRemoveIndex = txt.IndexOf(end, startRemoveIndex + start.Length, StringComparison.Ordinal);
                if (endRemoveIndex >= 0)
                {
                    txt = txt.Remove(startRemoveIndex, endRemoveIndex + end.Length - startRemoveIndex);
                }
            }
            return txt;
        }
    }
}