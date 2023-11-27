using SPG.Vogi.Recommendation.DomainModel;
using SPG.Vogi.Recommendation.Repository;
using System;

namespace SPG.Vogi.Recommendation.Application
{
    public class RecommService
    {
        public void Test() {
            MongoDbSettings mongoDbSettings = new MongoDbSettings();
            //user:pw @host
            mongoDbSettings.ConnectionString = "mongodb://root:1234@localhost:27017";
            mongoDbSettings.DatabaseName = "mongodb";

            //MongoRepository<Person> _peopleRepository = new MongoRepository<Person>(mongoDbSettings);
            MongoRepository<Posts> mongoRepository = new MongoRepository<Posts> (mongoDbSettings);  
        }


    }
}