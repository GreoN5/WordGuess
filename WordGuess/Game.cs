using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Text;

namespace WordGuess
{
    public class Game
    {
        private Player _player;
        private Words _words;
        private int _numberOfTries;
        private string _randomWord;
        private PlayerContext _playerContext = new PlayerContext();

        public Game(Words words, Player player, int numberOfTries)
        {
            this._numberOfTries = numberOfTries;
            this._words = words;
            this._player = player;
        }

        public void GuessTheWord()
        {
            StringBuilder builder = new StringBuilder();
            this._randomWord = this._words.GetRandomWord();

            string wordInProgress;

            int numberOfLetters = this._randomWord.Length - 1;
            ShowWordWithFirstLetter(this._randomWord);

            while (true)
            {
                if (this._numberOfTries <= 0)
                {
                    Console.WriteLine($"Game over! The word was \"{this._randomWord}\" and you didn't guess it!");

                    if (this._player.Points > 0)
                    {
                        this._player.Points--;

                        this._playerContext.Update(_player);
                        this._playerContext.SaveChanges();
                    }

                    return;
                }
                else
                {

                    if (numberOfLetters <= 0)
                    {
                        // if there are no letters left then you have won the game
                        Console.WriteLine($"You guessed the word! It is \"{this._randomWord}\"!");

                        this._player.Points += 2;

                        this._playerContext.Update(_player);
                        this._playerContext.SaveChanges();

                        return;
                    }
                    else
                    {
                        Console.Write($"The word has {numberOfLetters} remaining letters and you have {this._numberOfTries}" +
                            $" more tries to guess it! Guess a letter: ");
                        char letter = char.Parse(Console.ReadLine());

                        wordInProgress = WordInProgress(this._randomWord.ToCharArray(), letter, builder);

                        if (letter == '0' ||
                            letter == '1' ||
                            letter == '2' ||
                            letter == '3' ||
                            letter == '4' ||
                            letter == '5' ||
                            letter == '6' ||
                            letter == '7' ||
                            letter == '8' ||
                            letter == '9')
                        {
                            Console.WriteLine("Invalid letter! Please try again...");

                            continue;
                        }
                        else
                        {
                            if (CheckForContainingLetters(wordInProgress.ToCharArray(), letter))
                            {
                                numberOfLetters--;
                                ShowProgressOfWord(wordInProgress);
                            }
                            else
                            {
                                this._numberOfTries--;
                                ShowProgressOfWord(wordInProgress);
                            }
                        }
                    }
                }
            }
        }

        public void AddNewWordToTheGame(string word)
        {
            bool containsDigit = word.Any(char.IsDigit); // the word must not have any digits

            if (containsDigit == false && word.Length >= 3) // the word must be at least 3 letters long to be considered as a valid word
                this._words.ListOfWords.Add(word);
        }

        private bool CheckForContainingLetters(char[] letters, char letter)
        {
            if (letters.Contains(letter)) // checks if the letter is in the char array
                return true;
            else
                return false;
        }

        private void ShowWordWithFirstLetter(string word)
        {
            for (int i = 0; i < word.Length; i++)
            { // shows only the first letter of the word
                if (i == 0)
                    Console.Write($"{word[i]} ");
                else
                    Console.Write("_ "); // other letters are "empty"
            }

            Console.WriteLine();
        }

        private void ShowProgressOfWord(string word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] != '_')
                    Console.Write($"{word[i]} ");
                else
                    Console.Write("_ ");
            }

            Console.WriteLine();
        }

        private string WordInProgress(char[] letters, char letter, StringBuilder builder)
        {
            int indexOfLetter = Array.IndexOf(letters, letter);

            if (builder.Length == letters.Length)
            { // if the builder has a length of the array of chars
                for (int i = 0; i < builder.Length; i++)
                { // it will loop through the builder and find the index of the letter
                    if (builder[i] == '_')
                    {
                        if (i == indexOfLetter)
                        {
                            builder.Replace('_', letter, indexOfLetter, 1); // replaces the letter
                        }

                        if (i == indexOfLetter + 1 && letters[indexOfLetter + 1] == letter)
                        {
                            builder.Replace('_', letter, indexOfLetter + 1, 1); // replaces the letter
                        }
                    }
                }
            }
            else
            {// for the first time assigning the builder to the word(char array) it won't have the length of the word
                for (int i = 0; i < letters.Length; i++)
                {  // makes a new stringbuilder
                    if (i == indexOfLetter)
                        builder.Append(letter);
                    else if (i == 0)
                        builder.Append(letters[0]);
                    else
                        builder.Append('_');
                }
            }

            return builder.ToString();
        }

        private int GetRepeatedLetters(char[] letters, char letter)
        {
            int count = 0;

            for (int i = 0; i < letters.Length; i++)
            {
                if (letters[i] == letter)
                    count++;
            }

            return count;
        }
    }
}
