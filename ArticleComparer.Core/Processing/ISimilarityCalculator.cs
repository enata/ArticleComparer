using ArticleComparer.Core.Implementation;

namespace ArticleComparer.Core.Processing
{
    public interface ISimilarityCalculator
    {
        double CalculateSimilarity(ProcessedArticle article1, ProcessedArticle article2);
    }
}