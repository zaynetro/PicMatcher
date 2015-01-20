using System;
using System.Collections.ObjectModel;

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

		/**
		 * Urls to correct and wrong icons
		 * TODO: use a better way to grade the question
		 */
		public static string CorrectImg = "https://cdn3.iconfinder.com/data/icons/shadcon/512/circle-tick-256.png";
		public static string WrongImg = "https://cdn1.iconfinder.com/data/icons/toolbar-signs/512/no-256.png";

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

