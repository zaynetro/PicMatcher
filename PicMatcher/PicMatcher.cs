using System;

using Xamarin.Forms;

namespace PicMatcher
{
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
			game.LoadAndAdd ();

			game.Added += (object sender, EventArgs e) => {
				this.Children.Add (new QuestionPage (game.Next()));
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

