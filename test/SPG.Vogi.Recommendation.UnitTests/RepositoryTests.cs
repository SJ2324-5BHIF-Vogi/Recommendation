using System.Data.Common;
using MongoDB.Bson;
using MongoDB.Driver;
using SPG.Vogi.Recommendation.DomainModel;
using SPG.Vogi.Recommendation.Repository;
using Xunit;


namespace SPG.Vogi.Recommendation.UnitTests
{
    public class RepositoryTests
    {

        [Fact]
        public void TestAddUser()
        {
            IMongoDbSettings settings = new MongoDbSettings();
            settings.ConnectionString = "mongodb://localhost:27000";
            settings.DatabaseName = "RecommendationDb";

            var repo = new MongoRepository<User>(settings);
            User localUser = new User("Username", "Email");
            ObjectId id = localUser.Id;
            
            repo.InsertOne(localUser);
            
            var local = repo.FindById(id.ToString());

            Assert.Equal(1,repo.AsQueryable().Count());
        }

        [Fact]
        public void TestFilterUsername()
        {
            IMongoDbSettings settings = new MongoDbSettings();
            settings.ConnectionString = "mongodb://localhost:27000";
            settings.DatabaseName = "RecommendationDb";

            var repo = new MongoRepository<User>(settings);

            var user01 = new User("username1", "email1");
            var user02 = new User("username2", "email2");
            var user03 = new User("username1", "email3");
            
            List<User> users = new List<User> { user01, user02, user03 };
            
            repo.InsertMany(users);

            var local = repo.FilterBuilderUser<User>()
                .FilterUsername("username1")
                .SortByUsernameDesc()
                .Build();
            
            
            Assert.Equal(2, local.Count());
        }

        [Fact]
        public void TestDeleteUser()
        {
            IMongoDbSettings settings = new MongoDbSettings();
            settings.ConnectionString = "mongodb://localhost:27000";
            settings.DatabaseName = "RecommendationDb";

            var repo = new MongoRepository<User>(settings);

            var user01 = new User("username1", "email1");
            var user02 = new User("username2", "email2");
            var user03 = new User("username1", "email3");
            
            List<User> users = new List<User> { user01, user02, user03 };

            repo.InsertMany(users);
            
            var filter = Builders<User>.Filter.Eq(u => u.Id, user02.Id);
            
            repo.DeleteOne(filter);
            
            Assert.Equal(2, repo.AsQueryable().Count());
        }
        
        [Fact]
        public void TestUpdateUser()
        {
            IMongoDbSettings settings = new MongoDbSettings();
            settings.ConnectionString = "mongodb://localhost:27000";
            settings.DatabaseName = "RecommendationDb";

            var repo = new MongoRepository<User>(settings);

            var user01 = new User("username1", "email1");
            var user02 = new User("username2", "email2");
            var user03 = new User("username1", "email3");
            
            List<User> users = new List<User> { user01, user02, user03 };

            ObjectId userId = user02.Id;
            
            repo.InsertMany(users);

            var local = repo.UpdateBuilderUser<User>(user02)
                .UpdateEmail("perfekt")
                .Save();

            var mail = repo.FindById(user02.Id.ToString());
            string expectedEmail = mail.Email.ToString();
            
            Assert.Equal("perfekt",expectedEmail);
        }

        [Fact]
        public void TestGetAllUser()
        {
            IMongoDbSettings settings = new MongoDbSettings();
            settings.ConnectionString = "mongodb://localhost:27000";
            settings.DatabaseName = "RecommendationDb";

            var repo = new MongoRepository<User>(settings);

            var user01 = new User("username1", "email1");
            var user02 = new User("username2", "email2");
            var user03 = new User("username1", "email3");
            
            List<User> users = new List<User> { user01, user02, user03 };
            
            repo.InsertMany(users);
            var local = repo.AsQueryable();
            Assert.Equal(3,local.Count());
        }

        [Fact]
        public void TestAddPost()
        {
            IMongoDbSettings settings = new MongoDbSettings();
            settings.ConnectionString = "mongodb://localhost:27000";
            settings.DatabaseName = "RecommendationDb";

            var repo = new MongoRepository<Posts>(settings);
            Posts post = new Posts(new User(), "Lorem Ipsum", new DateTime(2001, 01, 02));
            
            ObjectId id = post.Id;
            
            repo.InsertOne(post);
            
            var local = repo.FindById(id.ToString());

            Assert.Equal(1,repo.AsQueryable().Count());
        }

        [Fact]
        public void TestFilterPost()
        {
            IMongoDbSettings settings = new MongoDbSettings();
            settings.ConnectionString = "mongodb://localhost:27000";
            settings.DatabaseName = "RecommendationDb";

            var repo = new MongoRepository<Posts>(settings);

            var post01 = new Posts(new User(), "content1", new DateTime(2001, 01, 02));
            var post02 = new Posts(new User("username2", "email2"), "content2", new DateTime(2002, 01, 02));
            var post03 = new Posts(new User(), "content1", new DateTime(2001, 01, 02));
            
            List<Posts> posts = new List<Posts> { post01, post02, post03 };
            
            repo.InsertMany(posts);

            var local = repo.FilterBuilderPost<Posts>()
                .FilterUser("username2")
                .Build();
            
            Assert.Single(local);
        }

        [Fact]
        public void TestUpdatePost()
        {
            IMongoDbSettings settings = new MongoDbSettings();
            settings.ConnectionString = "mongodb://localhost:27000";
            settings.DatabaseName = "RecommendationDb";

            var repo = new MongoRepository<Posts>(settings);
            
            var post01 = new Posts(new User(), "content1", new DateTime(2001, 01, 02));
            var post02 = new Posts(new User("username2", "email2"), "content2", new DateTime(2002, 01, 02));
            var post03 = new Posts(new User(), "content1", new DateTime(2001, 01, 02));
            
            List<Posts> posts = new List<Posts> { post01, post02, post03 };
            
            repo.InsertMany(posts);

            var local = repo.UpdateBuilderPost<Posts>(post02)
                .UpdateContent("amazingcontent")
                .UpdateUser(new User("jay", "jay@jay.com"))
                .Save();
            
            var content = repo.FindById(post02.Id.ToString());
            var expectedcontent = content.Content;
            
            Assert.Equal("amazingcontent", expectedcontent);
        }

        [Fact]
        public void TestDeletePost()
        {
            IMongoDbSettings settings = new MongoDbSettings();
            settings.ConnectionString = "mongodb://localhost:27000";
            settings.DatabaseName = "RecommendationDb";

            var repo = new MongoRepository<Posts>(settings);
            
            var post01 = new Posts(new User(), "content1", new DateTime(2001, 01, 02));
            var post02 = new Posts(new User("username2", "email2"), "content2", new DateTime(2002, 01, 02));
            var post03 = new Posts(new User(), "content1", new DateTime(2001, 01, 02));
            
            List<Posts> posts = new List<Posts> { post01, post02, post03 };
            
            repo.InsertMany(posts);
            
            var filter = Builders<Posts>.Filter.Eq(u => u.Id, post02.Id);
            
            repo.DeleteOne(filter);
            
            Assert.Equal(2,repo.AsQueryable().Count());
        }

        [Fact]
        public void TestGetAllPost()
        {
            IMongoDbSettings settings = new MongoDbSettings();
            settings.ConnectionString = "mongodb://localhost:27000";
            settings.DatabaseName = "RecommendationDb";

            var repo = new MongoRepository<Posts>(settings);
            
            var post01 = new Posts(new User(), "content1", new DateTime(2001, 01, 02));
            var post02 = new Posts(new User("username2", "email2"), "content2", new DateTime(2002, 01, 02));
            var post03 = new Posts(new User(), "content1", new DateTime(2001, 01, 02));
            
            List<Posts> posts = new List<Posts> { post01, post02, post03 };
            
            repo.InsertMany(posts);
            
            Assert.Equal(3,repo.AsQueryable().Count());
        }

        [Fact]
        public void TestGetSinglePost()
        {
            IMongoDbSettings settings = new MongoDbSettings();
            settings.ConnectionString = "mongodb://localhost:27000";
            settings.DatabaseName = "RecommendationDb";

            var repoPost = new MongoRepository<Posts>(settings);
            
            var repoUser = new MongoRepository<User>(settings);

            var user01 = new User("username1", "email1");
            var user02 = new User("username2", "email2");
            var user03 = new User("username1", "email3");
            
            List<User> users = new List<User> { user01, user02, user03 };
            
            repoUser.InsertMany(users);
            
            var post01 = new Posts(user01, "content1", new DateTime(2001, 01, 02));
            var post02 = new Posts(user02, "content2", new DateTime(2002, 01, 02));
            var post03 = new Posts(user03, "content1", new DateTime(2001, 01, 02));
            
            List<Posts> posts = new List<Posts> { post01, post02, post03 };
            
            repoPost.InsertMany(posts);
            
            ObjectId postId = post02.Id;
            string expectedId = postId.ToString();
            
            var local = repoPost.FindById(expectedId);
            
            Assert.Equal(expectedId, local.Id.ToString());
        }
    }
}