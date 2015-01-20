using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace PicMatcher
{
	public class Game
	{
		private ObservableCollection<Question> questions = new ObservableCollection<Question>();

		private int current = 0;
		public static int PerRound = 5;

		public GameStats Stats;

		public event EventHandler Added;
		public event EventHandler Error;

		protected virtual void OnAdded(EventArgs e) {
			if (Added != null)
				Added (this, e);
		}

		protected virtual void OnError(EventArgs e) {
			if (Error != null)
				Error (this, e);
		}

		public Game () {
			Stats = new GameStats {
				Correct = 0,
				Mistakes = 0
			};
			LoadAndAdd ();
		}

		/**
		 * Return next question
		 */
		public ContentPage Next() {
			if (questions.Count == 0)
				throw new Exception ("No more questions");

			var tmp = (current + 1) % PerRound;

			if (tmp == 0)
				return new StatsPage (Stats);

			return new QuestionPage (questions [questions.Count - 1]);
		}

		public async void LoadAndAdd() {
			try {
				var q = await Fetcher.GetObject<Question> (Question.FormUri(1, "en"));
				q.Correct += (object sender, EventArgs e) => {
					current++;
					Stats.Correct++;
				};

				q.Mistake += (object sender, EventArgs e) => {
					current++;
					Stats.Mistakes++;
				};
				questions.Add (q);
				OnAdded (EventArgs.Empty);
			} catch (Exception ex) {
				var args = new ErrorEventArgs ();
				args.Message = ex.ToString();
				OnError (args);
			}
		}
	}

	public class ErrorEventArgs : EventArgs
	{
		public string Message { get; set; }
	}
}

