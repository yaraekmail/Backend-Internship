//task1
using AlMandoobStoneManagement.Models;

public class StoneManagementService
{
    private List<Customer> customers = new()
    {
        new Customer { Id = 1, Name = "Ahmed" },
        new Customer { Id = 2, Name = "Yara" }
    };

    private List<Project> projects = new()
    {
        new Project { Id = 1, CustomerId = 1, TotalPrice = 500 },
        new Project { Id = 2, CustomerId = 2, TotalPrice = 700 }
    };

    private List<Service> services = new()
    {
        new Service { Id = 5, Name = "Restoration" },
        new Service { Id = 8, Name = "Stone Installation" }
    };

    public async Task<List<Customer>> GetCustomersAsync(CancellationToken token)
    {
        Console.WriteLine("Start loading customers...");

        await Task.Delay(5000, token);

        Console.WriteLine("Customers loaded.");

        return customers;
    }

    public async Task<List<Project>> GetProjectsAsync(CancellationToken token)
    {
        Console.WriteLine("Start loading projects...");

        await Task.Delay(3000, token);

        Console.WriteLine("Projects loaded.");

        return projects;
    }

    public async Task<List<Service>> GetServicesAsync(CancellationToken token)
    {
        Console.WriteLine("Start loading services...");

        await Task.Delay(4000, token);

        Console.WriteLine("Services loaded.");

        return services;
    }
}