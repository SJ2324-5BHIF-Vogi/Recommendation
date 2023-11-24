using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG.Vogi.Recommendation.DomainModel
{
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
