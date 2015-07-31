namespace MongoDB.Data
{
    using Bson;
    using Bson.Serialization;
    using Driver;

    public class MongoDBRepository
    {
        private const string connectionString = "mongodb://team:softuni123@ds047602.mongolab.com:47602/supermarket";
        private static readonly MongoClient client = new MongoClient(connectionString);
        private static readonly IMongoDatabase database = client.GetDatabase("supermarket");
        private static readonly IMongoCollection<BsonDocument> collection =
           database.GetCollection<BsonDocument>("SalesByProductReports");

        public async void ImportSalesByProductReport(string jsonReport)
        {
            await collection.InsertOneAsync(BsonSerializer.Deserialize<BsonDocument>(jsonReport));
        }
    }
}