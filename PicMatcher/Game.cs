using System;
using System.Collections.Generic;

namespace PicMatcher
{
	public class Game
	{
		private IList<Question> questions;
		private Random rnd = new Random ();

		public int Total = 0;
		private List<int> used = new List<int> ();

		public Game ()
		{
			/**
			 * Init game
			 */
			questions = Question.All;
			Total = questions.Count;
		}

		/**
		 * Return next question
		 */
		public Question Next()
		{
			if (questions.Count == 0)
				throw new Exception ("Questions collection is empty");

			int i;
			do {
				i = rnd.Next (questions.Count);
			} while(used.Contains (i) == true);

			used.Add (i);

			var q = questions [i];
			return q;
		}
	}
}

