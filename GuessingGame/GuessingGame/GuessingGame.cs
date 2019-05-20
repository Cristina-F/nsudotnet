using System;
using System.Collections.Generic;

namespace GuessingGame {
    internal class Guesser {
        private static void Main() {
            var guesser = new Guesser();
            guesser.Play();
        }

        private void Play() {
            Console.WriteLine("Please, enter your name(:");
            _userName = Console.ReadLine();
            var isCorrectAnswer = false;
            Console.WriteLine("Try to guess my number, time has gone))))))");
            _startTime = DateTime.Now;
            while (!isCorrectAnswer) {
                var userAnswer = Console.ReadLine();
                if (userAnswer != null && userAnswer.Equals(ExitCode)) {
                    Console.WriteLine("I apologize and finish.");
                    return;
                }

                var userNumber = GetUserNumber(userAnswer);
                isCorrectAnswer = IsCorrectAnswer(userNumber);
                if (!isCorrectAnswer) {
                    CheckNumberOfGuesses();
                }
            }
        }

        private void CheckNumberOfGuesses() {
            if (_guessingCounter % 4 != 0) return;
            var random = new Random();
            var i = random.Next(0, _motivatingPhrases.Length);
            Console.WriteLine($"{_userName}, {_motivatingPhrases[i]}");
        }

        private bool IsCorrectAnswer(int userNumber) {
            _guessingCounter++;
            if (userNumber.Equals(_randomNumber)) {
                _endTime = DateTime.Now;
                _history.Add(new KeyValuePair<int, Status>(userNumber, Status.CorrectNumber));
                PrintGameInfo();
                return true;
            }

            if (userNumber < _randomNumber) {
                _history.Add(new KeyValuePair<int, Status>(userNumber, Status.SmallNumber));
                Console.WriteLine(_comparisonPhrases[(int) Status.SmallNumber]);
            }
            else {
                _history.Add(new KeyValuePair<int, Status>(userNumber, Status.BigNumber));
                Console.WriteLine(_comparisonPhrases[(int) Status.BigNumber]);
            }

            return false;
        }

        private void PrintGameInfo() {
            Console.WriteLine($"Number of guessing attempts: {_guessingCounter}");
            foreach (var (userNumber, status) in _history) {
                Console.WriteLine($"{userNumber} - {_comparisonPhrases[(int) status]}");
            }

            var time = _endTime - _startTime;
            Console.WriteLine($"Play time:  {time.Minutes}:{time.Seconds}");
        }

        private int GetUserNumber(string userAnswer) {
            var userNumber = 0;
            try {
                userNumber = int.Parse(userAnswer);
                return userNumber;
            }
            catch (Exception) {
                Console.WriteLine($"{_userName}, you entered nonsense.....");
                GetUserNumber(Console.ReadLine());
            }

            return userNumber;
        }

        private Guesser() {
            var random = new Random();
            _randomNumber = random.Next(MinRandomNumber, MaxRandomNumber);
            Console.WriteLine($"Secret random number = {_randomNumber}");
        }

        private int _guessingCounter;
        private string _userName;
        private readonly int _randomNumber;
        private readonly List<KeyValuePair<int, Status>> _history = new List<KeyValuePair<int, Status>>();
        private DateTime _startTime;
        private DateTime _endTime;

        private const int MinRandomNumber = 0;
        private const int MaxRandomNumber = 51;
        private const string ExitCode = "q";

        private readonly string[] _comparisonPhrases = {
            "your number is too small):",
            "your number is too big):",
            "your number is CORRECT!!!!"
        };

        private readonly string[] _motivatingPhrases = {
            "come on, you almost guessed!",
            "you can I believe in you(;",
            "you\'re not stupid, let\'s guess!",
            "try hard!"
        };

        private enum Status {
            SmallNumber,
            BigNumber,
            CorrectNumber
        }
    }
}