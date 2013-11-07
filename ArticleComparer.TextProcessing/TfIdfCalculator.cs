using System;
using System.Collections.Generic;
using System.Linq;
using ArticleComparer.Core.Implementation;
using ArticleComparer.Core.Processing;

namespace ArticleComparer.TextProcessing
{
    public sealed class TfIdfCalculator : ITfIdfCalculator
    {
        private readonly IDocumentFrequencyProvider _dfProvider;

        public TfIdfCalculator(IDocumentFrequencyProvider dfProvider)
        {
            if (dfProvider == null)
                throw new ArgumentNullException("dfProvider");

            _dfProvider = dfProvider;
        }

        public Dictionary<IToken, TfIdf> Calculate(IEnumerable<IToken> tokenizedText)
        {
            if (tokenizedText == null)
                throw new ArgumentNullException("tokenizedText");

            Dictionary<IToken, int> groupedTokens = tokenizedText.GroupBy(t => t)
                                                                 .ToDictionary(g => g.Key, g => g.Count());
            int maxFrequency = groupedTokens.Values.Max(v => v);
            var result = new Dictionary<IToken, TfIdf>();
            foreach (var groupedToken in groupedTokens)
            {
                double tf = 0.5 + 0.5*groupedToken.Value/maxFrequency;
                double df = _dfProvider.GeDocumentsWithTokenCount(groupedToken.Key);
                double idf = Math.Log((_dfProvider.CorpusSize + 1.0)/(df + 1.0));
                double tfidfValue = tf*idf;
                var tfidf = new TfIdf(tfidfValue, groupedToken.Key);
                result.Add(tfidf.Token, tfidf);
            }
            return result;
        }
    }
}