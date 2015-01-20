using System;
using Xamarin.Forms;

namespace PicMatcher
{
	public class StatsPage : ContentPage
	{
		public StatsPage () {}

		public StatsPage (GameStats Stats)
		{
			BindingContext = Stats;

			var CorrectLabel = new Label {
				FontSize = 15
			};
			CorrectLabel.SetBinding(Label.TextProperty, "Correct");

			var MistakesLabel = new Label {
				FontSize = 15
			};
			MistakesLabel.SetBinding(Label.TextProperty, "Mistakes");

			var TotalLabel = new Label {
				FontSize = 15
			};
			TotalLabel.SetBinding(Label.TextProperty, "Total");

			var ForwardBtn = new Button {
				Text = "Go forward"
			};

			ForwardBtn.Clicked += ForwardClicked;

			var StatsLayout = new StackLayout {
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Children = {
					new Label {
						Text = "Stats",
						FontSize = 20
					},
					new Label {
						Text = "Correct"
					},
					CorrectLabel,
					new Label {
						Text = "Mistakes"
					},
					MistakesLabel,
					new Label {
						Text = "Total"
					},
					TotalLabel,
					ForwardBtn
				}
			};

			Content = StatsLayout;
		}

		void ForwardClicked(object sender, EventArgs e) {
			var Parent = (PicMatcher)this.Parent;
			Parent.NextPage ();
		}
	}
}

