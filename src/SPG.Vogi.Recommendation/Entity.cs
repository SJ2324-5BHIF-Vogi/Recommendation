namespace SPG.Vogi.Recommendation.DomainModel
{
    public class User
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        
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

    public class Posts
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public User user { get; set; }
        public String Content { get; set; }
        public DateTime CreatedAt { get; set; }
        private List<User> _likes { get; set; } = new();
        
        //Lists
        public IReadOnlyList<User> Likes => _likes;

        //public Dictionary<User, String> Comments { get; set; } = new Dictionary<User, String>();

    }

}