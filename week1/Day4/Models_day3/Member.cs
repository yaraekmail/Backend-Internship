//Day 3Task
//Task1 Design a small domain (e.g. a library, a task tracker, or an order system) with at least 2 related classes.

public class Member : IPrint
{
    public String Name { get; set; }
    public String Email { get; set; }
    public int MemberId { get; set; }
      public void PrintDetails()
    {
        Console.WriteLine($"Member Name: {Name}");
    }
}

