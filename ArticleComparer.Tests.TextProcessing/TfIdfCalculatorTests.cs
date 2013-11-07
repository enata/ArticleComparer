using System;
using System.Collections.Generic;
using ArticleComparer.Core.Implementation;
using ArticleComparer.Core.Processing;
using ArticleComparer.Core.Utils;
using ArticleComparer.TextProcessing;
using NUnit.Framework;
using Rhino.Mocks;

namespace ArticleComparer.Tests.TextProcessing
{
    [TestFixture]
    public sealed class TfIdfCalculatorTests
    {
        [Test]
        public void CalculateTest()
        {
            var dfProvider = MockRepository.GenerateStub<IDocumentFrequencyProvider>();
            dfProvider.Stub(dfp => dfp.CorpusSize)
                      .Return(10);
            dfProvider.Stub(dfp => dfp.GeDocumentsWithTokenCount(Arg<IToken>.Is.Anything))
                      .Return(1);
            var calculator = new TfIdfCalculator(dfProvider);
            var token1 = MockRepository.GenerateStub<IToken>();
            token1.Stub(t => t.Text)
                  .Return("мама");
            var token2 = MockRepository.GenerateStub<IToken>();
            token2.Stub(t => t.Text)
                  .Return("мыла");

            Dictionary<IToken, TfIdf> tfidfs = calculator.Calculate(new[] {token1, token2, token2});

            Assert.AreEqual(2, tfidfs.Count);
            Assert.IsTrue(tfidfs[token1].Value.IsAbout(1.279, 0.001));
            Assert.IsTrue(tfidfs[token2].Value.IsAbout(1.705, 0.001));
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CalculateTestNullTokensExc()
        {
            var calculator = new TfIdfCalculator(MockRepository.GenerateStub<IDocumentFrequencyProvider>());

            calculator.Calculate(null);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ConstructorTestExc()
        {
            new TfIdfCalculator(null);
        }
    }
}