using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace PicMatcher
{
	public class SettingsPage : ContentPage
	{
		GameSettings _settings;
		Dictionary<string, Category> _catsHash = new Dictionary<string, Category> (); 
		Dictionary<int, Language> _langHash = new Dictionary<int, Language> ();

		public SettingsPage () {}

		public SettingsPage (ref GameSettings Settings) {

			_settings = Settings;

			// Categories table section
			var catSection = new TableSection ("Categories");
			// Populate section with switches
			foreach (Category cat in Settings.Categories) {
				_catsHash.Add (cat.Name, cat);
				var cell = new SwitchCell {
					Text = cat.Name
				};
				// If category is selected, turn the switch on
				// TODO: optimize
				if (_settings.SelectedCategories.IndexOf (cat) > -1) {
					cell.On = true;
				}
				cell.OnChanged += SwitchCellChanged;
				catSection.Add (cell);
			}

			var tableView = new TableView {
				Intent = TableIntent.Settings,
				Root = new TableRoot ("Settings") {
					catSection
				}
			};

			// Languages picker
			var languagePicker = new Picker {
				Title = "Language",
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
			// Populate picker
			var i = 0;
			var selectedIndex = 0;
			foreach (Language lang in Settings.Languages) {
				_langHash.Add (i, lang);
				languagePicker.Items.Add (lang.Name);
				languagePicker.SelectedIndexChanged += LanguageSelected;

				if (lang.Equals (_settings.SelectedLanguage))
					selectedIndex = i;

				i++;
			}
			languagePicker.SelectedIndex = selectedIndex;

			var languageGroup = new StackLayout {
				Padding = 20,
				Children = {
					languagePicker
				}
			};

			var DoneBtn = new Button {
				Text = "Done"
			};
			DoneBtn.Clicked += (object sender, EventArgs e) => {
				if(CanLeave()) Navigation.PopModalAsync();
			};

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					languageGroup,
					tableView,
					DoneBtn
				}
			};
		}

		void SwitchCellChanged(object sender, EventArgs e) {
			var cell = (SwitchCell)sender;
			// Get Category
			var cat = _catsHash [cell.Text];

			// Count number of selected categories
			if (cell.On) {
				_settings.SelectCategory (cat);
			} else {
				_settings.DeselectCategory (cat);
			}
		}

		void LanguageSelected(object sender, EventArgs e) {
			var picker = (Picker)sender;
			_settings.SelectedLanguage = _langHash [picker.SelectedIndex];
		}

		bool CanLeave() {
			if(_settings.SelectedCategories.Count == 0) {
				DisplayAlert("No category selected", "Select at least one category", "OK");
				return false;
			}
			return true;
		}

		protected override bool OnBackButtonPressed () {
			// Return true if you don't want to leave page (wierd)
			if (!CanLeave()) return true;
			return base.OnBackButtonPressed ();
		}
	}
}

