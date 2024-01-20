using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG.Vogi.Recommendation.DomainModel
{
    [BsonCollection("posts")]
    public class Posts : Document
    {
        public Posts(){}
        public Posts(User user, string content, DateTime createdAtPost)
        {
            Guid = Guid.NewGuid();
            User = user;
            Content = content;
            CreatedAtPost = createdAtPost;
        }

        public Guid Guid { get; set; }
        public User User { get; set; }
        public String Content { get; set; }
        public DateTime CreatedAtPost { get; set; }
        private List<User> _likes { get; set; } = new();

        
        //Lists
        public IReadOnlyList<User> Likes => _likes;

        //public Dictionary<User, String> Comments { get; set; } = new Dictionary<User, String>();

    }
}
