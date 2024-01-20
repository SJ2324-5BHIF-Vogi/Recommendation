namespace SPG.Vogi.Recommendation.DomainModel.Interfaces;

public interface IUserFilterBuilder<TDocument> where TDocument : User
{
    IEnumerable<TDocument> Build();
    IUserFilterBuilder<TDocument> FilterUsername(string username);
    IUserFilterBuilder<TDocument> SortByUsernameAsc();
    IUserFilterBuilder<TDocument> SortByUsernameDesc();
}