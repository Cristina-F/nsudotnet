using System;
using System.Globalization;

namespace Calendar
{
    internal static class Calendar
    {
        private static void Main()
        {
            Console.WriteLine("Enter date, please:");
            var readDateAsString = Console.ReadLine();
            if (DateTime.TryParse(readDateAsString, out var valueDate))
            {
                _inputDate = valueDate;
                PrintCalendarForMonth();
            }
            else
            {
                Console.WriteLine($"{readDateAsString} is incorrect format date");
            }
        }

        private static void PrintCalendarForMonth()
        {
            Console.ForegroundColor = ConsoleColor.White;
            PrintDaysOfWeek();
            PrintStartIndent();
            var numberOfWorkingDays = 0;
            for (var currentDate = new DateTime(_inputDate.Year, _inputDate.Month, 1); currentDate.Month == _inputDate.Month; currentDate = currentDate.AddDays(1))
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
 
                Console.Write($"{currentDate.Day}\t");

                if (currentDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    Console.WriteLine();
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine($"\nNumber of working days: {numberOfWorkingDays}");
        }
        private static void PrintStartIndent()
        {
            var firstDay = (int)new DateTime(_inputDate.Year, _inputDate.Month, 1).DayOfWeek;
            if ((int)DayOfWeek.Sunday == firstDay)
            {
                firstDay = 7;
            }
            firstDay--;
            for (var i = 0; i < firstDay; i++)
            {
                Console.Write("\t");
            }

        }

        private static void PrintDaysOfWeek()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            var daysOfWeek = DateTimeFormatInfo.CurrentInfo.AbbreviatedDayNames;  
            for ( var i = 1; i < 7; i++)
            {
                Console.Write($"{daysOfWeek[i]}\t");
            }
            Console.Write($"{daysOfWeek[0]}\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static DateTime _inputDate;
    }
}
