using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG.Vogi.Recommendation.DomainModel.Interfaces
{

    public interface IRecommService
    {
        List<string> SearchForHashTags(string input);
        List<Posts> GetPosts(string userId);

        List<Posts> GetAllPosts();
    }

}
