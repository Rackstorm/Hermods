using System;
using System.Collections.Generic;

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
                Console.WriteLine("9. Visa passagerare baserat på kön");
                Console.WriteLine("10. Avsluta");
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
                        PrintPassengersByGender();
                        break;
                    case "10":
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
                string gender;
                do
                {
                    Console.Write("\nAnge kön (M/K): ");
                    gender = Console.ReadLine()?.Trim().ToUpper() ?? "";

                } while (gender != "M" && gender != "K");

                passengers.Add(new Passenger(age, gender));
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
                Console.WriteLine($"Ålder: {passenger.Age}, Kön: {passenger.Gender}");
            }
        }

        public void ShowAverageAge()
        {
            if (passengers.Count == 0)
            {
                Console.WriteLine("\nIngen passagerare att räkna genomsnittsålder på.");
                return;
            }

            int totalAge = 0;
            foreach (var passenger in passengers)
            {
                totalAge += passenger.Age;
            }

            double average = (double)totalAge / passengers.Count;
            Console.WriteLine($"\nGenomsnittsålder: {average:F2}");
        }

        public void ShowTotalAge()
        {
            int totalAge = 0;
            foreach (var passenger in passengers)
            {
                totalAge += passenger.Age;
            }

            Console.WriteLine($"\nDen totala åldern av alla passagerare: {totalAge} år.");
        }

        public void ShowMaxAge()
        {
            if (passengers.Count == 0)
            {
                Console.WriteLine("\nBussen har inga passagerare.");
                return;
            }

            int maxAge = passengers[0].Age;
            foreach (var passenger in passengers)
            {
                if (passenger.Age > maxAge)
                {
                    maxAge = passenger.Age;
                }
            }

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
                    List<Passenger> filteredPassengers = new List<Passenger>();

                    foreach (var passenger in passengers)
                    {
                        if (passenger.Age >= minAge && passenger.Age <= maxAge)
                        {
                            filteredPassengers.Add(passenger);
                        }
                    }

                    if (filteredPassengers.Count == 0)
                    {
                        Console.WriteLine("\nInga passagerare i detta åldersintervall.");
                    }
                    else
                    {
                        Console.WriteLine("\n--- Passagerare inom åldersintervallet ---");
                        foreach (var p in filteredPassengers)
                        {
                            Console.WriteLine($"Ålder: {p.Age}, Kön: {p.Gender}");
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
            for (int i = 0; i < passengers.Count - 1; i++)
            {
                for (int j = 0; j < passengers.Count - i - 1; j++)
                {
                    if (passengers[j].Age > passengers[j + 1].Age)
                    {
                        var temp = passengers[j];
                        passengers[j] = passengers[j + 1];
                        passengers[j + 1] = temp;
                    }
                }
            }

            Console.WriteLine("\nPassagerare sorterade efter ålder. Ange alternativ 2 för att visa passagerarlista.");
        }

        public void RemovePassenger()
        {
            Console.Write("\nAnge åldern på passageraren du vill ta bort: ");
            if (int.TryParse(Console.ReadLine(), out int age))
            {
                int removedCount = 0;
                for (int i = passengers.Count - 1; i >= 0; i--)
                {
                    if (passengers[i].Age == age)
                    {
                        passengers.RemoveAt(i);
                        removedCount++;
                    }
                }

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

        public void PrintPassengersByGender()
        {
            Console.Write("\nVisa passagerare med kön (M/K): ");
            string gender = Console.ReadLine()?.Trim().ToUpper() ?? "";

            List<Passenger> filteredPassengers = new List<Passenger>();

            foreach (var passenger in passengers)
            {
                if (passenger.Gender == gender)
                {
                    filteredPassengers.Add(passenger);
                }
            }

            if (filteredPassengers.Count == 0)
            {
                Console.WriteLine("\nInga passagerare av detta kön hittades.");
            }
            else
            {
                Console.WriteLine($"\n--- Passagerare med kön {gender} ---");
                foreach (var p in filteredPassengers)
                {
                    Console.WriteLine($"Ålder: {p.Age}, Kön: {p.Gender}");
                }
            }
        }
    }
}
