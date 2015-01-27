using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace PicMatcher
{
	public class QuestionPage : ContentPage
	{
		Question _question { get; set; }
		Image _img { get; set; }
		Button _correctBtn { get; set; }

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

			_question = q;

			_img = new Image {
				Source = QuestionPage.WrapImage(_question.Picture),
				WidthRequest = 256,
				HeightRequest = 256
			};

			var answers = _question.Answers;
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
					buttons [k] = new Button {
						WidthRequest = 140,
						Text = answers [k].Name
					};
					buttons [k].Clicked += onAnswerClicked;

					if (_question.Answer.Answer_id == answers [k].Answer_id) {
						_correctBtn = buttons [k];
					}

					AnswersLayout.Children.Add (buttons [k], i, j);
				}
			}

			var QuestionLayout = new StackLayout {
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = {
					_img,
					AnswersLayout
				}
			};

			Content = QuestionLayout;
		}

		/**
		 * Handle answer click
		 */
		void onAnswerClicked(object Sender, EventArgs e) {
			var button = (Button)Sender;
			var delay = 400;

			if (_question.IsCorrectAnswer (button.Text)) {
				// Mark as correct
				_img.Source = QuestionPage.WrapImage(Question.CorrectImg);
			} else {
				// Mark as wrong
				_img.Source = QuestionPage.WrapImage(Question.WrongImg);
				_correctBtn.BackgroundColor = Color.Green;
				delay = 1000;
			}

			var Parent = (PicMatcher)this.Parent;
			Action NextPage = async () => {
				await Task.Delay(delay);
				Parent.NextAndRemove (this);
			};

			Task.Run(NextPage);
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

