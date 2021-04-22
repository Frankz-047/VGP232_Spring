using System;

namespace Week2
{
    class Program
    {
        public enum DaysOfWeek
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday,
            Size
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string text = "Sunday";

            //DaysOfWeek Today = DaysOfWeek.Thursday;

            if (Enum.TryParse<DaysOfWeek>(text,out DaysOfWeek Today))
            {
                Console.WriteLine(Today);
            }
        }
    }
}
