using NLog;
using System;

namespace Redfox
{
    class Program
    {
        static void Main(string[] args)
        {
            Core.Boot();
            Console.CancelKeyPress += delegate
            {
                Core.CleanUp();
            };
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
