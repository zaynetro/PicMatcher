using System;
using System.Collections.ObjectModel;

namespace PicMatcher
{
	public class GameSettings
	{
		public GameSettings () {}

		public ObservableCollection<Language> Languages { get; set; }

		public ObservableCollection<Category> Categories { get; set; }
	}
}

