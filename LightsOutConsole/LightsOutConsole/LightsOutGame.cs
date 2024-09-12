using System;
using Microsoft.VisualBasic;

namespace LightsOutConsole
{
    public class LightsOutGame
    {
        // 2D array from https://www.w3schools.com/cs/cs_arrays_multi.php
        bool[,] grid;
        private int gridSize;
        public int GridSize
        {
            get { return gridSize; }
            set
            {
                if (value >= 3 && value <= 7)
                {
                    gridSize = value;
                    grid = new bool[value, value];
                    NewGame();
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
        public LightsOutGame()
        {
            GridSize = 3;
        }

        public void NewGame()
        {
            // Random generator from https://stackoverflow.com/questions/2706500/how-do-i-generate-a-random-integer-in-c
            var rnd = new Random();

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = rnd.NextDouble() >= 0.5;
                }
            }
        }

        public bool IsOn(int row, int col)
        {
            if (grid[row, col])
            {
                return true;
            }
            else
            {
                 return false; 
            }
        
        }

        public void FlipLight(int row, int col)
        {
            grid[row, col] = !grid[row, col];

            // Right
            if (col + 1 < gridSize)
            {
                grid[row, col + 1] = !grid[row, col + 1];
            }

            // Left
            if (col - 1 >= 0)
            {
                grid[row, col - 1] = !grid[row, col - 1];
            }

            // Top
            if (row - 1 >= 0)
            {
                grid[row - 1, col] = !grid[row - 1, col];
            }

            // Bottom
            if (row + 1 < gridSize)
            {
                grid[row + 1, col] = !grid[row + 1, col];
            }
        }

        public bool IsGameOver()
        {
            // Any syntax from https://stackoverflow.com/questions/29613558/linq-any-iterator-for-2d-array
            return !grid.Cast<bool>().Any(value => value == true);
        }

        public void Cheat()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = false;
                }
            }

            grid[0, 0] = true; // Top-left cell
            grid[0, 1] = true;
            grid[1, 0] = true;
        }

        public override string ToString()
        {
            string line = "";

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    line += grid[i, j] ? "T" : "F";
                }
                line += Environment.NewLine;
            }

            return line;
        }
    }
}


