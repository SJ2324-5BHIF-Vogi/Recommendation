using SPG.Vogi.Recommendation.DomainModel;
using SPG.Vogi.Recommendation.DomainModel.Exceptions;
using SPG.Vogi.Recommendation.DomainModel.Interfaces;
using SPG.Vogi.Recommendation.Repository;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SPG.Vogi.Recommendation.Application
{
    public class RecommService : IRecommService
    {
        IMongoRepository<Posts> _mongoRepository;
        IMongoRepository<User> _mongoRepositoryUser;

        public RecommService(IMongoRepository<Posts> mongoRepository, IMongoRepository<User> mongoRepositoryUser)
        {
            _mongoRepository = mongoRepository;
            _mongoRepositoryUser = mongoRepositoryUser;
        }


        public List<string> searchForHashTags(string input)
        {
            //var input = "asdads sdfdsf #burgers, #rabbits dsfsdfds #sdf #dfgdfg";
            var regex = new Regex(@"#\w+");
            var matches = regex.Matches(input);
            List<string> hashTags = new List<string>();
            foreach (var match in matches)
            {
                hashTags.Add(match.ToString());
            }
            return hashTags;
        }
        public List<Posts> getPosts(string userId)
        {
            if (_mongoRepositoryUser.FindById(userId) is null)
                 throw new UserNotFoundException("User with ID:" + userId + "not found!");
                
            var posts =  _mongoRepository.AsQueryable().ToList();
            if (posts is null || posts.Count == 0)
                throw new PostNotFoundException();

            var random = new Random();
            int index = random.Next(posts.Count);

            List<Posts> returnvalue = new List<Posts>();
            for(int i = 0; i <= 10; i++)
            {
                index = random.Next(posts.Count);
                returnvalue.Add(posts[index]);
            }
            return returnvalue;
        }


        //public void Test() {
        //    MongoDbSettings mongoDbSettings = new MongoDbSettings();
        //    //user:pw @host
        //    mongoDbSettings.ConnectionString = "mongodb://root:1234@localhost:27017";
        //    mongoDbSettings.DatabaseName = "mongodb";

        //    //MongoRepository<Person> _peopleRepository = new MongoRepository<Person>(mongoDbSettings);
        //    MongoRepository<Posts> mongoRepository = new MongoRepository<Posts> (mongoDbSettings); 

        //}
    }
}