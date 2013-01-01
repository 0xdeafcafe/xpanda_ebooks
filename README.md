## xpanda_ebooks

### How To Use ###
1. Create an SQL Database with the table `all_tweets` => tweet (text) -> id (text) -> date (text).
2. Update Connection strings in the `web.config` to be able to connect to it (Production for Release, Sandbox for Debug).
3. Update the `TwitterUser` setting in the `web.config` with the username of the person you're turning into an ebook.
4. Run `UpdateDatabase.aspx` with the querystring `?first=1` -> `UpdateDatabase.aspx?first=1`.
5. Create a twitter application on the ebook account and put the consumerkey/secret and accestoken/secret in the appsettings in `web.config`.
6. Then, simply run `CreateTweet.aspx` whenever you want to create a tweet (It's best to set this up as a scheduled task on a Window Server).