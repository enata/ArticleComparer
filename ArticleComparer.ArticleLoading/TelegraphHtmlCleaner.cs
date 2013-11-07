namespace ArticleComparer.ArticleLoading
{
    public sealed class TelegraphHtmlCleaner : BaseHtmlCleaner
    {
        private const string Site = @"http://www.telegraph.co.uk/";
        private const string Tag = "mainBodyArea";

        public override string SiteUrl
        {
            get { return Site; }
        }

        protected override string BodyTagId
        {
            get { return Tag; }
        }
    }
}