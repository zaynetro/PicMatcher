using System;
using Xamarin.Forms;

namespace PicMatcher
{
	public delegate void EventHandler(object sender, EventArgs e);

	public class PicMatcher : CarouselPage
	{
		private Game game;

		public PicMatcher ()
		{
			/**
			 * Init app carousel:
			 *   - Add home page
			 *   - Init game
			 */
			this.Title = "PicMatcher";
			this.Children.Add(new HomePage());

			game = new Game ();

			game.Added += (object sender, EventArgs e) => {
				this.Children.Add (game.Next());
			};

			game.Error += async (object sender, EventArgs e) => {
				var answer = await DisplayAlert("What a shame", "Something went wrong", "Try again", "Not now");
				if(answer) game.LoadAndAdd();
			};
		}

		public void NextPage() {
			var i = Children.IndexOf(CurrentPage);
			if(i < Children.Count - 1) CurrentPage = Children [i + 1];
			NextQuestion ();
		}

		public void NextQuestion () {
			game.LoadAndAdd();
		}
	}
}

