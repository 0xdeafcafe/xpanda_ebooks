using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xpanda_ebooks.MySql
{
    public class GetTweets
    {
        public static string GetFromDB(List<int> indexs)
        {
            string output = "";
            var query = new MySqlCommand("");

            foreach (int index in indexs)
            {
                query.CommandText = string.Format("SELECT * FROM `all_tweets` LIMIT @index,1");
                query.Parameters.Clear();
                query.Parameters.AddWithValue("@index", index);

                var sql = Connector.RunSQLSelectQuery(query);
                string fixedRow = HttpUtility.HtmlDecode(sql[0][0]);
                output += " " + fixedRow;
            }

            return output;
        }
        public static string GetLastID()
        {
            var query = new MySqlCommand("SELECT * FROM `all_tweets` ORDER BY `id` DESC LIMIT 0, 1");

            var sql = Connector.RunSQLSelectQuery(query);
            return HttpUtility.HtmlDecode(sql[0][1]);
        }
    }
}