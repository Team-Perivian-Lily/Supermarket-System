using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB.Data
{
    public static class MongoDBRepository
    {
        private const string connectionString = "mongodb://team:softuni123@ds047602.mongolab.com:47602/supermarket";
        private static MongoClient client = new MongoClient(connectionString);
        private static IMongoDatabase database = client.GetDatabase("supermarket");
        private static IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("SalesByProductReports");

        public static async void ImportSalesByProductReport(string jsonReport)
        {
            await collection.InsertOneAsync(MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(jsonReport));
        }
    }
}
