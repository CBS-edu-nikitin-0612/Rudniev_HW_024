using System;
using System.Linq;
using System.Threading;

namespace task2
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
            string[] column = new string[2];
            int positionsColumnX;
            int[] positionsColumnY = new int[2];
            int[] lengthColumn = new int[2];
            //стартовые параметры колонки
            positionsColumnX = nextPosition;
            nextPosition++;
            for (int i = 0; i < 2; i++)
                lengthColumn[i] = random.Next(3, height / 3);
            positionsColumnY[0] = random.Next(0, height/ 2);
            positionsColumnY[1] = random.Next(height / 2, height);

            while (true)
            {
                //генерация новых элементов колонки
                for (int i = 0; i < 2; i++)
                    column[i] = new string(Enumerable.Repeat(chars, lengthColumn[i])
                        .Select(s => s[random.Next(s.Length)]).ToArray());
                lock (block)
                {
                    //Вывод всех элементов колонки
                    for (int j = 0; j < 2; j++)
                    {
                        for (int i = 0; i < lengthColumn[j]; i++)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            if (i == 0)
                                Console.ForegroundColor = ConsoleColor.White;
                            else if (i == 1)
                                Console.ForegroundColor = ConsoleColor.Green;

                            int positionY = positionsColumnY[j] - i;
                            if (positionY < 0)
                                positionY = height + positionY;
                            Console.SetCursorPosition(positionsColumnX, positionY);
                            Console.Write(column[j][i]);
                        }
                        // Затираем хвост колонки, после передвижени вниз
                        if (positionsColumnY[j] - lengthColumn[j] < 0)
                            Console.SetCursorPosition(positionsColumnX, height + (positionsColumnY[j] - lengthColumn[j]));
                        else
                            Console.SetCursorPosition(positionsColumnX, positionsColumnY[j] - lengthColumn[j]);
                        Console.Write(" ");
                        // Меняем позицию колонки на 1 вниз
                        positionsColumnY[j]++;
                        if (positionsColumnY[j] == height)
                            positionsColumnY[j] = 0;
                    }
                }
                Thread.Sleep(1);
            }
        }


    }
}