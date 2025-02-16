public class Passenger
{
    // Egenskap för passagerarens ålder
    public int Age { get; set; }

    // Egenskap för passagerarens kön 
    public string Gender { get; set; }

    // Skapa en ny passagerare med info om ålder och kön
    public Passenger(int age, string gender)
    {
        Age = age;       // Sätter ålder på passageraren
        Gender = gender; // Sätter kön på passageraren
    }

    // Metod för att visa passagerarens information i konsolen
    public void DisplayInfo()
    {
        Console.WriteLine($"Ålder: {Age}, Kön: {Gender}");
    }
}
