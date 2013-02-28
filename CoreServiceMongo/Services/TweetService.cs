using Core.Domain;
using CoreServiceMongo.Helper;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreServiceMongo.Services
{

    public class TweetService
    {
        private readonly MongoHelper<Tweet> _tweets;

        public TweetService()
        {
            _tweets = new MongoHelper<Tweet>();
        }

        public void Create(Tweet tweet)
        {

            _tweets.Collection.Save(tweet);
        }

        public void Edit(Tweet tweet)
        {
            _tweets.Collection.Update(
                Query.EQ("_id", tweet.tweetId),
                Update.Set("Text", tweet.Text)
                .Set("ID", tweet.ID)
                .Set("Author", tweet.Author));
        }

        public void Delete(ObjectId tweetId)
        {
            _tweets.Collection.Remove(Query.EQ("_id", tweetId));
        }

        public IList<Tweet> GetTweets()
        {
            return _tweets.Collection.FindAll().ToList();
        }

        public Tweet GetTweet(ObjectId id)
        {
            var tweet = _tweets.Collection.Find(Query.EQ("_id", id)).Single();

            return tweet;
        }


    }
}
