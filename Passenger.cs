public class Passenger
{
    public int Age { get; set; }
    public string Gender { get; set; }

    public Passenger(int age, string gender)
    {
        Age = age;
        Gender = gender;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Ålder: {Age}, Kön: {Gender}");
    }
}
