using System;

namespace Bussen
{
    public class Bus
    {
        // En array för att lagra passagerare. Jag har lagt en maxgräns på 10 passagerare.
        private Passenger[] passengers = new Passenger[10];
        private int passengerCount = 0; // Håller reda på antal passagerare

        public void Run()
        {
            bool isRunning = true;
            // Programmet körs i en oändlig loop tills användaren väljer att avsluta.
            while (isRunning)
            {
                // Skriver ut en meny med alternativ för användaren.
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
                Console.WriteLine("10. Peta på en passagerare");
                Console.WriteLine("11. Passagerare stiger av");
                Console.WriteLine("12. Avsluta");
                Console.Write("Välj ett alternativ: ");

                string input = Console.ReadLine(); // Tar emot användarens val.
                switch (input)
                {
                    // Här anropas olika metoder baserat på användarens val.
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
                        PokePassenger();
                        break;
                    case "11":
                        PassengerExit();
                        break;
                    case "12":
                        isRunning = false; // Stoppar loopen och avslutar programmet.
                        Console.WriteLine("\nProgrammet avslutas...");
                        break;
                    default:
                        // Om användaren skriver något ogiltigt.
                        Console.WriteLine("\nOgiltigt val, försök igen.");
                        break;
                }
            }
        }

        public void PokePassenger()
        {
            // Frågar användaren efter en passagerare att "peta på" 
            Console.Write("\nAnge en siffra mellan 1-10 för passageraren du vill peta på: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < passengerCount)
            {
                Passenger p = passengers[index]; // Hämtar passageraren 
                Console.Write($"\nDu petade på en {p.Age}-årig passagerare ({p.Gender}). ");

                // Meddelande efter petande baserat på passagerarens ålder och kön.
                if (p.Gender == "M")
                {
                    Console.WriteLine(p.Age < 18 ? "Han skrattar och bjuder på en Twix." : "Han suckar och himlar med ögonen.");
                }
                else
                {
                    Console.WriteLine(p.Age < 18 ? "Hon skrattar och petar tillbaka." : "Hon glor surt och tar fram pepparsprayen.");
                }
            }
            else
            {
                // Om användaren skriver in ett ogiltigt index (utanför passagerarnas antal).
                Console.WriteLine("\nDet finns ingen passagerare på den platsen. Pröva en annan siffra! (Annars, kolla hur många passagerare som sitter på bussen först.)");
            }
        }

        public void PassengerExit()
        {
            // Frågar användaren efter en passagerares ålder för avstigning.
            Console.Write("\nAnge åldern på passageraren som ska stiga av: ");
            if (int.TryParse(Console.ReadLine(), out int age))
            {
                for (int i = 0; i < passengerCount; i++)
                {
                    if (passengers[i].Age == age) // Hittar passageraren med den åldern.
                    {
                        // Flyttar alla passagerare efter den som stiger av en plats bakåt.
                        for (int j = i; j < passengerCount - 1; j++)
                        {
                            passengers[j] = passengers[j + 1];
                        }
                        passengers[passengerCount - 1] = null; // Sätter sista platsen till null.
                        passengerCount--; // Minskar antalet passagerare.
                        Console.WriteLine("\nPassageraren har stigit av.");
                        return;
                    }
                }
                Console.WriteLine("\nIngen passagerare med denna ålder hittades.");
            }
            else
            {
                // Om användaren matar in ett ogiltig ålder.
                Console.WriteLine("\nOgiltig inmatning.");
            }
        }

        public void AddPassenger()
        {
            // Om bussen är full (10 passagerare), kan inga fler läggas till.
            if (passengerCount >= 10)
            {
                Console.WriteLine("\nBussen är full! Ingen fler passagerare kan läggas till.");
                return;
            }

            // Tar emot passagerarens ålder och kontrollerar att det är ett giltigt tal.
            Console.Write("\nAnge passagerarens ålder: ");
            if (int.TryParse(Console.ReadLine(), out int age))
            {
                string gender;
                // Loopa tills användaren anger ett giltigt kön 
                do
                {
                    Console.Write("\nAnge kön (M/K): ");
                    gender = Console.ReadLine()?.Trim().ToUpper() ?? "";
                } while (gender != "M" && gender != "K");

                // Skapar en ny passagerare och lägger till den i arrayen.
                passengers[passengerCount] = new Passenger(age, gender);
                passengerCount++; // Ökar passagerarantalet.
                Console.WriteLine("\nPassagerare tillagd!");
            }
            else
            {
                // Om användaren anger en ogiltig ålder.
                Console.WriteLine("\nOgiltig ålder, försök igen.");
            }
        }

        public void PrintPassengers()
        {
            // Om inga passagerare finns, informera att bussen är tom.
            if (passengerCount == 0)
            {
                Console.WriteLine("\nBussen är tom.");
                return;
            }

            // Skriv ut alla passagerare med deras ålder och kön.
            Console.WriteLine("\n--- Passagerarlista ---");
            for (int i = 0; i < passengerCount; i++)
            {
                Console.WriteLine($"Ålder: {passengers[i].Age}, Kön: {passengers[i].Gender}");
            }
        }

        public void ShowAverageAge()
        {
            // Om inga passagerare finns, skriv ut ett meddelande.
            if (passengerCount == 0)
            {
                Console.WriteLine("\nIngen passagerare att räkna genomsnittsålder på.");
                return;
            }

            int totalAge = 0;
            // Summerar åldern för alla passagerare.
            for (int i = 0; i < passengerCount; i++)
            {
                totalAge += passengers[i].Age;
            }

            // Beräknar och visar genomsnittsåldern.
            double average = (double)totalAge / passengerCount;
            Console.WriteLine($"\nGenomsnittsålder: {average:F2}");
        }

        public void ShowTotalAge()
        {
            // Summerar åldern för alla passagerare och visar den totala åldern.
            int totalAge = 0;
            for (int i = 0; i < passengerCount; i++)
            {
                totalAge += passengers[i].Age;
            }
            Console.WriteLine($"\nTotal ålder: {totalAge} år.");
        }

        public void ShowMaxAge()
        {
            // Om inga passagerare finns, informera att det inte finns några passagerare.
            if (passengerCount == 0)
            {
                Console.WriteLine("\nBussen har inga passagerare.");
                return;
            }

            int maxAge = passengers[0].Age;
            // Söker igenom alla passagerare för att hitta den äldsta.
            for (int i = 1; i < passengerCount; i++)
            {
                if (passengers[i].Age > maxAge)
                    maxAge = passengers[i].Age;
            }

            // Skriv ut den äldsta passagerarens ålder.
            Console.WriteLine($"\nÄldsta passageraren är {maxAge} år.");
        }

        public void PrintPassengersInAgeRange()
        {
            // Frågar användaren efter ett åldersintervall och skriver ut passagerare inom det intervallet.
            Console.Write("\nAnge minimiålder: ");
            if (int.TryParse(Console.ReadLine(), out int minAge))
            {
                Console.Write("\nAnge maxålder: ");
                if (int.TryParse(Console.ReadLine(), out int maxAge))
                {
                    bool found = false;
                    Console.WriteLine("\n--- Passagerare inom åldersintervallet ---");
                    for (int i = 0; i < passengerCount; i++)
                    {
                        if (passengers[i].Age >= minAge && passengers[i].Age <= maxAge)
                        {
                            Console.WriteLine($"Ålder: {passengers[i].Age}, Kön: {passengers[i].Gender}");
                            found = true;
                        }
                    }
                    if (!found)
                        Console.WriteLine("\nInga passagerare i detta åldersintervall.");
                }
            }
        }

        public void PrintPassengersByGender()
        {
            // Frågar användaren efter kön och skriver ut passagerare av det könet.
            Console.Write("\nAnge kön (M/K): ");
            string gender = Console.ReadLine()?.Trim().ToUpper() ?? "";

            bool found = false;
            Console.WriteLine($"\n--- Passagerare med kön {gender} ---");
            for (int i = 0; i < passengerCount; i++)
            {
                if (passengers[i].Gender == gender)
                {
                    Console.WriteLine($"Ålder: {passengers[i].Age}, Kön: {passengers[i].Gender}");
                    found = true;
                }
            }

            if (!found)
                Console.WriteLine("\nInga passagerare av detta kön hittades.");
        }

        public void SortPassengers()
        {
            // Om det finns mer än en passagerare, sortera dem efter ålder med bubble sort.
            if (passengerCount < 2)
            {
                Console.WriteLine("\nInget att sortera.");
                return;
            }

            for (int i = 0; i < passengerCount - 1; i++)
            {
                for (int j = 0; j < passengerCount - 1 - i; j++)
                {
                    if (passengers[j].Age > passengers[j + 1].Age)
                    {
                        // Byt plats på två passagerare om de inte är i rätt ordning.
                        Passenger temp = passengers[j];
                        passengers[j] = passengers[j + 1];
                        passengers[j + 1] = temp;
                    }
                }
            }

            Console.WriteLine("\nPassagerare sorterade efter ålder.");
        }

        public void RemovePassenger()
        {
            // Frågar användaren om en passagerares ålder för att ta bort denne.
            Console.Write("\nAnge åldern på passageraren du vill ta bort: ");
            if (int.TryParse(Console.ReadLine(), out int age))
            {
                for (int i = 0; i < passengerCount; i++)
                {
                    if (passengers[i].Age == age)
                    {
                        // Flytta passagerare bakåt för att ta bort den valda passageraren.
                        for (int j = i; j < passengerCount - 1; j++)
                        {
                            passengers[j] = passengers[j + 1];
                        }
                        passengerCount--; // Minska passagerarantalet.
                        Console.WriteLine("\nPassagerare borttagen.");
                        return;
                    }
                }
                Console.WriteLine("\nIngen passagerare med denna ålder hittades.");
            }
        }
    }
}
