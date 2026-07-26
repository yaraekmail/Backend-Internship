//Day 3Task
//Task1 Design a small domain (e.g. a library, a task tracker, or an order system) with at least 2 related classes.

public class Book : IPrint
{
    public String Title { get; set; }
    public int BookId { get; set; }
    public Author Author { get; set; }
    public Member Member { get; set; }
    public int Pages { get; set; }
    public void PrintDetails()
    {
        Console.WriteLine($"Book Title: {Title}");
    }

}

