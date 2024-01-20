using MongoDB.Driver;

namespace SPG.Vogi.Recommendation.DomainModel.Interfaces;

public interface IPostUpdateBuilder<TDocument> where TDocument : Posts
{
    UpdateResult Save();
    IPostUpdateBuilder<TDocument> UpdateUser(User user);
    IPostUpdateBuilder<TDocument> UpdateContent(string content);
    IPostUpdateBuilder<TDocument> UpdateCreatedAt(DateTime dateTime);
}