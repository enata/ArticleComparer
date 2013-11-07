using System;
using ArticleComparer.ArticleLoading;
using ArticleComparer.Core.Loading;
using ArticleComparer.Core.Processing;
using NUnit.Framework;
using Rhino.Mocks;

namespace ArticleComparer.Tests.ArticleLoading
{
    [TestFixture]
    public sealed class ArticleProviderTests
    {
        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ConstructorNullCleanerExc()
        {
            new ArticleProvider(null, MockRepository.GenerateStub<IHtmlLoader>());
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ConstructorNullLoaderExc()
        {
            new ArticleProvider(MockRepository.GenerateStub<IHtmlCleaner>(), null);
        }

        [Test]
        public void GetTest()
        {
            var loader = MockRepository.GenerateStub<IHtmlLoader>();
            loader.Stub(l => l.Load(Arg<String>.Is.Anything))
                  .Return("Uncleaned text");
            var mockArticle = MockRepository.GenerateStub<IArticle>();
            var cleaner = MockRepository.GenerateStub<IHtmlCleaner>();
            cleaner.Stub(c => c.Clean(Arg<string>.Is.Anything, Arg<string>.Is.Anything))
                   .Do(new Func<string, string, IArticle>((url, text) =>
                       {
                           mockArticle.Stub(ma => ma.Text)
                                      .Return(url);
                           return mockArticle;
                       }));
            var provider = new ArticleProvider(cleaner, loader);

            IArticle article =
                provider.Get(
                    @"http://www.dailymail.co.uk/news/article-2489974/An-urban-explorers-holiday-album-Traveller-spends-free-time-visiting-abandoned-buildings-world-despite-arrested-20-times-trespass.html");

            Assert.AreEqual(@"http://www.dailymail.co.uk/", article.Text);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void NullUrlExc()
        {
            var provider = new ArticleProvider(MockRepository.GenerateStub<IHtmlCleaner>(),
                                               MockRepository.GenerateStub<IHtmlLoader>());
            provider.Get(null);
        }
    }
}