using System;
using System.Linq;
using System.Threading;

namespace task1
{
    class Program
    {
        static string[] columns;
        static int[] positionsColumns;
        static int[] lengthColumns;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        static Random random;

        static void Main(string[] args)
        {
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;
            GenerateStartArgs(width, height);

            while (true)
            {
                outputColumns(width, height);
            }
        }

        static void GenerateStartArgs(int width, int height)
        {
            columns = new string[width];
            positionsColumns = new int[width];
            lengthColumns = new int[width];
            random = new Random();

            for (int i = 0; i < width; i++)
                positionsColumns[i] = random.Next(0, height);
            for (int i = 0; i < width; i++)
                lengthColumns[i] = random.Next(3, height / 3);
        }

        static void GenerateColumns()
        {
            for (int i = 0; i < columns.Length; i++)
            {
                columns[i] = new string(Enumerable.Repeat(chars, lengthColumns[i])
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
        }

        static void outputColumns(int width, int height)
        {
            GenerateColumns();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < lengthColumns[i]; j++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    if (j == 0)
                        Console.ForegroundColor = ConsoleColor.White;
                    else if (j == 1)
                        Console.ForegroundColor = ConsoleColor.Green;

                    int positionTop = positionsColumns[i] - j;
                    if (positionTop < 0)
                        positionTop = height + positionTop;
                    Console.SetCursorPosition(i, positionTop);
                    Console.Write(columns[i][j]);
                }
                if (positionsColumns[i] - lengthColumns[i] < 0)
                    Console.SetCursorPosition(i, height + (positionsColumns[i] - lengthColumns[i]));
                else
                    Console.SetCursorPosition(i, positionsColumns[i] - lengthColumns[i]);
                Console.Write(" ");

                positionsColumns[i]++;
                if (positionsColumns[i] >= height)
                    positionsColumns[i] = 0;
            }
        }
    }
}
