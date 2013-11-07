using System;
using ArticleComparer.Core.Implementation;
using ArticleComparer.Core.Loading;
using ArticleComparer.Core.Processing;

namespace ArticleComparer.TextProcessing
{
    public sealed class ArticleComparer : IArticleComparer
    {
        private readonly IArticleProcessor _articleProcessor;
        private readonly IArticleProvider _articleProvider;
        private readonly ISimilarityCalculator _similarityCalculator;

        public ArticleComparer(IArticleProvider articleProvider, ISimilarityCalculator similarityCalculator,
                               IArticleProcessor articleProcessor)
        {
            _articleProvider = articleProvider;
            _similarityCalculator = similarityCalculator;
            _articleProcessor = articleProcessor;
        }

        public double Compare(string url1, string url2)
        {
            if (string.IsNullOrWhiteSpace(url1) || string.IsNullOrWhiteSpace(url2))
                throw new ArgumentException("Invalid URL");

            ProcessedArticle article1 = GetProcessedArticle(url1);
            ProcessedArticle article2 = GetProcessedArticle(url2);

            double result = _similarityCalculator.CalculateSimilarity(article1, article2);
            return result;
        }

        private ProcessedArticle GetProcessedArticle(string url)
        {
            IArticle unprocessedArticle = _articleProvider.Get(url);
            ProcessedArticle result = _articleProcessor.Process(unprocessedArticle);
            return result;
        }
    }
}