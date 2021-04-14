namespace LibrariesTestingUI
{
    using System;
    using System.IO;
    using Clock;
    using Logger;

    public static class Program
    {
        public static void Main(string[] args)
        {
            Clock clock = new Clock();
            Logger logger = new Logger();
            while (true)
            {
                Console.WriteLine("1. Start the Clock\n2. Stop the Clock\n3. Log messages to file\nPress 'q' to quit");
                switch (Console.ReadLine())
                {
                    case "1":
                        clock.Start(OutputToFile);
                        break;
                    case "2":
                        clock.Stop();
                        break;
                    case "3":
                        clock.Logger += logger.LogToFile;
                        break;
                    case "q":
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }

                Console.Clear();
            }
        }

        private static void OutputToFile(DateTime time)
        {
            using (StreamWriter stream = File.CreateText("current_time.txt"))
            {
                stream.WriteLine(time);
            }
        }
    }
}
