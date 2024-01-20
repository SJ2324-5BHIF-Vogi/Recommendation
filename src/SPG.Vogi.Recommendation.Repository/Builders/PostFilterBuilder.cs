using MongoDB.Driver;
using SPG.Vogi.Recommendation.DomainModel;
using SPG.Vogi.Recommendation.DomainModel.Interfaces;

namespace SPG.Vogi.Recommendation.Repository.Builders;

public class PostFilterBuilder<TDocument> : IPostFilterBuilder<TDocument> where TDocument : Posts
{
    private readonly IMongoCollection<TDocument> _collectionFilter;
    private FilterDefinition<TDocument> _filterDefinition;
    private SortDefinition<TDocument> _sortDefinition;

    public PostFilterBuilder(IMongoCollection<TDocument> collectionFilter)
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

    public IPostFilterBuilder<TDocument> FilterUser(string username)
    {
        _filterDefinition = Builders<TDocument>.Filter.Eq(p => p.User.UserName, username);
        return this;
    }

    public IPostFilterBuilder<TDocument> FilterCreatedAt(DateTime dateTime)
    {
        _filterDefinition = Builders<TDocument>.Filter.Eq(p => p.CreatedAtPost, dateTime);
        return this;
    }

    public IPostFilterBuilder<TDocument> SortByCreatedAt()
    {
        _sortDefinition = Builders<TDocument>.Sort.Ascending(p => p.CreatedAtPost);
        return this;
    }

    public IPostFilterBuilder<TDocument> SortByCreatedAtDesc()
    {
        _sortDefinition = Builders<TDocument>.Sort.Descending(p => p.CreatedAtPost);
        return this;
    }
}