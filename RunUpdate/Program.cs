using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace RunUpdate
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args != null && args.Length == 1)
            {
                if (args[0] == "tweet")
                {
                    WebClient wb = new WebClient();
                    wb.DownloadString("http://localhost:1/CreateTweet.aspx");
                }
                else if (args[0] == "update")
                {
                    WebClient wb = new WebClient();
                    wb.DownloadString("http://localhost:1/UpdateDatabase.aspx");
                }
            }
        }
    }
}
