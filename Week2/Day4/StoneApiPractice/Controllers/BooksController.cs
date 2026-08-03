using Microsoft.AspNetCore.Mvc;
using StoneApiPractice.Models;

namespace StoneApiPractice.Controllers;


[ApiController]
[Route("api/[controller]")]

public class BooksController : ControllerBase
{

    private static List<Book> books = new()
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


    [HttpGet]
    public IActionResult GetBooks()
    {
        return Ok(books);
    }
    [HttpGet("{id}")]
    public IActionResult GetBookById(int id)
    {
        var book = books.FirstOrDefault(b => b.Id == id);

        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }
}