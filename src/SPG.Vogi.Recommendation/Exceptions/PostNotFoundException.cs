using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG.Vogi.Recommendation.DomainModel.Exceptions
{
    public class PostNotFoundException : Exception
    {
        public PostNotFoundException() : base("No posts found!")
        {
        }
    }
}
