using System;
using System.Collections.Generic;
using ArticleComparer.Core.Processing;

namespace ArticleComparer.Core.Implementation
{
    public sealed class ProcessedArticle
    {
        private readonly Dictionary<IToken, TfIdf> _bagOfWords;

        public ProcessedArticle(Dictionary<IToken, TfIdf> bagOfWords)
        {
            if (bagOfWords == null)
                throw new ArgumentNullException("bagOfWords");

            _bagOfWords = bagOfWords;
        }

        public Dictionary<IToken, TfIdf> BagOfWords
        {
            get { return _bagOfWords; }
        }
    }
}