using System;

using Xamarin.Forms;

namespace PicMatcher
{
	public class PicMatcher : CarouselPage
	{

		public PicMatcher ()
		{
			/**
			 * Init app carousel:
			 *   - Add home page
			 *   - Add questions
			 */
			this.Title = "PicMatcher";
			this.Children.Add(new HomePage());

			var game = new Game ();

			// Start from 1 as first page is taken
			for (var i = 1; i <= game.Total; i += 1) {
				this.Children.Add (new QuestionPage (game.Next ()));
			}
		}

	}
}

