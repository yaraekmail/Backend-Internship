using System.Diagnostics;
using AlMandoobStoneManagement.Models;

StoneManagementService service = new();

Console.WriteLine("========== Task 2 ==========");
await RunSequentialAsync(service);

Console.WriteLine();
Console.WriteLine("========== Task 3 ==========");
await RunConcurrentAsync(service);

Console.WriteLine();
Console.WriteLine("========== Task 4 ==========");
await RunCancellationAsync(service);


// ===============================
// Task 2
// ===============================

static async Task RunSequentialAsync(StoneManagementService service)
{
    CancellationTokenSource cts = new();

    Stopwatch stopwatch = new();

    stopwatch.Start();

    var customers = await service.GetCustomersAsync(cts.Token);
    var projects = await service.GetProjectsAsync(cts.Token);
    var services = await service.GetServicesAsync(cts.Token);

    stopwatch.Stop();

    Console.WriteLine("Customers:");
    foreach (var customer in customers)
        Console.WriteLine(customer.Name);

    Console.WriteLine();

    Console.WriteLine("Projects:");
    foreach (var project in projects)
        Console.WriteLine($"Project Id: {project.Id} - Total Price: {project.TotalPrice}");

    Console.WriteLine();

    Console.WriteLine("Services:");
    foreach (var serviceItem in services)
        Console.WriteLine(serviceItem.Name);

    Console.WriteLine();
    Console.WriteLine($"Sequential Time: {stopwatch.ElapsedMilliseconds} ms");
}


// ===============================
// Task 3
// ===============================

static async Task RunConcurrentAsync(StoneManagementService service)
{
    CancellationTokenSource cts = new();

    Stopwatch stopwatch = new();

    stopwatch.Start();

    Task<List<Customer>> customersTask = service.GetCustomersAsync(cts.Token);
    Task<List<Project>> projectsTask = service.GetProjectsAsync(cts.Token);
    Task<List<Service>> servicesTask = service.GetServicesAsync(cts.Token);

    await Task.WhenAll(customersTask, projectsTask, servicesTask);

    stopwatch.Stop();

    var customers = await customersTask;
    var projects = await projectsTask;
    var services = await servicesTask;

    Console.WriteLine("Customers:");
    foreach (var customer in customers)
        Console.WriteLine(customer.Name);

    Console.WriteLine();

    Console.WriteLine("Projects:");
    foreach (var project in projects)
        Console.WriteLine($"Project Id: {project.Id} - Total Price: {project.TotalPrice}");

    Console.WriteLine();

    Console.WriteLine("Services:");
    foreach (var serviceItem in services)
        Console.WriteLine(serviceItem.Name);

    Console.WriteLine();
    Console.WriteLine($"Concurrent Time: {stopwatch.ElapsedMilliseconds} ms");
}


// ===============================
// Task 4
// ===============================

static async Task RunCancellationAsync(StoneManagementService service)
{
    CancellationTokenSource cts = new();

    try
    {
        Task<List<Customer>> customersTask = service.GetCustomersAsync(cts.Token);

        await Task.Delay(2000);

        cts.Cancel();

        await customersTask;
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("The operation was cancelled.");
    }
}