namespace SPG.Vogi.Recommendation.DomainModel.Interfaces;

public interface IPostFilterBuilder<TDocument> where TDocument : Posts
{
    IEnumerable<TDocument> Build();
    IPostFilterBuilder<TDocument> FilterUser(string username);
    IPostFilterBuilder<TDocument> FilterCreatedAt(DateTime dateTime);
    IPostFilterBuilder<TDocument> SortByCreatedAt();
    IPostFilterBuilder<TDocument> SortByCreatedAtDesc();
}