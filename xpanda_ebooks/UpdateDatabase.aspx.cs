using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using xpanda_ebooks.MySql;

namespace xpanda_ebooks
{
    public partial class UpdateDatabase : Page
    {
        public class TweetStructure
        {
            public string created_at { get; set; }
            public string id_str { get; set; }
            public string text { get; set; }
            public string in_reply_to_status_id { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isFirst = (Request.QueryString["first"] == "1");

            if (isFirst)
            {
                var lastID = "";
                var allTweets = new List<TweetStructure>();

                for (var i = 0; i < 20; i++)
                {
                    var wb = new WebClient();

                    var twitterURL = "https://api.twitter.com/1/statuses/user_timeline.json?screen_name=" + ConfigurationManager.AppSettings["TwitterUser"] + "&count=200&exclude_replies=true";
                    if (lastID != "")
                        twitterURL += "&max_id=" + lastID;

                    var twitterResponse = wb.DownloadString(twitterURL);
                    var tweets = JsonConvert.DeserializeObject<IList<TweetStructure>>(twitterResponse);
                    lastID = tweets[tweets.Count - 1].id_str;
                    foreach (TweetStructure tweet in tweets)
                        if (!allTweets.Contains(tweet))
                            allTweets.Add(tweet);
                }

                foreach (TweetStructure tweet in allTweets)
                {
                    PastTweetDB.AddTweets(new Dictionary<string, object>
                    {
                        { "tweet", tweet.text },
                        { "id", tweet.id_str },
                        { "date", tweet.created_at }
                    });
                }
            }
            else
            {
                var lastID = "";
                var allTweets = new List<TweetStructure>();
                var lastSqlID = GetTweets.GetLastID();

                for (int i = 0; i < 1; i++)
                {
                    var wb = new WebClient();

                    var twitterURL = "https://api.twitter.com/1/statuses/user_timeline.json?screen_name=" + ConfigurationManager.AppSettings["TwitterUser"] + "&count=200&exclude_replies=true&since_id=" + lastSqlID;
                    if (lastID != "")
                        twitterURL += "&max_id=" + lastID;

                    var twitterResponse = wb.DownloadString(twitterURL);
                    var tweets = JsonConvert.DeserializeObject<IList<TweetStructure>>(twitterResponse);
                    if (tweets.Count <= 0)
                        lastID = "";
                    else
                        lastID = tweets[tweets.Count - 1].id_str;

                    foreach (var tweet in tweets)
                    {
                        var derpAsFuck = false;
                        foreach (TweetStructure tweetlv1 in allTweets)
                            if (tweet.id_str == tweetlv1.id_str)
                                derpAsFuck = true;

                        if (!derpAsFuck)
                            allTweets.Add(tweet);
                    }
                }

                foreach (TweetStructure tweet in allTweets)
                {
                    PastTweetDB.AddTweets(new Dictionary<string, object>
                    {
                        { "tweet", tweet.text },
                        { "id", tweet.id_str },
                        { "date", tweet.created_at }
                    });
                }
            }
        }
    }
}