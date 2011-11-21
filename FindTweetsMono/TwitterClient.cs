using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Net;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace FindTweetsMono
{
	public class TwitterClient
	{
		string _TwitterSearchBaseURL = @"http://search.twitter.com/search.json?lang=en&q=";
		
		public TwitterClient()
		{
		}
		
		public TwitterClient (string BaseURL)
		{
			_TwitterSearchBaseURL = BaseURL;
		}
		
		public IList<TweetItem> SearchTwitterWithQuery(string query)
		{
			string searchURL = String.Format("{0}/{1}", _TwitterSearchBaseURL, query);
			//string jsonResponse = String.Empty;
			IList<TweetItem> output = null;
			
			ThreadStart webRequest = delegate() {
				output = CreateTweetItemsFromJson(MakeAPIRequest(searchURL));
			};
			
			Thread webThread = new Thread(webRequest);
			webThread.Start();
			webThread.Join();
			
			return output;
		}
		
		private string MakeAPIRequest(string urlString)
		{
			string output = String.Empty;
			Uri requestUrl = new Uri(urlString);
			WebRequest request = WebRequest.Create(requestUrl);
			request.ContentType = "application/json; charset=trf-8";
			var response = request.GetResponse();
			
			using(StreamReader r = new StreamReader(response.GetResponseStream()))
			{
				output = r.ReadToEnd();
			}
			
			return output;
		}
		
		private IList<TweetItem> CreateTweetItemsFromJson(string jsonResponse)
		{
			IList<TweetItem> output = new List<TweetItem>();
			StringReader r = new StringReader(jsonResponse);
			var json = JsonValue.Load(r);
			var results = json["results"];
			
			Parallel.For(0, results.Count, (i) => {
				JsonObject result = (JsonObject)results[i];
				TweetItem item = new TweetItem(result["from_user"], result["text"], result["profile_image_url"]);
				output.Add(item);
			});
			
			return output;
		}
	}
}

