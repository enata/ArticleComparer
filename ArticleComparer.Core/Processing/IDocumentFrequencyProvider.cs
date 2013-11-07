namespace ArticleComparer.Core.Processing
{
    public interface IDocumentFrequencyProvider
    {
        int CorpusSize { get; }

        int GeDocumentsWithTokenCount(IToken token);
    }
}