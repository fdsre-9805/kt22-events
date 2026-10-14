using System;

namespace AchievementSystemApp
{
    class Program
    {
        static void Main()
        {
            Player player = new Player();

            Console.Write("Введите порог очков для достижения: ");
            int scoreThreshold;
            while (!TryParseNumber(Console.ReadLine()!, out scoreThreshold)) { }

            player.ScoreChanged += (sender, scoreChange) =>
                Console.WriteLine($"Очки: {scoreChange.OldScore} -> {scoreChange.NewScore}");
            player.AchievementUnlocked += (sender, achievementName) =>
            {
                Console.WriteLine($"Достижение разблокировано: {achievementName}");
                Console.WriteLine("Вы победили! Игра закончена.");
                Environment.Exit(0);
            };

            AchievementTracker achievementTracker = new AchievementTracker(player, scoreThreshold);

            Console.WriteLine("Вводите очки по одному числу, пустая строка - отписка трекера:");
            ReadNumbers(player.AddScore);

            achievementTracker.Unsubscribe();

            Console.WriteLine("Вводите очки после отписки, пустая строка - выход:");
            ReadNumbers(player.AddScore);
        }

        static void ReadNumbers(Action<int> handleNumber)
        {
            string inputLine = Console.ReadLine()!;
            while (inputLine != "")
            {
                if (TryParseNumber(inputLine, out int number))
                    handleNumber(number);
                inputLine = Console.ReadLine()!;
            }
        }

        static bool TryParseNumber(string inputLine, out int number)
        {
            number = 0;
            try
            {
                number = int.Parse(inputLine);
                return true;
            }
            catch (FormatException) { Console.WriteLine("Ошибка: введите целое число."); }
            catch (OverflowException) { Console.WriteLine("Ошибка: слишком большое число."); }
            return false;
        }
    }
}
