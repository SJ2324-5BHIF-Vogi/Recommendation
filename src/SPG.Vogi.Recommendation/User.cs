using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SPG.Vogi.Recommendation.DomainModel.Exceptions;

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

        public bool AddFollower(User follower)
        {
            bool containsEmail = _followers.Any(u => u.Email == follower.Email);

            if (containsEmail) return false;
            _followers.Add(follower);
            return true;
        }
        
        public bool AddFollowing(User following)
        {
            bool containsEmail = _followings.Any(u => u.Email == following.Email);

            if (containsEmail) return false;
            _followings.Add(following);
            return true;
        }
        
        public bool RemoveFollower(User follower)
        {
            User userToRemove = _followers.FirstOrDefault(u => u.Email == follower.Email) ?? throw new UserNotFoundException("No user found in the list!");

            _followers.Remove(userToRemove);
            return true;
        }

        public bool RemoveFollowing(User following)
        {
            User userToRemove = _followings.FirstOrDefault(u => u.Email == following.Email) ?? throw new UserNotFoundException("No user found in the list!");

            _followings.Remove(userToRemove);
            return true;
        }

        public bool AddPost(Posts post)
        {
            bool containsContent = _posts.Any(p=> p.Content == post.Content);

            if (containsContent) return false;
            _posts.Add(post);
            return true;
        }
        
        public bool AddLikedPost(Posts likesPost)
        {
            bool containsContent = _likesPosts.Any(p=> p.Content == likesPost.Content);

            if (containsContent) return false;
            _likesPosts.Add(likesPost);
            return true;
        }
        
        public bool RemovePost(Posts post)
        {
            Posts postToRemove = _posts.FirstOrDefault(p => p.Id == post.Id) ?? throw new PostNotFoundException();

            _posts.Remove(post);
            return true;
        }
        
        public bool RemoveLikedPost(Posts likedPost)
        {
            Posts postToRemove = _posts.FirstOrDefault(p => p.Id == likedPost.Id) ?? throw new PostNotFoundException();

            _posts.Remove(likedPost);
            return true;
        }
        
    }

    
}