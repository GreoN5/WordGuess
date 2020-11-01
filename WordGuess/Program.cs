using System;
using System.Linq;

namespace WordGuess
{
	public class Program
	{
		public static Game guessTheWord = null;
		public static Player defaultPlayer = null;

		public static void Main(string[] args)
		{
			
			PlayerContext playerContext = new PlayerContext();
			string firstAction = null;

			while (true)
			{
				Console.Write("Register/Login: ");
				firstAction = Console.ReadLine();

				if (firstAction == "register" || firstAction == "Register")
					break;
				else if (firstAction == "login" || firstAction == "Login")
					break;
				else
					continue;
			}

			Console.WriteLine();

			while (firstAction == "register" || firstAction == "Register")
			{
				Console.Write("Enter username: ");
				string username = Console.ReadLine();

				Console.Write("Enter password: ");
				string password = Console.ReadLine();

				if (playerContext.Players.Count() == 0)
				{
					defaultPlayer = new Player()
					{
						Username = username,
						Password = password,
						Points = 0
					};

					playerContext.Add(defaultPlayer);
					playerContext.SaveChanges();

					break;
				}

				foreach (var player in playerContext.Players)
				{
					if (username == player.Username)
					{
						Console.WriteLine("This username is already in use! Try another one.");

						break;
					}
					else
					{
						defaultPlayer = new Player()
						{
							Username = username,
							Password = password,
							Points = 0
						};

						playerContext.Add(defaultPlayer);
						playerContext.SaveChanges();
						
						break;
					}
				}

				if (defaultPlayer == null)
					continue;
				else
					break;
			}

			while (firstAction == "login" || firstAction == "Login")
			{
				Console.Write("Username: ");
				string username = Console.ReadLine();

				Console.Write("Password: ");
				string password = Console.ReadLine();

				defaultPlayer = playerContext.Players.Where(p => p.Username == username && p.Password == password).FirstOrDefault();

				break;
			}

			Console.WriteLine();

			while (true)
			{
				Console.Write("Start the game: ");
				string gameStart = Console.ReadLine();
				Console.WriteLine();

				if (gameStart == "start" || 
					gameStart == "Start" ||
					gameStart == "yes" || 
					gameStart == "Yes")
				{
					PlayGame();

					Console.Write("Do you want to play again?: ");
					string playAgain = Console.ReadLine();

					if (playAgain == "yes" || playAgain == "Yes")
					{
						continue;
					}
					else
					{
						Console.WriteLine("The game has stopped!");
						break;
					}
				}
				else
				{
					Console.WriteLine("The game is not started!");
					break;
				}
			}
		}

		private static void PlayGame()
		{
			guessTheWord = new Game(new Words(), defaultPlayer, 5);
			guessTheWord.AddNewWordToTheGame("train");
			guessTheWord.AddNewWordToTheGame("olive");
			guessTheWord.AddNewWordToTheGame("car");
			guessTheWord.AddNewWordToTheGame("parking");
			guessTheWord.AddNewWordToTheGame("ball");
			guessTheWord.AddNewWordToTheGame("2plus");

			guessTheWord.GuessTheWord();
		}
	}
}
