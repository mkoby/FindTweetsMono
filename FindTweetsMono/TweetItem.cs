using System;
using System.Json;
using System.Runtime.Serialization;

namespace FindTweetsMono
{
	public class TweetItem
	{
		public TweetItem ()
		{
		}
		
		public TweetItem (string username, string tweet, string imageURL)
		{
			this.Username = username;
			this.Tweet = tweet;
			this.ImageURL = imageURL;
		}
		
		public string Username { get; set; }
		
		public string Tweet { get; set; }
		
		public string ImageURL { get; set; }
	}
}

