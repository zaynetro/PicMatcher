using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PicMatcher
{
	public class Question
	{

		private Random rnd = new Random();

		/**
		 * Urls to correct and wrong icons
		 * TODO: use a better way to grade the question
		 */
		public static string CorrectImg = "https://cdn3.iconfinder.com/data/icons/shadcon/512/circle-tick-256.png";
		public static string WrongImg = "https://cdn1.iconfinder.com/data/icons/toolbar-signs/512/no-256.png";

		public Question () {}

		public Question (string category) {}

		public bool IsCorrectAnswer (string answer) {
			return answer.ToUpper() == Answer.ToUpper();
		}

		static Question() 
		{
			/**
			 * All questions
			 * TODO: load them dynamically from the server
			 */

			All = new ObservableCollection<Question> {
				new Question {
					Answer = "Orange",
					Category = "Fruit",
					Image = "http://pngimg.com/upload/small/orange_PNG805.png"
				},

				new Question {
					Answer = "Apple",
					Category = "Fruit",
					Image = "http://www.kimmelorchard.org/images/icon_apple_braeburn.png"
				},

				new Question {
					Answer = "Watermelon",
					Category = "Fruit",
					Image = "http://icons.iconarchive.com/icons/fi3ur/fruitsalad/256/watermelon-icon.png"
				},

				new Question {
					Answer = "Strawberry",
					Category = "Fruit",
					Image = "http://icons.iconarchive.com/icons/ergosign/free-spring/256/strawberry-icon.png"
				},

				new Question {
					Answer = "Peach",
					Category = "Fruit",
					Image = "http://www.bellybytes.com/recipe/bread/images/Peach.png"
				}
			};

		}

		/**
		 * Get answers, where one is correct
		 * TODO: shuffle answers
		 */
		public string[] GetAnswers()
		{
			var answers = new string[4];
			var k = rnd.Next (4);
			for (var i = 0; i < 4; i++) {
				if (i == k) answers [i] = this.Answer;
				else answers [i] = Question.All [i].Answer;
			}
			return answers;
		}

		public string Answer { set; get; }

		public string Category { set; get; }

		public string Image { set; get; }

		public static IList<Question> All { set; get; }
	}
}

