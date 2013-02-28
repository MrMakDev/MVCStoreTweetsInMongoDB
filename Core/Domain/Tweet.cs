using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;

namespace Core.Domain
{
    public class Tweet
    {
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime Time { get; set; }

        public string ID { get; set; }

        [BsonId]
        public ObjectId tweetId { get; set; }
    }
}
