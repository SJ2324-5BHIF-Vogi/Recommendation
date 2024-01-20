using MongoDB.Driver;
using SPG.Vogi.Recommendation.DomainModel;
using SPG.Vogi.Recommendation.DomainModel.Interfaces;

namespace SPG.Vogi.Recommendation.Repository.Builders;

public class UserUpdateBuilder<TDocument> : IUserUpdateBuilder<TDocument> where TDocument : User
{
    private readonly IMongoCollection<TDocument> _collection;
    public TDocument Data { get; set; }

    public UserUpdateBuilder(IMongoCollection<TDocument> collection, TDocument data)
    {
        _collection = collection;
        Data = data; 
    }
    public UpdateResult Save()
    {
        var filter = Builders<TDocument>.Filter.Eq(u => u.Id, Data.Id);
        var updateDefinition = Builders<TDocument>.Update
            .Set(u => u.UserName, Data.UserName)
            .Set(u => u.Email, Data.Email);

        return _collection.UpdateOne(filter, updateDefinition);
    }

    public IUserUpdateBuilder<TDocument> UpdateUsername(string username)
    {
        Data.UserName = username;
        return this;
    }

    public IUserUpdateBuilder<TDocument> UpdateEmail(string email)
    {
        Data.Email = email;
        return this;
    }
}