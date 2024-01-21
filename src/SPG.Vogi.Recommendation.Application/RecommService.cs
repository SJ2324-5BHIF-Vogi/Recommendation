using SPG.Vogi.Recommendation.DomainModel;
using SPG.Vogi.Recommendation.DomainModel.Exceptions;
using SPG.Vogi.Recommendation.DomainModel.Interfaces;
using SPG.Vogi.Recommendation.Repository;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace SPG.Vogi.Recommendation.Application
{
    public class RecommService : IRecommService
    {
        private readonly ILogger<RecommService> _logger;
        private readonly IMongoRepository<Posts> _mongoRepository;
        private readonly IMongoRepository<User> _mongoRepositoryUser;

        public RecommService(IMongoRepository<Posts> mongoRepository, IMongoRepository<User> mongoRepositoryUser, ILogger<RecommService> logger)
        {
            _mongoRepository = mongoRepository;
            _mongoRepositoryUser = mongoRepositoryUser;
            _logger = logger;
        }


        public List<string> SearchForHashTags(string input)
        {
            //var input = "asdads sdfdsf #burgers, #rabbits dsfsdfds #sdf #dfgdfg";
            var regex = new Regex(@"#\w+");
            var matches = regex.Matches(input);
            List<string> hashTags = new List<string>();
            foreach (var match in matches)
            {
                hashTags.Add(match.ToString());
            }
            _logger.LogInformation("Hashtags search initiated and delivered");
            return hashTags;
        }
        public List<Posts> GetPosts(string userId)
        {
            if (_mongoRepositoryUser.FindById(userId) is null)
            {
                _logger.LogInformation($"User with Id {userId} was not found!");
                throw new UserNotFoundException("User with ID:" + userId + "not found!");
            }

            var posts =  _mongoRepository.AsQueryable().ToList();
            if (posts is null || posts.Count == 0)
            {
                _logger.LogInformation($"No posts found!");
                throw new PostNotFoundException();
            }

            var random = new Random();
            int index = random.Next(posts.Count);

            List<Posts> returnvalue = new List<Posts>();
            for(int i = 0; i <= 10; i++)
            {
                index = random.Next(posts.Count);
                returnvalue.Add(posts[index]);
            }
            _logger.LogInformation($"Posts successfully recommended!");
            return returnvalue;
        }

        public List<Posts> GetAllPosts()
        {
            _logger.LogInformation($"Posts successfully recieved!");
            return _mongoRepository.AsQueryable().ToList();
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