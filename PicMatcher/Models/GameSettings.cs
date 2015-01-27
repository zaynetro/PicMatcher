using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PicMatcher
{
	public class GameSettings
	{
		public bool IsLoaded = false;

		public event EventHandler Loaded;
		public event EventHandler Changed;

		protected virtual void OnLoaded(EventArgs e) {
			IsLoaded = true;

			if (Loaded != null)
				Loaded (this, e);
		}

		public virtual void OnChanged(EventArgs e) {
			if (Changed != null)
				Changed (this, e);
		}

		public GameSettings () {
			Load ();
			SelectedCategories = new List<Category> ();
		}

		public async void Load() {
			var catTask = Fetcher.GetObject<ObservableCollection<Category>> (Category.FormUri());
			var langTask = Fetcher.GetObject<ObservableCollection<Language>> (Language.FormUri());

			await Task.WhenAll (catTask, langTask);

			Categories = catTask.Result;
			Languages = langTask.Result;

			// TODO: Save selected choice
			SelectCategory (Categories [0]);
			SelectedLanguage = Languages [0];

			OnLoaded (EventArgs.Empty);
		}

		public ObservableCollection<Language> Languages { get; set; }

		public ObservableCollection<Category> Categories { get; set; }

		public Language SelectedLanguage { get; set; }

		public List<Category> SelectedCategories { get; set; }

		public void SelectCategory(Category cat) {
			// Don't add if already in the list
			if (SelectedCategories.IndexOf (cat) > -1)
				return;

			SelectedCategories.Add (cat);
		}

		public void DeselectCategory(Category cat) {
			var index = SelectedCategories.IndexOf (cat);
			// Don't remove if not in the list
			if (index == -1)
				return;

			SelectedCategories.RemoveAt (index);
		}
	}
}

