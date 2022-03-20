using System;

namespace Geometry
{
    internal class Field
    {
        const char DefaultSymbol = '#';

        private readonly int[,] field;
        private readonly int width;
        private readonly int height;

        public int Area { get; private set; }

        public Field(int width, int height)
        {
            this.width = width;
            this.height = height;
            Area = this.width * this.height;

            field = new int[this.height, this.width];
        }

        public void PlaceRectangle(Rectangle rectangle)
        {
            for (int i = rectangle.Y1; i < rectangle.Y2; i++)
            {
                for (int j = rectangle.X1; j < rectangle.X2; j++)
                {
                    field[i, j] = rectangle.Player.Number;
                }
            }
        }

        public bool PlaceIsClear(Rectangle rectangle)
        {
            if (rectangle.X2 > width || rectangle.Y2 > height)
            {
                return false;
            }

            for (int i = rectangle.Y1; i < rectangle.Y2; i++)
            {
                for (int j = rectangle.X1; j < rectangle.X2; j++)
                {
                    if (field[i, j] != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool ItCanBeAnywhere(Rectangle rectangle)
        {
            int power;

            int[,] powerField = new int[height, width];

            for (int i = 0; i < height; i++)
            {
                power = 0;

                for (int j = 0; j < width; j++)
                {
                    power = field[i, j] == 0 ? ++power : 0;

                    powerField[i, j] = power;
                }
            }

            int linesInRow = 0;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (powerField[j, i] >= rectangle.Width)
                    {
                        linesInRow++;

                        if (linesInRow == rectangle.Height)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        linesInRow = 0;
                    }
                }

                linesInRow = 0;
            }

            return false;
        }

        public void Print(int turnsLeft = 0)
        {
            Console.Clear();

            if (turnsLeft >= 0)
                Console.WriteLine($"{turnsLeft} turns left");

            PrintHorizontalLine();

            for (int i = 0; i < height; i++)
            {
                Console.Write((i + 1) % 10 + "\t");

                for (int j = 0; j < width; j++)
                {
                    if (field[i, j] == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (field[i, j] == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Console.Write("{0:d} ", DefaultSymbol);

                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine("\t" + (i + 1) % 10);
            }

            Console.WriteLine();

            PrintHorizontalLine();

            void PrintHorizontalLine()
            {
                Console.Write("\t");

                for (int i = 0; i < width; i++)
                {
                    Console.Write("{0:d} ", (i + 1) % 10);
                }

                Console.WriteLine("\n");
            }
        }
    }
}
