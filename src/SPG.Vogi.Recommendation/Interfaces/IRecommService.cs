using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG.Vogi.Recommendation.DomainModel.Interfaces
{

    public interface IRecommService
    {
        List<string> searchForHashTags(string input);
        List<Posts> getPosts(int userId);
    }

}
