//task1. Create a List of at least 8 objects from your Day 3 domain model with varied property values.
List<Book> books = new List<Book>()
{
    new Book { BookId = 1, Title = "C#", Pages = 500 },
    new Book { BookId = 2, Title = "Java", Pages = 450 },
    new Book { BookId = 3, Title = "Operating System", Pages = 700 },
    new Book { BookId = 4, Title = "Networking", Pages = 350 },
    new Book { BookId = 5, Title = "Digital Lab", Pages = 200 },
    new Book { BookId = 6, Title = "Microprocessor", Pages = 600 },
    new Book { BookId = 7, Title = "Organization", Pages = 420 },
    new Book { BookId = 8, Title = "Digital Design", Pages = 380 }
};

//task2. Write 3 LINQ queries against the list: one filter, one projection, and one aggregation (Count, Sum, or Average).
var res1=books.Where(n => n.Pages > 500);
var res2 = books.Select(n => n.Title);
var res3=books.Sum(n => n.Pages);
Console.WriteLine("Books with more than 500 pages:");
foreach (var book in res1)
{
    Console.WriteLine(book.Title);
}

Console.WriteLine();

Console.WriteLine("Book titles:");
foreach (var title in res2)
{
    Console.WriteLine(title);
}

Console.WriteLine();

Console.WriteLine("Total pages:");
Console.WriteLine(res3);
//task3. Write an async method that simulates an I/O delay (Task.Delay) and returns a result, then await it from Main.


async Task<int> GetBookCount()
{
    await Task.Delay(3000);
    return books.Count;
}

int count = await GetBookCount();

Console.WriteLine($"Number of books: {count}");

//task4. Wrap a risky operation (e.g. parsing user input) in a try/catch that catches a specific exception type and handles it meaningfully.

Console.WriteLine("Enter Book Id: ");
string input=Console.ReadLine();

try
{
    int id = int.Parse(input);
     Console.WriteLine($"Book Id: {id}");
}
catch(FormatException)
{
    Console.WriteLine("Please enter a valid number.");
}
catch (Exception)
{
    Console.WriteLine("Unexpected error.");
}