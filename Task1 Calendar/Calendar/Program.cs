using System;
using System.Globalization;

namespace Calendar
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter date, please:");
            string readDateAsString = Console.ReadLine();
            if (DateTime.TryParse(readDateAsString, out DateTime valueDate))
            {
                PrintCalendarForMonth(valueDate);
            }
            else
            {
                Console.WriteLine("{0} is incorrect format date", readDateAsString);
            }
            Console.ReadKey();
        }

        private static void PrintCalendarForMonth(DateTime valueDate)
        {
            Console.ForegroundColor = ConsoleColor.White;
            PrintDaysOfWeek();
            PrintStartIndent(valueDate);
            int numberOfWorkingDays = 0;
            for (DateTime currentDate = new DateTime(valueDate.Year, valueDate.Month, 1); currentDate.Month == valueDate.Month; currentDate = currentDate.AddDays(1))
            {
                if (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                { 
                    numberOfWorkingDays++;
                }
                if (currentDate == DateTime.Today)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                if (currentDate.Day == valueDate.Day)
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                }

                Console.Write("{0}\t", currentDate.Day);

                if (currentDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    Console.WriteLine();
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine();
        }
        private static void PrintStartIndent(DateTime valueDate)
        {
            int firstDay = (int)new DateTime(valueDate.Year, valueDate.Month, 1).DayOfWeek;
            if ((int)DayOfWeek.Sunday == firstDay)
            {
                firstDay = 7;
            }
            firstDay--;
            for (int i = 0; i < firstDay; i++)
            {
                Console.Write("\t");
            }

        }

        private static void PrintDaysOfWeek()
        {
            Console.WriteLine();
            string[] daysOfWeek = DateTimeFormatInfo.CurrentInfo.AbbreviatedDayNames;  
            for ( int i = 1; i < 7; i++)
            {
                Console.Write($"{daysOfWeek[i]}\t");
            }
            Console.Write($"{daysOfWeek[0]}\n");
        }
                
        
    }
}
