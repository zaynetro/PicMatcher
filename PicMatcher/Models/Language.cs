using System;

namespace PicMatcher
{
	public class Language
	{
		public static string FormUri () {
			return "language";
		}

		public Language () {}

		public int Language_id { get; set; }

		public string Name { get; set; }
	}
}

