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
				FontSize = 30,
				TextColor = Color.Green
			};
			CorrectLabel.SetBinding(Label.TextProperty, "Correct");

			var TotalLabel = new Label {
				FontSize = 30
			};
			TotalLabel.SetBinding(Label.TextProperty, "Total");

			var ForwardBtn = new Button {
				Text = "Go forward"
			};
			ForwardBtn.Clicked += ForwardClicked;

			var StatsLayout = new StackLayout {
				HorizontalOptions = LayoutOptions.Center,
				Children = {
					new Label {
						Text = "Score",
						FontSize = 35
					},
					new StackLayout {
						HorizontalOptions = LayoutOptions.Center,
						Orientation = StackOrientation.Horizontal,
						Children = {
							CorrectLabel,
							new Label {
								Text = "/",
								FontSize = 30
							},
							TotalLabel,
						}
					},
					ForwardBtn
				}
			};

			Content = StatsLayout;
		}

		void ForwardClicked(object sender, EventArgs e) {
			var Parent = (PicMatcher)this.Parent;
			Parent.NextAndRemove (this);
			_stats.Erase ();
		}
	}
}

