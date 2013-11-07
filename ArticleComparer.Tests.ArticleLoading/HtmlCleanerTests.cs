using System;
using System.Linq;
using ArticleComparer.ArticleLoading;
using ArticleComparer.Core.Loading;
using ArticleComparer.Core.Processing;
using NUnit.Framework;
using Rhino.Mocks;

namespace ArticleComparer.Tests.ArticleLoading
{
    [TestFixture]
    public sealed class HtmlCleanerTests
    {
        [Test]
        public void CleanTest()
        {
            var siteCleaner = MockRepository.GenerateStub<ISiteHtmlCleaner>();
            siteCleaner.Stub(sc => sc.SiteUrl)
                       .Return("url");
            var mockArticle = MockRepository.GenerateStub<IArticle>();
            mockArticle.Stub(a => a.Text)
                       .Return("!");
            siteCleaner.Stub(sc => sc.Clean(Arg<string>.Is.Anything))
                       .Return(mockArticle);
            var cleaner = new HtmlCleaner(new[] {siteCleaner});

            IArticle cleaned = cleaner.Clean("url", string.Empty);

            Assert.AreEqual("!", cleaned.Text);
        }

        [Test]
        public void CleanTestDefaultCleaner()
        {
            var cleaner = new HtmlCleaner(Enumerable.Empty<ISiteHtmlCleaner>());

            IArticle cleaned = cleaner.Clean("url", @"<body>Some text</body>");

            Assert.AreEqual("Some text", cleaned.Text);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CleanTestNullHtmlExc()
        {
            var cleaner = new HtmlCleaner(Enumerable.Empty<ISiteHtmlCleaner>());
            cleaner.Clean(string.Empty, null);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CleanTestNullUrlExc()
        {
            var cleaner = new HtmlCleaner(Enumerable.Empty<ISiteHtmlCleaner>());
            cleaner.Clean(null, string.Empty);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ConstructorNullCleanersExc()
        {
            new HtmlCleaner(null);
        }
    }
}