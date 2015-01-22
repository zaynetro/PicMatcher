using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace PicMatcher
{
	public delegate void EventHandler(object sender, EventArgs e);

	public class PicMatcher : CarouselPage
	{
		private Game game;

		public PicMatcher ()
		{
			this.Title = "PicMatcher";
//			this.Children.Add(new HomePage());

			game = new Game ();
			this.Children.Add (game.Next());

			game.Added += (object sender, EventArgs e) => {
				this.Children.Add (game.Next());
			};

			game.NextPage += (object sender, EventArgs e) => {
				NextPage();
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

		public void NextAndRemove (ContentPage page) {
			Action NextAndRemoveTask = async () => {
				NextPage ();
				await Task.Delay (500);
				Children.Remove (page);
			};

			Device.BeginInvokeOnMainThread(NextAndRemoveTask);
		}
	}
}

