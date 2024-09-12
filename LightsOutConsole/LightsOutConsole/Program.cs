using System;

namespace LightsOutConsole
{
    class Program
    {
        static void GetMove(int gridSize, out int row, out int col)
        {
            Console.Write("Row (1 -" + gridSize + ")? ");
            while (!Int32.TryParse(Console.ReadLine(), out row) ||
                row < 1 || row > gridSize)
            {
                Console.WriteLine("Please enter a number between 1 and " +
                    gridSize + ".");
                Console.Write("Row (1 -" + gridSize + ")? ");
            }

            Console.Write("Col (1 -" + gridSize + ")? ");
            while (!Int32.TryParse(Console.ReadLine(), out col) ||
                col < 1 || col > gridSize)
            {
                Console.WriteLine("Please enter a number between 1 and " +
                    gridSize + ".");
                Console.Write("Col (1 -" + gridSize + ")? ");
            }
        }

        static void PrintGameBoard(LightsOutGame game)
        {
            Console.Write("  ");
            for (int c = 0; c < game.GridSize; c++)
            {
                Console.Write(c + 1);
            }

            Console.WriteLine();

            for (int r = 0; r < game.GridSize; r++)
            {
                Console.Write(r + 1 + " ");
                for (int c = 0; c < game.GridSize; c++)
                {
                    if (game.IsOn(r, c))
                    {
                        Console.Write("O");
                    }
                    else
                    {
                        Console.Write("-");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            LightsOutGame game = new LightsOutGame();
            game.Cheat();

            PrintGameBoard(game);
            while (!game.IsGameOver())
            {
                int row;
                int col;
                GetMove(game.GridSize, out row, out col);

                game.FlipLight(row - 1, col - 1);

                Console.WriteLine();
                PrintGameBoard(game);
            }

            Console.WriteLine("You won!");
        }
    }
}
