using MongoDB.Driver;
using SPG.Vogi.Recommendation.DomainModel;
using SPG.Vogi.Recommendation.DomainModel.Interfaces;

namespace SPG.Vogi.Recommendation.Repository.Builders;

public class PostUpdateBuilder<TDocument> : IPostUpdateBuilder<TDocument> where TDocument : Posts
{
    private readonly IMongoCollection<TDocument> _collection;
    public TDocument Data { get; set; }

    public PostUpdateBuilder(IMongoCollection<TDocument> collection, TDocument data)
    {
        _collection = collection;
        Data = data; 
    }
    public UpdateResult Save()
    {
        var filter = Builders<TDocument>.Filter.Eq(p => p.Id, Data.Id);
        var updateDefinition = Builders<TDocument>.Update
            .Set(p => p.User, Data.User)
            .Set(p => p.Content, Data.Content)
            .Set(p => p.CreatedAtPost, Data.CreatedAtPost);
        

        return _collection.UpdateOne(filter, updateDefinition);
    }

    public IPostUpdateBuilder<TDocument> UpdateUser(User user)
    {
        Data.User = user;
        return this;
    }

    public IPostUpdateBuilder<TDocument> UpdateContent(string content)
    {
        Data.Content = content;
        return this;
    }

    public IPostUpdateBuilder<TDocument> UpdateCreatedAt(DateTime dateTime)
    {
        Data.CreatedAtPost = dateTime;
        return this;
    }
}