namespace SPG.Vogi.Recommendation.DomainModel
{
    public class User
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<Posts> Posts{ get; set; } = new List<Posts>();
        public List<Posts> LikesPosts { get; set; } = new List<Posts>();
        public List<User> Followings { get; set; } = new List<User>();
        public List<User> Followers { get; set; } = new List<User>();



    }

    public class Posts
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public User user { get; set; }
        public String Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<User> Likes { get; set; }
        //public Dictionary<User, String> Comments { get; set; } = new Dictionary<User, String>();

    }

}