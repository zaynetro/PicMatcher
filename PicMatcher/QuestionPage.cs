using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace PicMatcher
{
	public class QuestionPage : ContentPage
	{
		private Question question { get; set; }
		private Image Img { get; set; }

		public QuestionPage () {}

		public QuestionPage (Question q)
		{
			/**
			 * Init single question page
			 * Includes:
			 *   - Question
			 *   - Image
			 *   - Possible answers
			 */

			question = q;

			Img = new Image {
				Source = QuestionPage.WrapImage(question.Image),
				MinimumWidthRequest = 256,
				MinimumHeightRequest = 256
			};

			var answers = question.GetAnswers ();
			var buttons = new Button[4];

			var AnswersLayout = new Grid {
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				RowDefinitions = {
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = GridLength.Auto },
				},
				ColumnDefinitions = {
					new ColumnDefinition { Width = GridLength.Auto },
					new ColumnDefinition { Width = GridLength.Auto }
				}
			};

			for (var i = 0; i < 2; i++) {
				for (var j = 0; j < 2; j++) {
					var k = i * 2 + j;
					buttons [k] = new Button { Text = answers [k] };
					buttons [k].Clicked += onAnswerClicked;
					AnswersLayout.Children.Add (buttons [k], i, j);
				}
			}

			var QuestionLayout = new StackLayout {
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = {
					new Label { 
						Text = "What is on the picture?",
//						FontSize = Device.GetNamedSize(NamedSize.Large),
						FontSize = 24,
						HorizontalOptions = LayoutOptions.CenterAndExpand
					},
					Img,
					AnswersLayout
				}
			};

			this.Content = QuestionLayout;
		}

		/**
		 * Handle answer click
		 */
		void onAnswerClicked(object Sender, EventArgs e) {
			var button = (Button)Sender;
			if (question.IsCorrectAnswer (button.Text)) {
				// Mark as correct
				Img.Source = QuestionPage.WrapImage(Question.CorrectImg);
			} else {
				// Mark as wrong
				Img.Source = QuestionPage.WrapImage(Question.WrongImg);
			}

			var Parent = (CarouselPage)this.Parent;

			Action RemovePrevious = async () => {
				await Task.Delay (500);
				Parent.Children.Remove (this);
			};

			Action NextPage = async () => {
				await Task.Delay(500);
				Parent.CurrentPage = Parent.Children[2];
				RemovePrevious();
			};

			var count = Parent.Children.Count;
			if(Parent.Children.Count > 2)
				Device.BeginInvokeOnMainThread(NextPage);
		}

		/**
		 * Wrap image url into UriImageSource
		 * TODO: move to separate file
		 */
		private static UriImageSource WrapImage(string url) {
			return new UriImageSource {
				Uri = new Uri(url),
				CachingEnabled = true,
				CacheValidity = new TimeSpan(5, 0, 0, 0)
			};
		}
	}
}

