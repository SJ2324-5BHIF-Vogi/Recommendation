using MongoDB.Driver;

namespace SPG.Vogi.Recommendation.DomainModel.Interfaces;

public interface IUserUpdateBuilder<TDocument> where TDocument : User
{
    UpdateResult Save();
    IUserUpdateBuilder<TDocument> UpdateUsername(string username);
    IUserUpdateBuilder<TDocument> UpdateEmail(string email);
}