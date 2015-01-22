using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Diagnostics;

namespace PicMatcher
{
	public class Game
	{
		private ObservableCollection<Question> questions = new ObservableCollection<Question>();

		private int current = 0;
		private bool IsStatPage = true;
		public static int PerRound = 5;

		public GameStats Stats;

		public GameSettings Settings;

		public event EventHandler Added;
		public event EventHandler Error;
		public event EventHandler NextPage;

		protected virtual void OnAdded(EventArgs e) {
			if (Added != null)
				Added (this, e);
		}

		protected virtual void OnError(EventArgs e) {
			if (Error != null)
				Error (this, e);
		}

		protected virtual void OnNextPage(EventArgs e) {
			if (NextPage != null)
				NextPage (this, e);
		}

		public Game () {
			Stats = new GameStats ();
			Settings = new GameSettings ();
			LoadAndAdd ();
		}

		/**
		 * Return next question
		 */
		public ContentPage Next() {
			if (IsStatPage) {
				IsStatPage = false;
				return new StatsPage (Stats);
			}

			if (questions.Count == 0)
				throw new Exception ("No more questions");

			var isRoundEnd = (current + 1) % PerRound == 0;

			if (isRoundEnd)
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
				Debug.WriteLine (ex);
				OnError (args);
			}
		}
	}

	public class ErrorEventArgs : EventArgs
	{
		public string Message { get; set; }
	}
}

