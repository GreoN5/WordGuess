using System;
using System.Collections.Generic;

namespace WordGuess
{
	public class Words
	{
		public List<string> ListOfWords { get; private set; } = new List<string>();

		public Words() { }

		public string GetRandomWord()
		{
			Random random = new Random();

			for (int i = 0; i < this.ListOfWords.Count; i++)
			{
				if (this.ListOfWords[i] != null)
				{
					//get a random string value from the list and return it
					this.ListOfWords[i] = this.ListOfWords[random.Next(0, this.ListOfWords.Count)];

					return this.ListOfWords[i];
				}
			}

			return default;
		}
	}
}
