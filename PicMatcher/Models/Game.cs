using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Diagnostics;

namespace PicMatcher
{
	public class Game
	{
		private ObservableCollection<Question> questions = new ObservableCollection<Question>();

		int _current = 0;
		public static int PerRound = 5;
		public bool IsGameFinished = true;

		public GameStats Stats;
		public GameSettings Settings;

		public event EventHandler Added;
		public event EventHandler Error;
		public event EventHandler NextPage;
		public event EventHandler Clean;

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

		protected virtual void OnClean(EventArgs e) {
			if (Clean != null)
				Clean (this, e);
		}

		public Game () {
			Stats = new GameStats ();
			Settings = new GameSettings ();

			Settings.Loaded += (object sender, EventArgs e) => {
				LoadAndAdd ();
			};

			Settings.Changed += (object sender, EventArgs e) => {
				questions.Clear ();
				OnClean(EventArgs.Empty);
				PrepareForGame ();
				LoadAndAdd ();
			};
		}

		void PrepareForGame() {
			_current = 0;
			IsGameFinished = false;
		}

		/**
		 * Return next question
		 */
		public ContentPage Next() {
			if (_current >= PerRound)
				IsGameFinished = true;

			if (IsGameFinished) {
				PrepareForGame ();
				return new StatsPage (Stats, ref Settings);
			}

			_current++;

			// Handle no questions error
			if (questions.Count == 0)
				throw new Exception ("No more questions");

			return new QuestionPage (questions [questions.Count - 1]);
		}

		public async void LoadAndAdd() {
			try {
				var q = await Fetcher.GetObject<Question> (Question.FormUri(Settings.SelectedCategories, Settings.SelectedLanguage));
				q.Correct += (object sender, EventArgs e) => {
					Stats.Correct++;
					questions.Remove(q);
				};

				q.Mistake += (object sender, EventArgs e) => {
					Stats.Mistakes++;
					questions.Remove(q);
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

