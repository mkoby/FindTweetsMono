using System;
using System.Json;
using System.Runtime.Serialization;

namespace FindTweetsMono
{
	[DataContract]
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
		
		[DataMember(Name = "from_user")]
		public string Username { get; set; }
		
		[DataMember(Name = "text")]
		public string Tweet { get; set; }
		
		[DataMember(Name = "profile_image_url")]
		public string ImageURL { get; set; }
	}
}

