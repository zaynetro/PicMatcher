using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace PicMatcher
{
	public class Fetcher
	{
		public static string BaseUrl = "http://10.0.2.2:3000/api/";

		public Fetcher () {}

		public static async Task<string> GetString (string uri) {
			var client = new HttpClient ();
			using (var response = await client.GetAsync (BaseUrl + uri)) {
				if (!response.IsSuccessStatusCode)
					throw new HttpRequestException ();

				var content = response.Content;
				return await content.ReadAsStringAsync ();

			}
		}

		public static async Task<T> GetObject<T> (string uri) {
			var jsonStr = await GetString (uri);
			return JsonConvert.DeserializeObject<T> (jsonStr); 
		}
	}
}

