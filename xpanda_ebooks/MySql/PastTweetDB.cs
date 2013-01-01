using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace xpanda_ebooks.MySql
{
    public static class PastTweetDB
    {
        public static void AddTweets(Dictionary<string, object> sqlNigs)
        {
            MySqlCommand query = new MySqlCommand();
            query.CommandText = 
@"INSERT INTO `all_tweets` 
(
`tweet` , 
`id` , 
`date`
)
VALUES 
(
@tweet, 
@id, 
@date
);";

            query.Parameters.Clear();
            foreach (var sqlParam in sqlNigs)
                query.Parameters.AddWithValue("@" + sqlParam.Key.ToString(), sqlParam.Value);

            Connector.RunSQLUpdateQuery(query);
        }
    }
}