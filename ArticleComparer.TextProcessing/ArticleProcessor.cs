using System;
using System.Collections.Generic;
using ArticleComparer.Core.Implementation;
using ArticleComparer.Core.Processing;

namespace ArticleComparer.TextProcessing
{
    public sealed class ArticleProcessor : IArticleProcessor
    {
        private readonly TfIdfCalculator _tfIdfCalculator;
        private readonly ITokenizer _tokenizer;

        public ArticleProcessor(TfIdfCalculator tfIdfCalculator, ITokenizer tokenizer)
        {
            if (tfIdfCalculator == null || tokenizer == null)
                throw new ArgumentNullException();

            _tfIdfCalculator = tfIdfCalculator;
            _tokenizer = tokenizer;
        }

        public ProcessedArticle Process(IArticle article)
        {
            IEnumerable<IToken> tokenized = _tokenizer.Tokenize(article.Text);
            Dictionary<IToken, TfIdf> tfIdfs = _tfIdfCalculator.Calculate(tokenized);
            return new ProcessedArticle(tfIdfs);
        }
    }
}