using System;
using System.Linq;
using ArticleComparer.Core.Implementation;
using ArticleComparer.Core.Processing;

namespace ArticleComparer.TextProcessing
{
    public sealed class CosineSimilarityCalculator : ISimilarityCalculator
    {
        public double CalculateSimilarity(ProcessedArticle article1, ProcessedArticle article2)
        {
            if (article1 == null || article2 == null)
                throw new ArgumentNullException();

            double numerator = 0.0;
            double sqrsum1 = 0.0, sqrsum2 = 0.0;

            foreach (TfIdf tfIdf in article1.BagOfWords.Values)
            {
                double tfidf2 = article2.BagOfWords.ContainsKey(tfIdf.Token)
                                    ? article2.BagOfWords[tfIdf.Token].Value
                                    : 0.0;
                ProcessTfIdfValue(tfIdf.Value, tfidf2, ref numerator, ref sqrsum1, ref sqrsum2);
            }
            foreach (TfIdf tfIdf in article2.BagOfWords.Values.Where(v => !article1.BagOfWords.ContainsKey(v.Token)))
            {
                ProcessTfIdfValue(0.0, tfIdf.Value, ref numerator, ref sqrsum1, ref sqrsum2);
            }

            double similarity = numerator/(Math.Sqrt(sqrsum1)*Math.Sqrt(sqrsum2));
            return similarity;
        }

        private static void ProcessTfIdfValue(double tfIdf, double tfIdf2, ref double numerator, ref double sqrsum1,
                                              ref double sqrsum2)
        {
            numerator += tfIdf*tfIdf2;
            sqrsum1 += Math.Pow(tfIdf, 2);
            sqrsum2 += Math.Pow(tfIdf2, 2);
        }
    }
}