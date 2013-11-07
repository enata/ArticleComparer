using System;
using ArticleComparer.Core.Processing;
using ArticleComparer.TextProcessing;
using NUnit.Framework;
using Rhino.Mocks;

namespace ArticleComparer.Tests.DocumentFrequencyCollector
{
    [TestFixture]
    public sealed class DocumentFrequencyProviderTest
    {
        [Test]
        public void EmptyProviderTest()
        {
            var provider = new DocumentFrequencyProvider();

            Assert.AreEqual(0, provider.CorpusSize);
        }

        [Test]
        public void ProcessTest()
        {
            var provider = new DocumentFrequencyProvider();
            var token1 = MockRepository.GenerateStub<IToken>();
            token1.Stub(t => t.Text)
                  .Return("мама");
            var token2 = MockRepository.GenerateStub<IToken>();
            token2.Stub(t => t.Text)
                  .Return("мыла");
            var token3 = MockRepository.GenerateStub<IToken>();
            token3.Stub(t => t.Text)
                  .Return("мама");

            provider.ProcessText(new[] {token1, token2, token3});

            Assert.AreEqual(1, provider.CorpusSize);
            Assert.AreEqual(1, provider.GeDocumentsWithTokenCount(token1));
            var testToken = MockRepository.GenerateStub<IToken>();
            testToken.Stub(tt => tt.Text)
                     .Return("кот");
            Assert.AreEqual(0, provider.GeDocumentsWithTokenCount(testToken));
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ProcessTestNullTokensExc()
        {
            var provider = new DocumentFrequencyProvider();

            provider.ProcessText(null);
        }
    }
}