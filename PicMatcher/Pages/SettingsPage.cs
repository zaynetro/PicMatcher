using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace PicMatcher
{
	public class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
			var tableView = new TableView {
				Intent = TableIntent.Settings,
				Root = new TableRoot ("Settings") {
					new TableSection ("Categories") {
						new SwitchCell {
							Text = "Fruits"
						},
						new SwitchCell {
							Text = "Furniture"
						},
						new SwitchCell {
							Text = "Other"
						},
						new SwitchCell {
							Text = "Other"
						}
					},
					new TableSection ("Game type") {
						new SwitchCell {
							Text = "Guess picture"
						},
						new SwitchCell {
							Text = "Guess word"
						},
						new SwitchCell {
							Text = "Match"
						}
					}
				}
			};

			Dictionary<string, int> pickerData = new Dictionary<string, int>
			{
				{ "English", 1 },
				{ "Russian", 2 },
				{ "Finnish", 3 }
			};

			var languagePicker = new Picker {
				Title = "Language",
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			foreach (string language in pickerData.Keys) {
				languagePicker.Items.Add (language);
			}

			var languageGroup = new StackLayout {
				Padding = 20,
				Children = {
					languagePicker
				}
			};

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					languageGroup,
					tableView
				}
			};
		}
	}
}

