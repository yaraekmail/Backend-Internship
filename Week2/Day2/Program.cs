using AlMandoobStoneManagement.Models;
//task1
List<Customer> customers = new()
{
    new Customer
    {
        Id = 1,
        Name = "Ahmed"
    },

    new Customer
    {
        Id = 2,
        Name = "Yara"
    },
        new Customer
    {
        Id = 3,
        Name = "mayar"
    },

    new Customer
    {
        Id = 4,
        Name = "leen"
    },
        new Customer
    {
        Id = 5,
        Name = "Eyad"
    },

    new Customer
    {
        Id = 6,
        Name = "kamel"
    }
};



List<Project> projects = new()
{
    new Project { Id = 1, CustomerId = 1, EmployeeId = 1, TotalPrice = 500 },
    new Project { Id = 2, CustomerId = 1, EmployeeId = 2, TotalPrice = 700 },
    new Project { Id = 3, CustomerId = 2, EmployeeId = 1, TotalPrice = 300 },
    new Project { Id = 4, CustomerId = 3, EmployeeId = 3, TotalPrice = 1000 },
    new Project { Id = 5, CustomerId = 4, EmployeeId = 8, TotalPrice = 200 },
        new Project { Id = 6, CustomerId = 5, EmployeeId = 4, TotalPrice = 900 }

};
//task2
var totalorder=projects.GroupBy(p => p.CustomerId)
    .Select(group => new
    {
        CustomerId = group.Key,
        TotalPrice = group.Sum(p => p.TotalPrice)
    });


foreach (var item in totalorder)
{
    Console.WriteLine($"Customer: {item.CustomerId}");
    Console.WriteLine($"Total Price: {item.TotalPrice}");
}


//task3

var resut = customers.Join(
    projects,
    customer => customer.Id,
    project => project.CustomerId,
    (customer, project) => new
    {
        CustomerName = customer.Name,
        ProjectId = project.Id,
        TotalPrice = project.TotalPrice
    });
foreach (var item in resut)
{
    Console.WriteLine(item.CustomerName);
    Console.WriteLine(item.ProjectId);
    Console.WriteLine(item.TotalPrice);
    Console.WriteLine();
}
//task4
List<Project> projectsWithServices = new()
{
    new Project
    {
        Id = 1,
        CustomerId = 1,
        EmployeeId = 1,
        TotalPrice = 500,
        Services = new List<Service>
        {
            new Service { Id = 1, Name = "External Cladding" },
            new Service { Id = 2, Name = "Stone Engraving" }
        }
    },

    new Project
    {
        Id = 2,
        CustomerId = 2,
        EmployeeId = 2,
        TotalPrice = 700,
        Services = new List<Service>
        {
            new Service { Id = 3, Name = "Roman Columns" }
        }
    },

    new Project
    {
        Id = 3,
        CustomerId = 3,
        EmployeeId = 3,
        TotalPrice = 1000,
        Services = new List<Service>
        {
            new Service { Id = 4, Name = "Interior Decoration" },
            new Service { Id = 5, Name = "Stone Polishing" }
        }
    }
};
var result = projects.SelectMany(p => p.Services);
foreach(var r in result)
{
    Console.WriteLine(r.Name);
}
//task5
//(Deferred Execution)
var a = projects.Where(p => p.TotalPrice > 500);

projects.Add(new Project
{
    Id = 7,
    CustomerId = 6,
    EmployeeId = 5,
    TotalPrice = 1500
});

foreach (var project in a)
{
    Console.WriteLine(project.TotalPrice);
}
//(Immediate Execution)
var w = projects
    .Where(p => p.TotalPrice > 500)
    .ToList();

projects.Add(new Project
{
    Id = 7,
    CustomerId = 6,
    EmployeeId = 5,
    TotalPrice = 1500
});

foreach (var project in w)
{
    Console.WriteLine(project.TotalPrice);
}