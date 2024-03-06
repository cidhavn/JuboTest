using MongoDB.Bson;
using MongoDB.Driver;

namespace JuboTest.Repository.Jubo
{
    public abstract class RepositoryBase
    {
        private readonly JuboRepositorySetting _setting;

        public RepositoryBase(JuboRepositorySetting setting)
        {
            _setting = setting;
        }

        /// <summary>
        /// 取得集區，如果不存在會建立
        /// </summary>
        /// <param name="database">資料庫</param>
        /// <param name="collectionName">集區名稱</param>
        protected IMongoCollection<T> GetCollection<T>(string collectionName) where T : JuboModelBase
        {
            var database = ClientFactory.GetClient(_setting.Default).GetDatabase("JuboTest");
            var filter = new BsonDocument("name", collectionName);
            var options = new ListCollectionNamesOptions { Filter = filter };

            if (database.ListCollectionNames(options).Any() == false)
            {
                database.CreateCollection(collectionName);
            }

            return database.GetCollection<T>(collectionName);
        }
    }

    internal class ClientFactory
    {
        private static Dictionary<string, MongoClient> _clients = new Dictionary<string, MongoClient>();

        private ClientFactory()
        { }

        /// <summary>
        /// 取得 MongoDB 連線，會處理重複連線
        /// </summary>
        public static MongoClient GetClient(string connectionString)
        {
            MongoClient client = null;

            if (_clients.ContainsKey(connectionString))
            {
                client = _clients[connectionString];

                if (client == null)
                {
                    throw new Exception("Connect MongoDB fail.");
                }
            }
            else
            {
                client = new MongoClient(MongoUrl.Create(connectionString));

                if (client == null)
                {
                    throw new Exception("Can not connect to MongoDB.");
                }

                _clients.TryAdd(connectionString, client);
            }

            return client;
        }
    }
}