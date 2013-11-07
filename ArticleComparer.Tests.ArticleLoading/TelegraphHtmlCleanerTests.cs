using ArticleComparer.ArticleLoading;
using ArticleComparer.Core.Loading;
using NUnit.Framework;

namespace ArticleComparer.Tests.ArticleLoading
{
    [TestFixture]
    public sealed class TelegraphHtmlCleanerTests : BaseHtmlCleanerTests
    {
        protected override string FileName
        {
            get { return "telegraphTest.txt"; }
        }

        protected override ISiteHtmlCleaner Cleaner
        {
            get { return new TelegraphHtmlCleaner(); }
        }
    }
}