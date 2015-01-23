using System;
using Xamarin.Forms;

namespace PicMatcher
{
	public class StatsPage : ContentPage
	{

		GameStats _stats;
		GameSettings _settings;
			
		public StatsPage () {}

		public StatsPage (GameStats Stats, GameSettings Settings)
		{
			_stats = Stats;
			_settings = Settings;
			BindingContext = _stats;

			var CorrectLabel = new Label {
				FontSize = 40,
				TextColor = Color.Green
			};
			CorrectLabel.SetBinding(Label.TextProperty, "Correct");

			var TotalLabel = new Label {
				FontSize = 40
			};
			TotalLabel.SetBinding(Label.TextProperty, "Total");

			var ForwardBtn = new Button {
				Text = "Go forward"
			};
			ForwardBtn.Clicked += ForwardClicked;

			var SettingsBtn = new Button {
				Text = "Configure"
			};
			SettingsBtn.Clicked += OpenSettings;

			var ActivityBar = new ActivityIndicator {
				IsRunning = false
			};

			var StatsLayout = new StackLayout {
				HorizontalOptions = LayoutOptions.Center,
				Children = {
					new Label {
						Text = "Score",
						FontSize = 50
					},
					new StackLayout {
						HorizontalOptions = LayoutOptions.Center,
						Orientation = StackOrientation.Horizontal,
						Children = {
							CorrectLabel,
							new Label {
								Text = "/",
								FontSize = 40
							},
							TotalLabel,
						}
					},
					ActivityBar
				}
			};

			// When settings are not loaded, wait for them
			if (!_settings.IsLoaded) {
				ActivityBar.IsRunning = true;

				Settings.Loaded += (object sender, EventArgs e) => {
					// TODO: remove repetition
					StatsLayout.Children.Add (ForwardBtn);
					StatsLayout.Children.Add (new Label {
						Text = "Or swipe from right"
					});
					StatsLayout.Children.Add (SettingsBtn);
					ActivityBar.IsRunning = false;
				};

			} else {
				// TODO: remove repetition
				StatsLayout.Children.Add (ForwardBtn);
				StatsLayout.Children.Add (new Label {
					Text = "Or swipe from right"
				});
				StatsLayout.Children.Add (SettingsBtn);
			}


			Content = StatsLayout;
		}

		void ForwardClicked(object sender, EventArgs e) {
			var Parent = (PicMatcher)this.Parent;
			Parent.NextAndRemove (this);
			_stats.Erase ();
		}

		void OpenSettings(object sender, EventArgs e) {
			Navigation.PushModalAsync (new SettingsPage (_settings));
		}
	}
}

