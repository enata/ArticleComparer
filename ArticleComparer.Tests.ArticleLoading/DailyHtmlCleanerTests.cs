using ArticleComparer.ArticleLoading;
using ArticleComparer.Core.Loading;
using NUnit.Framework;

namespace ArticleComparer.Tests.ArticleLoading
{
    [TestFixture]
    public sealed class DailyHtmlCleanerTests : BaseHtmlCleanerTests
    {
        protected override string FileName
        {
            get { return "dailyTest.txt"; }
        }

        protected override ISiteHtmlCleaner Cleaner
        {
            get { return new DailyHtmlCleaner(); }
        }
    }
}