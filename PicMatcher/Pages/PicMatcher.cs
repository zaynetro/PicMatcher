﻿using System;
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

			// Remove all children following the current page
			game.Clean += (object sender, EventArgs e) => {
				var index = Children.IndexOf(CurrentPage);
				var length = Children.Count;
				for(var i = index + 1; i < length; i++) {
					Children.RemoveAt (i);
				}
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

		protected override bool OnBackButtonPressed () {
			// When game is not finished, ask user to keep playing
//			if (!game.IsGameFinished) {
//				AskToExit ();
//				return true;
//			}
			return base.OnBackButtonPressed ();
		}

//		async void AskToExit() {
//			var answer = await DisplayAlert ("Game is not finished", 
//				"Are you sure you want to leave?", 
//				"Leave", "Keep playing");
//			if (answer) {
//				// Explicitly set to true, so app can be closed
//				game.IsGameFinished = true;
//				SendBackButtonPressed ();
//			}
//		}
	}
}

