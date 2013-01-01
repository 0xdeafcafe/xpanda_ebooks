using Markov;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TweetSharp;
using xpanda_ebooks.MySql;

namespace xpanda_ebooks
{
    public partial class CreateTweet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string content = "";
            List<int> indexs = new List<int>();
            int lastIndex = -1;
            Random ran = new Random();

            while (true)
            {
                int index = ran.Next(0, 2200);
                if (index != lastIndex)
                {
                    indexs.Add(index);
                    lastIndex = index;
                }

                if (indexs.Count > 20)
                    break;
            }

            content = GetTweets.GetFromDB(indexs);
            MarkovChain mc = new MarkovChain();
            mc.Load(content);
            string output = "";
            while (true)
            {
                output = mc.Output();

                if (output.Length < 140)
                    break;
            }

            var service = new TwitterService(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerKeySecret"]);
            service.AuthenticateWith(ConfigurationManager.AppSettings["AccessToken"], ConfigurationManager.AppSettings["AccessTokenSecret"]);
            service.SendTweet(output);
        }
    }
}