using MongoDB.Driver;

namespace ValueBlue.MovieSearch.IntegrationTests.MongoDbTests
{
    public class DatabaseFixture
    {
        public DatabaseFixture()
        {
            const string connectionString = "mongodb://localhost:27017";
            const string databaseName = "IntegrationTests";
                
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase(databaseName);

            Database = database;
        }

        public IMongoDatabase Database { get; }
    }
}