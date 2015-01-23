using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PicMatcher
{
	public class GameSettings
	{
		public bool IsLoaded = false;

		public event EventHandler Loaded;

		protected virtual void OnLoaded(EventArgs e) {
			IsLoaded = true;

			if (Loaded != null)
				Loaded (this, e);
		}

		public GameSettings () {
			Load ();
		}

		public async void Load() {
			var catTask = Fetcher.GetObject<ObservableCollection<Category>> (Category.FormUri());
			var langTask = Fetcher.GetObject<ObservableCollection<Language>> (Language.FormUri());

			await Task.WhenAll (catTask, langTask);

			Categories = catTask.Result;
			Languages = langTask.Result;

			OnLoaded (EventArgs.Empty);
		}

		public ObservableCollection<Language> Languages { get; set; }

		public ObservableCollection<Category> Categories { get; set; }
	}
}

