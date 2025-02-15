using System;

namespace Bussen
{
    public class Passenger
    {
        public int Age { get; set; }

        public Passenger(int age)
        {
            Age = age;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Passagerare: Ålder {Age}");
        }
    }
}
