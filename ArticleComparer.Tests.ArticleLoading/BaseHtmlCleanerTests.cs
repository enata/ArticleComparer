using System;
using System.IO;
using System.Text;
using ArticleComparer.Core.Loading;
using ArticleComparer.Core.Processing;
using NUnit.Framework;

namespace ArticleComparer.Tests.ArticleLoading
{
    public abstract class BaseHtmlCleanerTests
    {
        protected abstract string FileName { get; }
        protected abstract ISiteHtmlCleaner Cleaner { get; }

        [Test]
        public void CleanTest()
        {
            string html = File.ReadAllText(FileName, Encoding.Unicode);
            ;

            IArticle article = Cleaner.Clean(html);

            Assert.AreEqual("Some text", article.Text);
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void CleanTestInvalidFormatExc()
        {
            const string html = @"<p>Some text</p>";

            Cleaner.Clean(html);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CleanTestNullHtmlExc()
        {
            Cleaner.Clean(null);
        }
    }
}