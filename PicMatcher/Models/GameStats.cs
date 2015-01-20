using System;
using System.ComponentModel;

namespace PicMatcher
{
	public class GameStats : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public GameStats () {
			Correct = 0;
			Mistakes = 0;
			Total = 0;
		}

		int _correct;
		public int Correct {
			get { return _correct; }
			set {
				if (value == _correct) {
					return;
				}
				_correct = value;
				OnPropertyChanged ("Correct");

				Total = _mistakes + _correct;
				OnPropertyChanged ("Total");
			}
		}

		int _mistakes;
		public int Mistakes {
			get { return _mistakes; }
			set {
				if (value == _mistakes) {
					return;
				}

				_mistakes = value;
				OnPropertyChanged ("Mistakes");

				Total = _mistakes + _correct;
				OnPropertyChanged ("Total");
			}
		}

		public int Total { get; set; }

		void OnPropertyChanged(string propertyName = null) {
			var handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}

