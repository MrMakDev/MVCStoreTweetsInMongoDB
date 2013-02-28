using Core.Domain;
using CoreServiceMongo.Services;
using MVCAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace MVCAssignment.Controllers
{
    public class TweetController : Controller
    {
        public ActionResult Index()
        {
            TweetService tweets = new TweetService();

            return View(tweets.GetTweets());
        }

        public ActionResult Search(SearchKey obj)
        {
            string query = obj.SearchKeyWord;
            //This code should probably be moved to a TwitterService or similar.
            using (var webClient = new WebClient())
            {
                //We should also add errorhandling, since the webclient (and parsing) could throw an exception.
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

                if (tweets != null && tweets.Count() > 0)
                {
                    foreach (Tweet twt in tweets)
                    {
                        TweetService service = new TweetService();
                        service.Create(twt);

                    }
                }
                return View(tweets.ToArray());
            }
        }
        public ActionResult Start()
        {
            return View();
        }

    }
}
