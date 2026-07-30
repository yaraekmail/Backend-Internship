namespace AlMandoobStoneManagement.Models;

public class Project
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int EmployeeId { get; set; }

    public decimal TotalPrice { get; set; }

    public List<Service> Services { get; set; } = new();
}
