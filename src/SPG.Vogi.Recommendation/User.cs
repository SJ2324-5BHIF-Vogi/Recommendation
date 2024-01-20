using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SPG.Vogi.Recommendation.DomainModel
{
    [BsonCollection("users")]
    public class User : Document
    {
        public Guid Guid { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public User(){}

        public User(string userName, string email)
        {
            Guid = Guid.NewGuid();
            UserName = userName;
            Email = email;
        }
        //Lists
        private List<Posts> _posts { get; set; } = new();
        public IReadOnlyList<Posts> Posts => _posts;
        private List<Posts> _likesPosts { get; set; } = new();
        public IReadOnlyList<Posts> LikesPosts => _likesPosts;
        private List<User> _followings { get; set; } = new();
        public IReadOnlyList<User> Followings => _followings;
        private List<User> _followers { get; set; } = new();
        public IReadOnlyList<User> Followers => _followers;
       
    }

    
}