using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace PicMatcher
{

	public class Question
	{

		public event EventHandler Correct;
		public event EventHandler Mistake;

		protected virtual void OnCorrect(EventArgs e) {
			if (Correct != null)
				Correct (this, e);
		}

		protected virtual void OnMistake(EventArgs e) {
			if (Mistake != null)
				Mistake (this, e);
		}

		public static string FormUri (int cat, string lang) {
			return String.Format ("question?cat={0}&lang={1}", cat, lang);
		}

		public static string FormUri (List<Category> cats, Language lang) {
			var catsParams = "";
			foreach (Category cat in cats) {
				catsParams += "&cat[]=" + cat.Category_id;
			}
			catsParams = catsParams.Substring (1);
			return String.Format ("question?{0}&lang={1}", catsParams, lang.Language_id);
		}

		/**
		 * Urls to correct and wrong icons
		 */
		public static string CorrectImg = "https://cdn2.iconfinder.com/data/icons/free-basic-icon-set-2/300/11-256.png";
		public static string WrongImg = "https://cdn2.iconfinder.com/data/icons/free-basic-icon-set-2/300/17-256.png";

		public Question () {}

		public bool IsCorrectAnswer (string answer) {
			var isCorrect = answer.ToUpper() == Answer.Name.ToUpper();

			if (isCorrect)
				OnCorrect (EventArgs.Empty);
			else
				OnMistake (EventArgs.Empty);

			return isCorrect;
		}

		public int Question_id { set; get; }

		public string Name { set; get; }

		public string Picture { set; get; }

		public Answer Answer { set; get; }

		public string Category { set; get; }

		public ObservableCollection<Answer> Answers { set; get; }
	}
}

