using System;

namespace Geometry
{
    internal class Player
    {
        private static int counter = 1;

        public string Name { get; set; }
        public int Number { get; set; }
        public int Score { get; set; } = 0;

        public Player(string name)
        {
            Name = name;
            Number = counter;

            IncreaseCounter();
        }

        static void IncreaseCounter()
        {
            counter++;
        }

        public void PrintScore()
        {
            Console.WriteLine($"Player {Name} has score: {Score}");
        }
    }
}
