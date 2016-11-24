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
using SellTables.ViewModels;

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


        public static ICollection<CreativeViewModel> Search(string input, string fieldName = "")
        {
            if (string.IsNullOrEmpty(input)) return new List<CreativeViewModel>();

            var terms = input.Trim().Replace("-", " ").Split(' ')
                .Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim() + "*");
            input = string.Join(" ", terms);

            return _search(input, fieldName);
        }

        public static ICollection<CreativeViewModel> GetAllIndexRecords()
        {
            // validate search index
            if (!System.IO.Directory.EnumerateFiles(_luceneDir).Any()) return new List<CreativeViewModel>();

            // set up lucene searcher
            var searcher = new IndexSearcher(_directory, false);
            var reader = IndexReader.Open(_directory, false);
            var docs = new List<Document>();
            var term = reader.TermDocs();
            while (term.Next()) docs.Add(searcher.Doc(term.Doc));
            reader.Dispose();
            searcher.Dispose();
            return MapLuceneToDataList(docs);
        }


      


        public static void AddUpdateLuceneIndex(ICollection<Creative> creative)
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
            doc.Add(new Field("Rating", creative.Rating.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("CreativeUri", creative.CreativeUri, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Date", creative.CreationDate.ToShortDateString() + " " + creative.CreationDate.ToShortTimeString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("EditDate", creative.EditDate.ToShortDateString() + " " + creative.EditDate.ToShortTimeString(), Field.Store.YES, Field.Index.ANALYZED));
            string tags = "";
            string chapters = "";
            foreach (var chapter in creative.Chapters)
            {
                if (chapter.TagsString != null)
                {
                    tags += chapter.TagsString + " ";
                }
                if (chapter != null)
                {
                    chapters += chapter.Name + "/";
                }
            }
            doc.Add(new Field("Tags", tags, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Chapters", chapters, Field.Store.YES, Field.Index.ANALYZED));

            writer.AddDocument(doc);
        }

     

        private static ICollection<Tag> GetTags(string tagList)
        {
            var stringList = tagList.Split(' ');
            var tags = new List<Tag>();
            if (stringList != null)
            {
                foreach (string text in stringList)
                    tags.Add(new Tag() { Description = text });
            }
            return tags;
        }


        private static ICollection<ChapterViewModel> GetChapters(string chaptersList)
        {
            var stringList = chaptersList.Split('/');
            var chapters = new List<ChapterViewModel>();
            if (stringList != null)
            {
                foreach (string text in stringList)
                    chapters.Add(new ChapterViewModel() { Name = text });
            }
            return chapters;
        }

        private static CreativeViewModel MapLuceneDocumentToData(Document doc)
        {
            return new CreativeViewModel
            {
                Id = Convert.ToInt32(doc.Get("Id")),
                UserName = doc.Get("User"),
                Name = doc.Get("Name"),
                Tags = doc.Get("Tags"),
                EditDate = doc.Get("EditDate"),
                Rating = Convert.ToDouble(doc.Get("Rating")),
                CreationDate = doc.Get("Date"),
                CreativeUri = doc.Get("CreativeUri"),
                Chapters = GetChapters(doc.Get("Chapters"))   
            };
        }

     

        private static ICollection<CreativeViewModel> MapLuceneToDataList(ICollection<Document> hits)
        {
            return hits.Select(MapLuceneDocumentToData).ToList();
        }


        private static ICollection<CreativeViewModel> MapLuceneToDataList(ICollection<ScoreDoc> hits,
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


        private static ICollection<CreativeViewModel> _search(string searchQuery, string searchField = "")
        {
            // validation
            if (string.IsNullOrEmpty(searchQuery.Replace("*", "").Replace("?", ""))) return new List<CreativeViewModel>();

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
                        (Version.LUCENE_30, new[] { "Id", "Name", "User, Rating", "Date", "Tags", "EditDate"}, analyzer);
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

    
    }
}