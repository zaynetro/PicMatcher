using System;

namespace PicMatcher
{
	public class Category
	{
		public static string FormUri () {
			return "category";
		}

		public Category () {}

		public int Category_id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }
	}
}

