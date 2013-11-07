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
    public sealed class CosineSimilarityCalculatorTests
    {
        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CalculateSimilariteArticle1NullExc()
        {
            var calculator = new CosineSimilarityCalculator();
            var article = new ProcessedArticle(new Dictionary<IToken, TfIdf>());

            calculator.CalculateSimilarity(null, article);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CalculateSimilariteArticle2NullExc()
        {
            var calculator = new CosineSimilarityCalculator();
            var article = new ProcessedArticle(new Dictionary<IToken, TfIdf>());

            calculator.CalculateSimilarity(article, null);
        }

        [Test]
        public void CalculateSimilarityTest()
        {
            var calculator = new CosineSimilarityCalculator();
            var token1 = MockRepository.GenerateStub<IToken>();
            token1.Stub(t => t.Text)
                  .Return("a");
            var token2 = MockRepository.GenerateStub<IToken>();
            token2.Stub(t => t.Text)
                  .Return("b");
            var token3 = MockRepository.GenerateStub<IToken>();
            token3.Stub(t => t.Text)
                  .Return("c");
            var bow1 = new Dictionary<IToken, TfIdf>
                {
                    {token1, new TfIdf(0.3, token1)},
                    {token2, new TfIdf(0.1, token2)}
                };
            var bow2 = new Dictionary<IToken, TfIdf>
                {
                    {token2, new TfIdf(0.5, token2)},
                    {token3, new TfIdf(0.2, token3)}
                };
            var processed1 = new ProcessedArticle(bow1);
            var processed2 = new ProcessedArticle(bow2);

            double similarity = calculator.CalculateSimilarity(processed1, processed2);

            Assert.IsTrue(similarity.IsAbout(0.294, 0.001));
        }
    }
}