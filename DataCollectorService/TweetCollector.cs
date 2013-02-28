using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataCollectorService
{
    class TweetCollector
    {
        public List<Tweet> GetTweets()
        {
             using (var webClient = new WebClient())
            {
            var result = webClient.DownloadString("http://search.twitter.com/search.atom?q=" + Server.UrlEncode(query));
            var tweetDocument = XDocument.Parse(result);
            XNamespace xmlNamespace = "http://www.w3.org/2005/Atom";
            var tweets = from entry in tweetDocument.Descendants(xmlNamespace + "entry")
                         select new Tweet()
                         {
                             Author = entry.Descendants(xmlNamespace + "name").First().Value,
                             ID = entry.Element(xmlNamespace + "id").Value,
                             Text = entry.Element(xmlNamespace + "title").Value,
                             Time = DateTime.Parse(entry.Element(xmlNamespace + "published").Value)
                         };
                 return tweets.ToList
             }
            return null ;
        }
    }
}
