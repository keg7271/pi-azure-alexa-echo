using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace PiAzureAlexa.Skills.Data
{
    public static class DocumentDBClient
    {
        public static Uri DatabaseLink { get; set; }

        public static string AuthorizationKey { get; set; }

        public static Uri DatabaseCollectionLink { get; set; }

        public static string DatabaseName { get; set; }

        public static string DatabaseCollectionName { get; set; }
        
        public static async Task<T> GetDbAsync<T>(string Key)
        {
            var item = default(T);
            try
            {
                var documentUri = UriFactory.CreateDocumentUri(DatabaseName, DatabaseCollectionName, Key);

                ResourceResponse<Document> response = null;

                using (var noSqlClient = new DocumentClient(DatabaseLink, AuthorizationKey))
                {
                    response = await noSqlClient.ReadDocumentAsync(documentUri).ConfigureAwait(false);
                }

                item = (T)(dynamic)response.Resource;
            }
            catch (DocumentClientException dcex)
            {
                if (dcex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return default(T);
                }
            }
            catch
            {
                return default(T);
            }
            return item;
        }
        
        public static async Task<Database> GetOrCreateDocumentDBAsync()
        {
            using (var noSqlClient = new DocumentClient(DatabaseLink, AuthorizationKey))
            {
                //Check if DocumentDB exists, if not create new DocumentDB
                Database database = noSqlClient.CreateDatabaseQuery()
                    .Where(db => db.Id == DatabaseName)
                    .AsEnumerable()
                    .FirstOrDefault();
                if (database == null)
                {
                    database = await noSqlClient.CreateDatabaseAsync(new Database { Id = DatabaseName });
                }
                return database;
            }
        }

        public static async Task<DocumentCollection> GetOrCreateCollectionAsync()
        {
            DocumentCollection collection = null;
            //Check if DocumentDB exists, if not create new DocumentDB
            var database = await GetOrCreateDocumentDBAsync();

            if (database != null)
            {
                using (var noSqlClient = new DocumentClient(DatabaseLink, AuthorizationKey))
                {
                    //Check if collection exists, if not create new collection
                    collection = noSqlClient.CreateDocumentCollectionQuery("dbs/" + DatabaseName)
                        .Where(c => c.Id == DatabaseCollectionName)
                        .AsEnumerable()
                        .FirstOrDefault();

                    if (collection == null)
                    {
                        collection = await noSqlClient.CreateDocumentCollectionAsync("dbs/" + DatabaseName, new DocumentCollection { Id = DatabaseCollectionName });
                    }
                }
            }
            return collection;
        }
    }
}
