using System;

using Xamarin.Forms;

namespace PicMatcher
{
	public class PicMatcher : NavigationPage
	{

		public PicMatcher ()
		{
			/**
			 * Create main navigation page with launch screen
			 */

			var button = new Button { Text = "Launch" };

			button.Clicked += (object sender, EventArgs e) => {
				this.PushAsync(new QuizPage());
			};

			this.PushAsync (new ContentPage {
				Title = "PicMatcher",
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.CenterAndExpand,
					Children = {
						button
					}
				}
			});
		}

	}
}

