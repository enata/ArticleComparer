using System;
using System.IO;
using System.Runtime.Serialization;
using ArticleComparer.ArticleLoading;
using ArticleComparer.Core.Loading;
using ArticleComparer.Core.Processing;
using ArticleComparer.TextProcessing;

namespace ArticleComparer.TestConsole
{
    internal class Program
    {
        private const string FrequenciesFile = "dfContainer";

        private static void Main(string[] args)
        {
            var htmlCleaner = new HtmlCleaner(new ISiteHtmlCleaner[] {new DailyHtmlCleaner(), new MirrorHtmlCleaner()});
            var htmlLoader = new HtmlLoader();
            var articleProvider = new ArticleProvider(htmlCleaner, htmlLoader);
            var cosineSimilarityCalculator = new CosineSimilarityCalculator();
            IDocumentFrequencyProvider dfProvider = LoadFrequencies();
            var tfIdfCalculator = new TfIdfCalculator(dfProvider);
            var tokenizer = new Tokenizer();
            var articleProcessor = new ArticleProcessor(tfIdfCalculator, tokenizer);
            var articleComparer = new TextProcessing.ArticleComparer(articleProvider, cosineSimilarityCalculator,
                                                                     articleProcessor);

            Console.WriteLine("Similar articles:");
            double similarity =
                articleComparer.Compare(
                    @"http://www.dailymail.co.uk/news/article-2489957/Britains-spy-chiefs-grilled-MPs-television-time.html",
                    @"http://www.mirror.co.uk/news/uk-news/mi6-mi5-gchq-bosses-questioned-2685310");
            Console.WriteLine(similarity);
            similarity =
                articleComparer.Compare(
                    @"http://www.dailymail.co.uk/news/article-2489640/80-parents-caught-children-copying-porn-style-dances-offensive-lyrics.html",
                    @"http://www.mirror.co.uk/news/uk-news/miley-cyrus-twerking-kids-copying-2685363");
            Console.WriteLine(similarity);

            Console.WriteLine("Same article:");
            similarity =
                articleComparer.Compare(
                    @"http://www.dailymail.co.uk/news/article-2490296/You-STILL-likely-lose-job-recession-25s-shop-workers-risk.html",
                    @"http://www.dailymail.co.uk/news/article-2490296/You-STILL-likely-lose-job-recession-25s-shop-workers-risk.html");
            Console.WriteLine(similarity);

            Console.WriteLine("Different articles:");
            similarity =
                articleComparer.Compare(
                    @"http://www.dailymail.co.uk/femail/article-2489984/Needy-people-likely-cheat.html",
                    @"http://www.dailymail.co.uk/news/article-2490531/Worlds-oldest-paperboy-deliver-round-71-years-route.html");
            Console.WriteLine(similarity);
            similarity =
                articleComparer.Compare(
                    @"http://www.dailymail.co.uk/news/article-2490412/Wikileaks-journalist-spent-4-months-Edward-Snowden-leaves-Russia.html",
                    @"http://www.dailymail.co.uk/news/article-2489994/Twitter-share-prices-soar-firms-day-trading.html");
            Console.WriteLine(similarity);
            Console.ReadKey();
        }

        public static IDocumentFrequencyProvider LoadFrequencies()
        {
            var serializer = new DataContractSerializer(typeof (DocumentFrequencyProvider));
            using (var fileStream = new FileStream(FrequenciesFile, FileMode.Open))
            {
                var result = (IDocumentFrequencyProvider) serializer.ReadObject(fileStream);
                return result;
            }
        }
    }
}