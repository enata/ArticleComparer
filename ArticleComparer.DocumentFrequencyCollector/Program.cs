using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using ArticleComparer.ArticleLoading;
using ArticleComparer.Core.Processing;
using ArticleComparer.TextProcessing;

namespace ArticleComparer.DocumentFrequencyCollector
{
    internal class Program
    {
        private const string ArticlesUrlsFile = "corpusArticles.txt";
        private const string DfContainerFile = "dfContainer";

        private static void Main(string[] args)
        {
            var htmlCleaner = new HtmlCleaner(new[] {new TelegraphHtmlCleaner()});
            var htmlLoader = new HtmlLoader();
            var articleProvider = new ArticleProvider(htmlCleaner, htmlLoader);
            var frequencyProvider = new DocumentFrequencyProvider();
            var tokenizer = new Tokenizer();
            string[] articleUrls = File.ReadAllLines(ArticlesUrlsFile, Encoding.Unicode);
            foreach (string articleUrl in articleUrls)
            {
                IArticle article = articleProvider.Get(articleUrl);
                IEnumerable<IToken> tokens = tokenizer.Tokenize(article.Text);
                frequencyProvider.ProcessText(tokens);
            }

            SaveFrequencies(frequencyProvider);
        }

        private static void SaveFrequencies(DocumentFrequencyProvider frequencyProvider)
        {
            var serializer = new DataContractSerializer(typeof (DocumentFrequencyProvider));
            using (var file = new FileStream(DfContainerFile, FileMode.Create))
            {
                serializer.WriteObject(file, frequencyProvider);
            }
        }
    }
}