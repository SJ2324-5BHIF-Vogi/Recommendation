using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;



namespace SPG.Vogi.Recommendation.TextAnalyseTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            const LuceneVersion AppLuceneVersion = LuceneVersion.LUCENE_48;
            // Construct a machine-independent path for the index
            var basePath = Environment.GetFolderPath(
                Environment.SpecialFolder.CommonApplicationData);
            var indexPath = Path.Combine(basePath, "index");

            using var dir = FSDirectory.Open(indexPath);

            // Create an analyzer to process the text
            var analyzer = new StandardAnalyzer(AppLuceneVersion);

            // Create an index writer
            var indexConfig = new IndexWriterConfig(AppLuceneVersion, analyzer);
            using var writer = new IndexWriter(dir, indexConfig);
        }
        public void Query() 
        {
            // Search with a phrase
            var phrase = new MultiPhraseQuery
                {
                    new Term("favoritePhrase", "brown"),
                    new Term("favoritePhrase", "fox")
                };
        }
        public void addToIndex(IndexWriter writer) 
        {
            var source = new
            {
                Name = "Kermit the Frog",
                FavoritePhrase = "The quick brown fox jumps over the lazy dog"
            };
            var doc = new Document
            {
                // StringField indexes but doesn't tokenize
                new StringField("name",
                    source.Name,
                    Field.Store.YES),
                new TextField("favoritePhrase",
                    source.FavoritePhrase,
                    Field.Store.YES)
            };

            writer.AddDocument(doc);
            writer.Flush(triggerMerge: false, applyAllDeletes: false);
        }

        public void FetchResult(IndexWriter writer)
        {
            // Re-use the writer to get real-time updates
            using var reader = writer.GetReader(applyAllDeletes: true);
            var searcher = new IndexSearcher(reader);
            var hits = searcher.Search(phrase, 20 /* top 20 */).ScoreDocs;

            // Display the output in a table
            Console.WriteLine($"{"Score",10}" +
                $" {"Name",-15}" +
                $" {"Favorite Phrase",-40}");
            foreach (var hit in hits)
            {
                var foundDoc = searcher.Doc(hit.Doc);
                Console.WriteLine($"{hit.Score:f8}" +
                    $" {foundDoc.Get("name"),-15}" +
                    $" {foundDoc.Get("favoritePhrase"),-40}");
            }
        }
    }
}