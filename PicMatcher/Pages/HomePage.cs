using System;
using Xamarin.Forms;

namespace PicMatcher 
{

	public class HomePage : ContentPage
	{
		public HomePage ()
		{
			var button = new Button { Text = "Launch" };

			button.Clicked += (object sender, EventArgs e) => {
				var Parent = (PicMatcher)this.Parent;
				Parent.NextPage();
			};

			this.Content = new StackLayout {
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = {
					button,
					new Label {
						Text = "Or swipe from the right",
						HorizontalOptions = LayoutOptions.CenterAndExpand
					}
				}
			};
		}
	}
}

