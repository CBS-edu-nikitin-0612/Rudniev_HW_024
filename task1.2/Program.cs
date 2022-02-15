using System;
using System.Linq;
using System.Threading;

namespace task1._2
{
    class Program
    {
        private static object block = new object();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        static Random random;
        static int width;
        static int height;
        static int nextPosition = 0;
        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 24);
            width = Console.WindowWidth;
            height = Console.WindowHeight;
            random = new Random();

            for (int i = 0; i < width; i++)
                new Thread(outputColumns).Start();
        }

        static void outputColumns()
        {
            string column;
            int positionsColumnX;
            int positionsColumnY;
            int lengthColumn;
            //стартовые параметры колонки
            positionsColumnX = nextPosition;
            nextPosition++;
            positionsColumnY = random.Next(0, height);
            lengthColumn = random.Next(3, height / 3);

            while (true)
            {
                //генерация новых элементов колонки
                column = new string(Enumerable.Repeat(chars, lengthColumn)
                .Select(s => s[random.Next(s.Length)]).ToArray());
                lock (block)
                {
                    //Вывод всех элементов колонки
                    for (int i = 0; i < lengthColumn; i++)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        if (i == 0)
                            Console.ForegroundColor = ConsoleColor.White;
                        else if (i == 1)
                            Console.ForegroundColor = ConsoleColor.Green;

                        int positionY = positionsColumnY - i;
                        if (positionY < 0)
                            positionY = height + positionY;
                        Console.SetCursorPosition(positionsColumnX, positionY);
                        Console.Write(column[i]);
                    }
                    // Затираем хвост колонки, после передвижени вниз
                    if (positionsColumnY - lengthColumn < 0)
                        Console.SetCursorPosition(positionsColumnX, height + (positionsColumnY - lengthColumn));
                    else
                        Console.SetCursorPosition(positionsColumnX, positionsColumnY - lengthColumn);
                    Console.Write(" ");
                    // Меняем позицию колонки на 1 вниз
                    positionsColumnY++;
                    if (positionsColumnY == height)
                        positionsColumnY = 0;
                }
                Thread.Sleep(1);
            }
        }


    }
}