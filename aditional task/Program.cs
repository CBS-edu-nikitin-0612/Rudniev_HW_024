using System;
using System.Threading;

namespace aditional_task
{
    class Program
    {
        static int x;
        static void Main(string[] args)
        {
            x = 0;
            ThreadStart method = new ThreadStart(Method);
            Thread thread = new Thread(method);
            thread.Start();
        }
        static void Method()
        {
            Console.WriteLine(++x);
            Thread.Sleep(800);
            Method();
        }
    }
}
