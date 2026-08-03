using StoneApiPractice.Models;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


var books = new List<Book>
{
    new Book
    {
        Id = 1,
        Title = "Clean Code",
        Price = 50
    },

    new Book
    {
        Id = 2,
        Title = "C# in Depth",
        Price = 70
    },

    new Book
    {
        Id = 3,
        Title = "Harry Potter",
        Price = 80
    }
};


app.MapGet("/books", () =>
{
    return books;
});


app.MapGet("/books/{id}", (int id) =>
{
    var book = books.FirstOrDefault(b => b.Id == id);

    if (book == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(book);
});


app.Run();