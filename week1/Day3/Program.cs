//Day 3Task
void Print(IPrint item)
{
    item.PrintDetails();
}
Author author = new Author();
Member member = new Member();
Book book = new Book();

author.AuthorId = 1;
author.Name = "yara";
member.MemberId = 100;
member.Name = "MAYAR";
member.Email = "mayar@gmail.com";
book.BookId = 10;
book.Title = "C#";
book.Pages = 1000;
book.Author = author;
book.Member = member;

Console.WriteLine("--------------------------------------");

Console.WriteLine("Task 1: Book Information");
Console.WriteLine("--------------------------------------");
Console.WriteLine($"Book Title: {book.Title}");
Console.WriteLine($"Book Pages: {book.Pages}");
Console.WriteLine($"Book Author: {book.Author.Name}");
Console.WriteLine($"Book Member: {book.Member.Name}");
Console.WriteLine("--------------------------------------");

Console.WriteLine("Task 2:");
Console.WriteLine("--------------------------------------");
AddBookRequest addBookRequest = new AddBookRequest("C# Programming", 1000, 1);
RegisterMemberRequest memberRequest = new RegisterMemberRequest("Yara", "yara@gmail.com", 101);
BorrowBookRequest borrowRequest =
    new BorrowBookRequest(101, 1);
Console.WriteLine("=== Add Book Request ===");
Console.WriteLine($"Title: {addBookRequest.Title}");
Console.WriteLine($"Pages: {addBookRequest.Pages}");
Console.WriteLine($"Author Id: {addBookRequest.AuthorId}");

Console.WriteLine();

Console.WriteLine("=== Register Member Request ===");
Console.WriteLine($"Name: {memberRequest.Name}");
Console.WriteLine($"Email: {memberRequest.Email}");
Console.WriteLine($"Member Id: {memberRequest.MemberId}");

Console.WriteLine();

Console.WriteLine("=== Borrow Book Request ===");
Console.WriteLine($"Member Id: {borrowRequest.MemberId}");
Console.WriteLine($"Book Id: {borrowRequest.BookId}");

Console.WriteLine("--------------------------------------");
Console.WriteLine("Task 3:");
Console.WriteLine("--------------------------------------");

Bookk bookk = new Bookk("Clean Code", 464);

Console.WriteLine(bookk.Title);
Console.WriteLine(bookk.Pages);
Console.WriteLine("--------------------------------------");

Console.WriteLine("Task 4:");
Console.WriteLine("--------------------------------------");


book.Title = "C# Programming";

member.Name = "Yara";

Print(book);
Print(member);
Console.WriteLine("--------------------------------------");



