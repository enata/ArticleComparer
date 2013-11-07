namespace ArticleComparer.Core.Processing
{
    public interface IArticleComparer
    {
        double Compare(string url1, string url2);
    }
}