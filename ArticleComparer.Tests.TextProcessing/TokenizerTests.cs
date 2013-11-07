using System;
using System.Linq;
using ArticleComparer.Core.Processing;
using ArticleComparer.TextProcessing;
using NUnit.Framework;

namespace ArticleComparer.Tests.TextProcessing
{
    [TestFixture]
    public sealed class TokenizerTests
    {
        [Test]
        public void TokenizeTest()
        {
            var tokenizer = new Tokenizer();
            const string text = "Мама, вымой раму!";

            IToken[] tokens = tokenizer.Tokenize(text)
                                       .ToArray();

            Assert.AreEqual(3, tokens.Length);
            Assert.AreEqual("мама", tokens[0].Text);
            Assert.AreEqual("вымой", tokens[1].Text);
            Assert.AreEqual("раму", tokens[2].Text);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void TokenizeTestNullTextExc()
        {
            var tokenizer = new Tokenizer();
            tokenizer.Tokenize(null);
        }
    }
}