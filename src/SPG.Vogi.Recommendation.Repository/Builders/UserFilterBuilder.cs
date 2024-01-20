using MongoDB.Driver;
using SPG.Vogi.Recommendation.DomainModel;
using SPG.Vogi.Recommendation.DomainModel.Interfaces;
using SPG.Vogi.Recommendation.Infrastructure.MongoDB;

namespace SPG.Vogi.Recommendation.Repository.Builders;

public class UserFilterBuilder<TDocument> : IUserFilterBuilder<TDocument> where TDocument : User
{
    private readonly IMongoCollection<TDocument> _collectionFilter;
    private FilterDefinition<TDocument> _filterDefinition;
    private SortDefinition<TDocument> _sortDefinition;

    public UserFilterBuilder(IMongoCollection<TDocument> collectionFilter)
    {
        _collectionFilter = collectionFilter;
    }


    public IEnumerable<TDocument> Build()
    {
        var findFluent = _collectionFilter.Find(_filterDefinition);

        if (_sortDefinition != null)
        {
            findFluent = findFluent.Sort(_sortDefinition);
        }

        var result = findFluent.ToList();
        return result;
    }

    public IUserFilterBuilder<TDocument> FilterUsername(string username)
    {
        _filterDefinition = Builders<TDocument>.Filter.Eq(u => u.UserName, username);
        return this;
    }

    public IUserFilterBuilder<TDocument> SortByUsernameAsc()
    {
        _sortDefinition = Builders<TDocument>.Sort.Ascending(u => u.UserName);
        return this;
    }

    public IUserFilterBuilder<TDocument> SortByUsernameDesc()
    {
        _sortDefinition = Builders<TDocument>.Sort.Descending(u => u.UserName);
        return this;
    }
}