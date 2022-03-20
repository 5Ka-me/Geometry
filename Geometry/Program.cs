using System;

namespace Geometry
{
    static internal class Program
    {        
        const string ProgramName = "Geometry";
        const string EnterWidth = "Enter field width";
        const string EnterHeight = "Enter field length";
        const string EnterTurns = "Enter number of turns";
        const string NoAttempts = "You spend all attempts.Press any key to continue.";
        const string CantPlaceHere = "you can't place here. Try another one!";
        const string EnterCorrectNumber = "Enter correct number";
        const string FirstPlayerName = "player1";
        const string SecondPlayerName = "player2";
        const string PressAnyKey = "Press any key to continue.";
        const string Result = "Result:";
        const string Draw = "Draw!";

        const int MaxAttempts = 2;
        const int MaxDiceSide = 6;
        const int MinFieldWidth = 30;
        const int MaxFieldWidth = 98;
        const int MinfieldHeight = 20;
        const int MaxFieldHeight = 38;
        const int MinNumberOfTurns = 20;

        private static Field field;
        private static Player player1;
        private static Player player2;
        private static bool isGameContinue = true;
        private static int maxTurnCount;


        static void Main(string[] args)
        {
            Console.Title = ProgramName;

            Console.WriteLine(EnterWidth);
            int fieldWidth = EnterNumberMoreThan(MinFieldWidth, MaxFieldWidth);

            Console.WriteLine(EnterHeight);
            int fieldHeigh = EnterNumberMoreThan(MinfieldHeight, MaxFieldHeight);

            Console.WriteLine(EnterTurns);
            maxTurnCount = EnterNumberMoreThan(MinNumberOfTurns, int.MaxValue);

            field = new Field(fieldWidth, fieldHeigh);

            player1 = new(FirstPlayerName);
            player2 = new(SecondPlayerName);

            Game(player1, player2);

            PrintResult();
        }

        public static int EnterNumberMoreThan(int minNumber, int maxNumber)
        {
            int number;

            while (!int.TryParse(Console.ReadLine(), out number) || number < minNumber || number > maxNumber)
            {
                Console.WriteLine($"Number should be more or equal than {minNumber} and less than {maxNumber}");
            }

            return number;
        }

        public static void Game(Player player1, Player player2)
        {
            int turnNumber = 0;

            while (isGameContinue)
            {
                Turn(player1, maxTurnCount - turnNumber);
                Turn(player2, maxTurnCount - turnNumber);

                turnNumber++;

                int summaryScore = player1.Score + player2.Score;

                if ((turnNumber > maxTurnCount) || (summaryScore == field.Area))
                {
                    isGameContinue = false;
                }
            }
        }

        public static void Turn(Player player, int turnLeft)
        {
            field.Print(turnLeft);

            Rectangle rectangle;
            int attempNumber = 0;
            bool itCanBePlacedHere;
            bool itCanBePlacedAnywhere;

            do
            {
                (int width, int height) = RollDice();
                rectangle = new(width, height, player);
                itCanBePlacedAnywhere = field.ItCanBeAnywhere(rectangle);


                if (!itCanBePlacedAnywhere)
                {
                    Console.WriteLine($"{player.Name}, you get {width} and {height} on dice. You can't place it. You have {attempNumber} attemps");
                    Console.WriteLine(PressAnyKey);
                    Console.ReadKey();
                    field.Print(turnLeft);
                }

            }
            while (!itCanBePlacedAnywhere && ++attempNumber < MaxAttempts);

            if (!itCanBePlacedAnywhere)
            {
                Console.WriteLine(NoAttempts);
                return;
            }

            do
            {
                EnterCoordinates(rectangle, player);

                itCanBePlacedHere = field.PlaceIsClear(rectangle);

                if (!itCanBePlacedHere)
                {
                    field.Print(turnLeft);
                    Console.WriteLine(player.Name + CantPlaceHere);
                }
            }
            while (!itCanBePlacedHere);

            field.PlaceRectangle(rectangle);
            player.Score += rectangle.CalculateScore();
        }

        public static void PrintResult()
        {
            field.Print();

            Console.WriteLine(Result);
            if (player1.Score == player2.Score)
            {
                Console.WriteLine(Draw);
            }
            else if (player1.Score > player2.Score)
            {
                Player winer = player1.Score > player2.Score ? player1 : player2;
                Player loser = player1.Score < player2.Score ? player1 : player2;

                Console.WriteLine($"Player {winer.Name} won! His score: {winer.Score}");
                Console.WriteLine($"Player {loser.Name} lost. His score: {loser.Score}");
            }
        }

        public static Rectangle EnterCoordinates(Rectangle rectangle, Player player)
        {
            Console.WriteLine($"Score: {player1.Name} has {player1.Score} points.\t{player2.Name} has {player2.Score} points");

            Console.WriteLine($"\nPlayer {player.Name} you get {rectangle.Width} and {rectangle.Height} on your dice");

            Console.Write($"\nPlayer {player.Name} enter pos x: ");
            int x = EnterNumber();
            Console.Write($"Player {player.Name} enter pos y: ");
            int y = EnterNumber();

            rectangle.MoveTo(x, y);

            return rectangle;
        }

        public static int EnterNumber()
        {
            int number;

            while (!int.TryParse(Console.ReadLine(), out number) || (number <= 0))
            {
                Console.WriteLine(EnterCorrectNumber);
            }

            return number;
        }

        public static (int, int) RollDice()
        {
            Random rand = new();

            int firstDice = rand.Next(MaxDiceSide) + 1;
            int secondDice = rand.Next(MaxDiceSide) + 1;

            return (firstDice, secondDice);
        }
    }
}
