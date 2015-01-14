using System;
using Xamarin.Forms;

namespace PicMatcher
{
	public class QuizPage : CarouselPage
	{
		public QuizPage ()
		{	
			/**
			 * Actual game page
			 */

			this.Title = "Quiz";

			var game = new Game ();

			for (var i = 0; i < game.Total; i += 1) {
				this.Children.Add (new QuestionPage (game.Next (), i));
			}
		}
	}
}

