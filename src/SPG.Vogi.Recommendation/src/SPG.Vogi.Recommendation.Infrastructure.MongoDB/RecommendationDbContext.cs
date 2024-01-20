using MongoDB.Driver;

namespace SPG.Vogi.Recommendation.Infrastructure.MongoDB;

public class RecommendationDbContext
{
    private readonly MongoClient _client;

    public RecommendationDbContext()
    {
        var connectionString = $"mongodb://localhost:27000";
        MongoClientSettings settings = MongoClientSettings.FromConnectionString(connectionString);
        _client = new MongoClient(settings);
    }

    public MongoClient GetMongoClient()
    {
        return _client;
    }
}