using SPG.Vogi.Recommendation.DomainModel;

namespace SPG.Vogi.Recommendation.UnitTests;

public class DomainModelTests
{
    [Fact]
    public void TestAdd()
    {
        var user01 = new User("username1", "email1");
        var user02 = new User("username2", "email2");
        var user03 = new User("username3", "email3");

        user01.AddFollower(user02);
        user01.AddFollower(user03);
        user01.AddFollowing(user03);
        
        Assert.Equal(1, user01.Followings.Count);
        Assert.Equal(2,user01.Followers.Count);
    }

    [Fact]
    public void TestRemove()
    {
        var user01 = new User("username1", "email1");
        var user02 = new User("username2", "email2");
        var user03 = new User("username3", "email3");
        
        user01.AddFollower(user02);
        user01.AddFollower(user03);
        user01.AddFollowing(user03);

        user01.RemoveFollower(user03);
        user01.RemoveFollowing(user03);
        
        Assert.Equal(0, user01.Followings.Count);
        Assert.Equal(1,user01.Followers.Count);
    }
    
    [Fact]
    public void TestAddPost()
    {
        var user01 = new User("username1", "email1");
        var user02 = new User("username2", "email2");

        var post01 = new Posts(user01, "Hello", new DateTime(2000, 01, 01));

        post01.AddLike(user02);
        
        Assert.Equal(1, post01.Likes.Count);
    }

    [Fact]
    public void TestRemovePost()
    {
        var user01 = new User("username1", "email1");
        var user02 = new User("username2", "email2");

        var post01 = new Posts(user01, "Hello", new DateTime(2000, 01, 01));

        post01.AddLike(user02);
        post01.RemoveLike(user02);
        
        Assert.Equal(0, post01.Likes.Count);
    }
}