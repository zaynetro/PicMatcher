using System;
using Xamarin.Forms;

namespace PicMatcher
{
	public class StatsPage : ContentPage
	{

		GameStats _stats;
			
		public StatsPage () {}

		public StatsPage (GameStats Stats)
		{
			_stats = Stats;
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
					ForwardBtn,
					new Label {
						Text = "Or swipe from right"
					},
					SettingsBtn
				}
			};

			Content = StatsLayout;
		}

		void ForwardClicked(object sender, EventArgs e) {
			var Parent = (PicMatcher)this.Parent;
			Parent.NextAndRemove (this);
			_stats.Erase ();
		}

		void OpenSettings(object sender, EventArgs e) {
			Navigation.PushModalAsync (new SettingsPage ());
		}
	}
}

