using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PicMatcher
{
	public delegate void AddEventHandler(object sender, EventArgs e);

	public class Game
	{
		private IList<Question> questions = new ObservableCollection<Question>();

		private int current = 0;

		public event AddEventHandler Added;

		protected virtual void OnAdded(EventArgs e) {
			if (Added != null)
				Added (this, e);
		}

		public Game ()
		{
			LoadAndAdd ();
		}

		/**
		 * Return next question
		 */
		public Question Next() {
			if (questions.Count == 0)
				throw new Exception ("No more questions");

			return questions [current++];
		}

		public async void LoadAndAdd() {
			questions.Add (await Question.Load ());
			OnAdded (EventArgs.Empty);
		}
	}
}

