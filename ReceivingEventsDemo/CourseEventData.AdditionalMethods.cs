using System;
using System.Collections.Generic;
using System.Text;

namespace ReceivingEventsDemo
{
    public partial class CourseEventData
    {
        public override string ToString()
        {
            // formatting for console
            ChangeConsoleColor(Department);
            return "Course: " + Department + " " + CourseNumber + "\tStart Time: " + StartTime.ToString("t");
        }

        private static void ChangeConsoleColor(string department)
        {
            Console.ForegroundColor = department switch
            {
                "MATH" => ConsoleColor.Green,
                "PHYS" => ConsoleColor.Blue,
                "CSE" => ConsoleColor.Yellow,
                "BIOL" => ConsoleColor.Red,
                "ENGL" => ConsoleColor.Cyan,
                _ => ConsoleColor.White,
            };
        }
    }
}
