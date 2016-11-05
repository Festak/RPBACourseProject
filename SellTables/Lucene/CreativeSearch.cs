using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using System;
using Version = Lucene.Net.Util.Version;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lucene.Net.Store;
using System.IO;
using SellTables.Models;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using Lucene.Net.Linq;

namespace SellTables.Lucene
{
    public class CreativeSearch
    {
        private static string _luceneDir = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "LuceneIndexOfCreative");

        private static FSDirectory _directoryTemp;

        private static FSDirectory _directory
        {
            get
            {
                if (_directoryTemp == null) _directoryTemp = FSDirectory.Open(new DirectoryInfo(_luceneDir));
                if (IndexWriter.IsLocked(_directoryTemp)) IndexWriter.Unlock(_directoryTemp);
                var lockFilePath = Path.Combine(_luceneDir, "write.lock");
                if (File.Exists(lockFilePath)) File.Delete(lockFilePath);
                return _directoryTemp;
            }
        }

        private static void AddToLuceneIndex(Creative creative, IndexWriter writer)
        {
            // remove older index
            var searchQuery = new TermQuery(new Term("Id", creative.Id.ToString()));
            writer.DeleteDocuments(searchQuery);
            // add new index
            var doc = new Document();
            // add lucene fields mapped to db fields
            doc.Add(new Field("Id", creative.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("Name", creative.Name, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("User", creative.User.UserName, Field.Store.YES, Field.Index.ANALYZED));
            // add to index
            writer.AddDocument(doc);
        }

        public static void AddUpdateLuceneIndex(IEnumerable<Creative> creative)
        {
            // init lucene
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            using (var writer = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                // add data to lucene search index (replaces older if any)
                foreach (var a in creative) AddToLuceneIndex(a, writer);
                // close handles
                analyzer.Close();
                writer.Dispose();
            }
        }

        public static void AddUpdateLuceneIndex(Creative creative)
        {
            AddUpdateLuceneIndex(new List<Creative> { creative });
        }

        public static void ClearLuceneIndexRecord(int creativeId)
        {
            // init lucene
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            using (var writer = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                // remove older index 
                var searchQuery = new TermQuery(new Term("Id", creativeId.ToString()));
                writer.DeleteDocuments(searchQuery);
                // close handles
                analyzer.Close();
                writer.Dispose();
            }
        }
        public static bool ClearLuceneIndex()
        {
            try
            {
                var analyzer = new StandardAnalyzer(Version.LUCENE_30);
                using (var writer = new IndexWriter(_directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    // remove older index entries
                    writer.DeleteAll();
                    // close handles
                    analyzer.Close();
                    writer.Dispose();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static void Optimize()
        {
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            using (var writer = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                analyzer.Close();
                writer.Optimize();
                writer.Dispose();
            }
        }

        private static Creative MapLuceneDocumentToData(Document doc)
        {
            return new Creative
            {
                Id = Convert.ToInt32(doc.Get("Id")),
                Name = doc.Get("Name"),
                User = new ApplicationUser() { UserName = doc.Get("User") }
            };
        }

     

        private static IEnumerable<Creative> MapLuceneToDataList(IEnumerable<Document> hits)
        {
            return hits.Select(MapLuceneDocumentToData).ToList();
        }

        private static IEnumerable<Creative> MapLuceneToDataList(IEnumerable<ScoreDoc> hits,
            IndexSearcher searcher)
        {
            return hits.Select(hit => MapLuceneDocumentToData(searcher.Doc(hit.Doc))).ToList();
        }

        private static Query parseQuery(string searchQuery, QueryParser parser)
        {
            Query query;
            try
            {
                query = parser.Parse(searchQuery.Trim());
            }
            catch (ParseException)
            {
                query = parser.Parse(QueryParser.Escape(searchQuery.Trim()));
            }
            return query;
        }

        private static IEnumerable<Creative> _search(string searchQuery, string searchField = "")
        {
            // validation
            if (string.IsNullOrEmpty(searchQuery.Replace("*", "").Replace("?", ""))) return new List<Creative>();

            // set up lucene searcher
            using (var searcher = new IndexSearcher(_directory, false))
            {
                var hits_limit = 1000;
                var analyzer = new StandardAnalyzer(Version.LUCENE_30);

                // search by single field
                if (!string.IsNullOrEmpty(searchField))
                {
                    var parser = new QueryParser(Version.LUCENE_30, searchField, analyzer);
                    var query = parseQuery(searchQuery, parser);
                    var hits = searcher.Search(query, hits_limit).ScoreDocs;
                    var results = MapLuceneToDataList(hits, searcher);
                    analyzer.Close();
                    searcher.Dispose();
                    return results;
                }
                // search by multiple fields (ordered by RELEVANCE)
                else
                {
                    var parser = new MultiFieldQueryParser
                        (Version.LUCENE_30, new[] { "Id", "Name", "User"}, analyzer);
                    var query = parseQuery(searchQuery, parser);
                    var hits = searcher.Search
                    (query, null, hits_limit, Sort.RELEVANCE).ScoreDocs;
                    var results = MapLuceneToDataList(hits, searcher);
                    analyzer.Close();
                    searcher.Dispose();
                    return results;
                }
            }
        }

        public static IEnumerable<Creative> Search(string input, string fieldName = "")
        {
            if (string.IsNullOrEmpty(input)) return new List<Creative>();

            var terms = input.Trim().Replace("-", " ").Split(' ')
                .Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim() + "*");
            input = string.Join(" ", terms);

            return _search(input, fieldName);
        }
    }
}