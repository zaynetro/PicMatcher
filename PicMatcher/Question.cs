using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net;

namespace PicMatcher
{
	public class Question
	{

//		private Random rnd = new Random();

		/**
		 * Urls to correct and wrong icons
		 * TODO: use a better way to grade the question
		 */
		public static string CorrectImg = "https://cdn3.iconfinder.com/data/icons/shadcon/512/circle-tick-256.png";
		public static string WrongImg = "https://cdn1.iconfinder.com/data/icons/toolbar-signs/512/no-256.png";

		public Question () {}

		public bool IsCorrectAnswer (string answer) {
			return answer.ToUpper() == Answer.Name.ToUpper();
		}

		public static async Task<Question> Load() {
			string uri = "http://10.0.2.2:3000/api/question?cat=1&lang=en";
			var client = new HttpClient ();
			using (var response = await client.GetAsync (uri)) {
				if (!response.IsSuccessStatusCode)
					throw new HttpRequestException ();

				var content = response.Content;
				var jsonStr = await content.ReadAsStringAsync ();
				return JsonConvert.DeserializeObject<Question> (jsonStr); 
			}

		}

		public int Question_id { set; get; }

		public string Name { set; get; }

		public string Picture { set; get; }

		public Answer Answer { set; get; }

		public string Category { set; get; }

		public ObservableCollection<Answer> Answers { set; get; }
	}
}

