using System;
using System.Collections.Generic;
using System.Linq;

namespace Bussen
{
    public class Bus
    {
        private List<Passenger> passengers = new List<Passenger>();

        public void Run()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("\n--- Bussimulator ---");
                Console.WriteLine("1. Lägg till passagerare");
                Console.WriteLine("2. Skriv ut alla passagerare");
                Console.WriteLine("3. Visa genomsnittsålder");
                Console.WriteLine("4. Visa högsta ålder");
                Console.WriteLine("5. Visa total ålder");
                Console.WriteLine("6. Sök passagerare i åldersintervall");
                Console.WriteLine("7. Sortera passagerare efter ålder");
                Console.WriteLine("8. Ta bort passagerare");
                Console.WriteLine("9. Avsluta programmet");
                Console.Write("Välj ett alternativ: ");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        AddPassenger();
                        break;
                    case "2":
                        PrintPassengers();
                        break;
                    case "3":
                        ShowAverageAge();
                        break;
                    case "4":
                        ShowMaxAge();
                        break;
                    case "5":
                        ShowTotalAge();
                        break;
                    case "6":
                        PrintPassengersInAgeRange();
                        break;
                    case "7":
                        SortPassengers();
                        break;
                    case "8":
                        RemovePassenger();
                        break;
                    case "9":
                        isRunning = false;
                        Console.WriteLine("\nProgrammet avslutas...");
                        break;
                    default:
                        Console.WriteLine("\nOgiltigt val, försök igen.");
                        break;
                }
            }
        }

        public void AddPassenger()
        {
            if (passengers.Count >= 10)
            {
                Console.WriteLine("\nBussen är full! Ingen fler passagerare kan läggas till.");
                return;
            }

            Console.Write("\nAnge passagerarens ålder: ");
            if (int.TryParse(Console.ReadLine(), out int age))
            {
                passengers.Add(new Passenger(age));
                Console.WriteLine("\nPassagerare tillagd!");
            }
            else
            {
                Console.WriteLine("\nOgiltig ålder, försök igen.");
            }
        }

        public void PrintPassengers()
        {
            if (passengers.Count == 0)
            {
                Console.WriteLine("\nBussen är tom.");
                return;
            }

            Console.WriteLine("\n--- Passagerarlista ---");
            foreach (var passenger in passengers)
            {
                passenger.DisplayInfo();
            }
        }

        public void ShowAverageAge()
        {
            if (passengers.Count == 0)
            {
                Console.WriteLine("\nIngen passagerare att räkna genomsnittsålder på.");
                return;
            }

            double average = (double)passengers.Sum(p => p.Age) / passengers.Count;
            Console.WriteLine($"\nGenomsnittsålder: {average:F2}");
        }

        public int TotalAge()
        {
            return passengers.Count == 0 ? 0 : passengers.Sum(p => p.Age);
        }

        public void ShowTotalAge()
        {
            Console.WriteLine($"\nDen totala åldern av alla passagerare: {TotalAge()} år.");
        }

        public void ShowMaxAge()
        {
            if (passengers.Count == 0)
            {
                Console.WriteLine("\nBussen har inga passagerare.");
                return;
            }

            int maxAge = passengers.Max(p => p.Age);
            Console.WriteLine($"\nDen äldsta passageraren är {maxAge} år.");
        }

        public void PrintPassengersInAgeRange()
        {
            Console.Write("\nAnge minimiålder: ");
            if (int.TryParse(Console.ReadLine(), out int minAge))
            {
                Console.Write("\nAnge maxålder: ");
                if (int.TryParse(Console.ReadLine(), out int maxAge))
                {
                    var filtered = passengers.Where(p => p.Age >= minAge && p.Age <= maxAge).ToList();
                    if (filtered.Count == 0)
                    {
                        Console.WriteLine("\nInga passagerare i detta åldersintervall.");
                    }
                    else
                    {
                        Console.WriteLine("\n--- Passagerare inom åldersintervallet ---");
                        foreach (var p in filtered)
                        {
                            p.DisplayInfo();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\nOgiltigt maxålder, försök igen.");
                }
            }
            else
            {
                Console.WriteLine("\nOgiltigt minimiålder, försök igen.");
            }
        }

        public void SortPassengers()
        {
            passengers.Sort((p1, p2) => p1.Age.CompareTo(p2.Age));
            Console.WriteLine("\nPassagerare sorterade efter ålder. Ange alternativ 2 för att visa passagerarlista.");
        }

        public void RemovePassenger()
        {
            Console.Write("\nAnge åldern på passageraren du vill ta bort: ");
            if (int.TryParse(Console.ReadLine(), out int age))
            {
                int removedCount = passengers.RemoveAll(p => p.Age == age);
                if (removedCount > 0)
                {
                    Console.WriteLine($"\n{removedCount} passagerare med ålder {age} har tagits bort.");
                }
                else
                {
                    Console.WriteLine("\nIngen passagerare med denna ålder hittades.");
                }
            }
            else
            {
                Console.WriteLine("\nOgiltig inmatning.");
            }
        }
    }
}
