using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace PicMatcher
{
	public class SettingsPage : ContentPage
	{
		GameSettings _settings;
		Dictionary<string, Category> _catsHash = new Dictionary<string, Category> (); 
		Dictionary<string, Language> _langHash = new Dictionary<string, Language> ();
		int _catsSelected = 0;

		public SettingsPage () {}

		public SettingsPage (GameSettings Settings) {

			_settings = Settings;

			// Categories table section
			var catSection = new TableSection ("Categories");
			// Populate section with switches
			foreach (Category cat in Settings.Categories) {
				_catsHash.Add (cat.Name, cat);
				var cell = new SwitchCell {
					Text = cat.Name
				};
				cell.OnChanged += SwitchCellChanged;
				if (cat.IsOn) cell.On = true;
				catSection.Add (cell);
			}

			// When no category is selected, select first one
			if (_catsSelected == 0)
				((SwitchCell)catSection [0]).On = true;

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
			foreach (Language lang in Settings.Languages) {
				_langHash.Add (lang.Name, lang);
				languagePicker.Items.Add (lang.Name);
			}
			languagePicker.SelectedIndex = 0;

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
			// Get Category position
			var catIndex = _settings.Categories.IndexOf (_catsHash [cell.Text]);
			// Update IsEnabled field
			var isOn = cell.On;
			_settings.Categories [catIndex].IsOn = isOn;

			// Count number of selected categories
			if (isOn)
				_catsSelected++;
			else
				_catsSelected--;
		}

		bool CanLeave() {
			if(_catsSelected == 0) {
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

