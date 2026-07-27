using AlMandoobStoneManagement.Models;
using AlMandoobStoneManagement.Repositories;

Repository<Employee> employees = new();

employees.Add(new Employee
{
    Id = 1,
    Name = "Ahmad",
    Phone = "059111111",
    Address = "Qabatiya",
    JobTitle = "Manager",
    Salary = 3500,
    HireDate = DateTime.Now,
    IsActive = true
});

employees.Add(new Employee
{
    Id = 2,
    Name = "Ali",
    Phone = "059222222",
    Address = "Jenin",
    JobTitle = "Worker",
    Salary = 2200,
    HireDate = DateTime.Now,
    IsActive = true
});

Console.WriteLine("===== All Employees =====");

foreach (Employee employee in employees.GetAll())
{
    Console.WriteLine($"{employee.Id} - {employee.Name} - {employee.JobTitle}");
}
Console.WriteLine();
Console.WriteLine("===== Managers =====");

IEnumerable<Employee> managers =
    employees.Find(employee => employee.JobTitle == "Manager");

foreach (Employee manager in managers)
{
    Console.WriteLine($"{manager.Name} - {manager.JobTitle}");
}