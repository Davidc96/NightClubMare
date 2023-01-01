using System;

namespace PCLILogger
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggerServer lg_server = new LoggerServer();
            lg_server.initServer();

            Console.ReadLine();
        }
    }
}
