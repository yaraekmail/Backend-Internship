// using System;

// namespace Day1
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {


//         }



//     }




// }
using System.Data.Common;
using AlMandoobStoneManagement.Models;

var employees=new  Repository<Employee>();
employees.Add(new Employee
{
    Id = 1,
    Name = "Ahmad",
    JobTitle = "Manager"
});
employees.Add(new Employee
{
    Id = 2,
    Name = "Ali",
    JobTitle = "Worker"
});
foreach(var employee in employees.GetAll())
{
    Console.WriteLine($"employee Name:{employee.Name}");
    Console.WriteLine($"employee JobTitle:{employee.JobTitle}");
    Console.WriteLine("----------------");
}
var allEmployees = employees.GetAll();


//task3
var customers=new Repository<Customer>();
customers.Add(new Customer{
    Id=55,
    Name="Omar"
});
customers.Add(new Customer
{
    Id = 2,
    Name = "Khaled"
});


foreach(var customer in customers.GetAll())
{
    Console.WriteLine($"customer Name:{customer.Name}");
}