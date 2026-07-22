//task3 Apply proper encapsulation: private backing fields, public properties, and a constructor that enforces valid initial state.


public class Bookk
{
    // Private Backing Fields
    private string _title;
    private int _pages;

    // Public Properties
    public string Title
    {
        get
        {
            return _title;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("Title cannot be empty.");
            }
            else
            {
                _title = value;
            }
        }
    }

    public int Pages
    {
        get
        {
            return _pages;
        }
        set
        {
            if (value > 0)
            {
                _pages = value;
            }
            else
            {
                Console.WriteLine("Pages must be greater than zero.");
            }
        }
    }

    // Constructor
    public Bookk(string title, int pages)
    {
        Title = title;
        Pages = pages;
    }
}